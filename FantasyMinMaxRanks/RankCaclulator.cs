using System.Diagnostics;
namespace FantasyMinMaxRanks
{
 
    public class RankCaclulator
    {
        private List<Team> Teams = new List<Team>();
        List<List<Matchup>> Matchups = new List<List<Matchup>>();

        public RankCaclulator(List<Team> teams, List<List<Matchup>> matchUps)
        {
            Teams = teams;
            Matchups = matchUps;
        }

        public async Task<Dictionary<string, RankingData>> CalculateRanks()
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                // Initial rankings (min/max for each team)
                var rankings = Teams.ToDictionary(t => t.Name, t => new RankingData());
                var rankingsUsingDenseRank = Teams.ToDictionary(t => t.Name, t => new RankingData());

                // Get all possible outcomes
                var possibleOutcomes = GeneratePossibleOutcomes();
                var possibleOutcomesLength = possibleOutcomes.Count();
                Console.WriteLine($"{possibleOutcomes.Count} diffrent simulations");

                // Run the simulations in parallel
                var tasks = possibleOutcomes.Select(outcome => Task.Run(() =>
                {
                    var simulatedTeams = Teams.Select(t => new Team
                    {
                        Name = t.Name,
                        Wins = t.Wins,
                        Losses = t.Losses,
                        TotalScore = t.TotalScore
                    }).ToList();

                    // Calculate rankings after simulation
                    SimulateMatchups(simulatedTeams, outcome);
                    var DenseRankedTeams = CalculateRanks(simulatedTeams);
                    foreach (var t in DenseRankedTeams)
                    {
                        if (rankingsUsingDenseRank[t.name].MinRank > t.MinRank + 1)
                        {
                            rankingsUsingDenseRank[t.name].MinRank = t.MinRank + 1;
                        }
                        if (rankingsUsingDenseRank[t.name].MaxRank < t.MaxRank + 1)
                        {
                            rankingsUsingDenseRank[t.name].MaxRank = t.MaxRank + 1;
                        }

                        if (t.MinRank == t.MaxRank)
                        {
                            rankingsUsingDenseRank[t.name].distrubution[t.MinRank] += 1.00d;
                        }
                        else
                        {
                            var distributaedrank = 1.00d / (t.MaxRank - t.MinRank);

                            for (int i = t.MinRank; i <= t.MaxRank; i++)
                            {
                                rankingsUsingDenseRank[t.name].distrubution[i] += distributaedrank;
                            }
                        }
                    }

                })).ToList();

                // Wait for all tasks to complete
                await Task.WhenAll(tasks);
                sw.Stop();
                Console.WriteLine(sw.Elapsed.ToString());

                // Output the results
                Console.WriteLine("Team Dense Rankings (Min and Max positions):");
                foreach (var team in rankingsUsingDenseRank)
                {
                    for (int i = 0; i < team.Value.distrubution.Length; i++)
                    {
                        rankingsUsingDenseRank[team.Key].distrubution[i] = rankingsUsingDenseRank[team.Key].distrubution[i] / possibleOutcomesLength;
                    }
                    Console.WriteLine($"{team.Key}: Min Rank = {team.Value.MinRank}, Max Rank = {team.Value.MaxRank}");
                }
                return rankingsUsingDenseRank;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }

        }
        private List<List<string>> GeneratePossibleOutcomes()
        {
            var outcomes = new List<List<string>>();

            // Generate all combinations of match outcomes (2 outcomes per match)
            int numMatchups = Matchups.Sum(week => week.Count);
            int totalOutcomes = (int)Math.Pow(2, numMatchups);

            for (int i = 0; i < totalOutcomes; i++)
            {
                var outcome = new List<string>();

                int bitmask = i;
                foreach (var week in Matchups)
                {
                    foreach (var matchup in week)
                    {
                        outcome.Add((bitmask & 1) == 0 ? matchup.TeamA : matchup.TeamB);
                        bitmask >>= 1;
                    }
                }

                outcomes.Add(outcome);
            }

            return outcomes;
        }

        private void SimulateMatchups(List<Team> simulatedTeams, List<string> outcome)
        {
            int outcomeIndex = 0;

            foreach (var week in Matchups)
            {
                foreach (var matchup in week)
                {
                    string winningTeam = outcome[outcomeIndex++];

                    // Update stats based on the winning team
                    var winningTeamObj = simulatedTeams.First(t => t.Name == winningTeam);
                    var losingTeamObj = simulatedTeams.First(t => t.Name != winningTeam && (t.Name == matchup.TeamA || t.Name == matchup.TeamB));

                    winningTeamObj.Wins++;
                    losingTeamObj.Losses++;
                }
            }
        }
        private List<TeamRank> CalculateRanks(List<Team> simulatedTeams)
        {
            var s = simulatedTeams
                .OrderByDescending(t => t.Wins)
                //.ThenByDescending(t => t.TotalScore)
                .Select((t, r) => (t.Name, t.Wins, t.Losses, rank: r))
                .ToList();
            List<TeamRank> trs = new List<TeamRank>();
            foreach (var team in Teams)
            {
                var wins = s.First(t => t.Name == team.Name).Wins;
                var max = s.Where(w => w.Wins == wins).Max(x => x.rank);
                var min = s.Where(w => w.Wins == wins).Min(x => x.rank);
                trs.Add(new TeamRank { name = team.Name, MaxRank = max, MinRank = min, scores = simulatedTeams });
            }
            return trs;
        }
    }
}

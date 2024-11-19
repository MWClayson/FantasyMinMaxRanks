using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FantasyMinMaxRanks
{
    internal class Program
    {
       public  List<Team> teamsStatic = new List<Team>
    {
        new Team { Name = "Olavers Team", Wins = 9, Losses = 2, TotalScore = 1164 },
        new Team { Name = "Hurts to be winning", Wins = 9, Losses = 2, TotalScore = 1090 },
        new Team { Name = "Stroud Boys", Wins = 7, Losses = 4, TotalScore = 1199 },
        new Team { Name = "Allen a Day’s Work", Wins = 7, Losses = 4, TotalScore = 1149 },
        new Team { Name = "Sheffield Squirrels", Wins = 6, Losses = 5, TotalScore = 1059 },
        new Team { Name = "Help me Stetson I’m Stuck", Wins = 5, Losses = 6, TotalScore = 1004 },
        new Team { Name = "Nacua Matata", Wins = 5, Losses = 6, TotalScore = 1090 },
        new Team { Name = "Get Swifty", Wins = 5, Losses = 6, TotalScore = 1054 },
        new Team { Name = "Tuffers", Wins = 4, Losses = 7, TotalScore = 1035 },
        new Team { Name = "Bijan & The Boys", Wins = 4, Losses = 7, TotalScore = 1060 },
        new Team { Name = "Willy Blue-Jeans", Wins = 3, Losses = 8, TotalScore = 956 },
        new Team { Name = "Sherlock Mahomes", Wins = 2, Losses = 9, TotalScore = 949 }
    };

        // List of matchups for the remaining weeks
        public  List<List<Matchup>> matchupsStatic = new List<List<Matchup>>
    {
        new List<Matchup> // Week 12
        {
            new Matchup { TeamA = "Olavers Team", TeamB = "Help me Stetson I’m Stuck" },
            new Matchup { TeamA = "Hurts to be winning", TeamB = "Sherlock Mahomes" },
            new Matchup { TeamA = "Sheffield Squirrels", TeamB = "Willy Blue-Jeans" },
            new Matchup { TeamA = "Tuffers", TeamB = "Nacua Matata" },
            new Matchup { TeamA = "Get Swifty", TeamB = "Bijan & The Boys" },
            new Matchup { TeamA = "Stroud Boys", TeamB = "Allen a Day’s Work" }
        },
        new List<Matchup> // Week 13
        {
            new Matchup { TeamA = "Help me Stetson I’m Stuck", TeamB = "Tuffers" },
            new Matchup { TeamA = "Hurts to be winning", TeamB = "Sheffield Squirrels" },
            new Matchup { TeamA = "Sherlock Mahomes", TeamB = "Willy Blue-Jeans" },
            new Matchup { TeamA = "Olavers Team", TeamB = "Nacua Matata" },
            new Matchup { TeamA = "Stroud Boys", TeamB = "Bijan & The Boys" },
            new Matchup { TeamA = "Get Swifty", TeamB = "Allen a Day’s Work" }
        },
        new List<Matchup> // Week 13
        {
            new Matchup { TeamA = "Help me Stetson I’m Stuck", TeamB = "Nacua Matata" },
            new Matchup { TeamA = "Hurts to be winning", TeamB = "Willy Blue-Jeans" },
            new Matchup { TeamA = "Sherlock Mahomes", TeamB = "Sheffield Squirrels" },
            new Matchup { TeamA = "Olavers Team", TeamB = "Tuffers" },
            new Matchup { TeamA = "Allen a Day’s Work", TeamB = "Bijan & The Boys" },
            new Matchup { TeamA = "Stroud Boys", TeamB = "Get Swifty" }
        }
        };
        static void Main(string[] args)
        {
            getRankingData(teamsStatic, matchupsStatic).RunSynchronously(); ;

        }

        public async Task<Dictionary<string,RankingData>> getRankingData(List<Team> teams, List<List<Matchup>> matchups)
        {
            Stopwatch sw = Stopwatch.StartNew();
            // Initial rankings (min/max for each team)
            var rankings = teams.ToDictionary(t => t.Name, t => new RankingData());
            var rankingsUsingDenseRank = teams.ToDictionary(t => t.Name, t => new RankingData());

            // Get all possible outcomes
            var possibleOutcomes = GeneratePossibleOutcomes();
            var possibleOutcomesLength = possibleOutcomes.Count();
            Console.WriteLine($"{possibleOutcomes.Count} diffrent simulations");

            // Run the simulations in parallel
            var tasks = possibleOutcomes.Select(outcome => Task.Run(() =>
            {
                var simulatedTeams = teams.Select(t => new Team
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
            Task.WhenAll(tasks).Wait();
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

            Console.WriteLine(JsonSerializer.Serialize(rankingsUsingDenseRank));
            return rankingsUsingDenseRank;
        }

        static List<List<string>> GeneratePossibleOutcomes()
        {
            var outcomes = new List<List<string>>();

            // Generate all combinations of match outcomes (2 outcomes per match)
            int numMatchups = matchups.Sum(week => week.Count);
            int totalOutcomes = (int)Math.Pow(2, numMatchups);

            for (int i = 0; i < totalOutcomes; i++)
            {
                var outcome = new List<string>();

                int bitmask = i;
                foreach (var week in matchups)
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

        static void SimulateMatchups(List<Team> simulatedTeams, List<string> outcome)
        {
            int outcomeIndex = 0;

            foreach (var week in matchups)
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
        static List<TeamRank> CalculateRanks(List<Team> simulatedTeams)
        {
            var s = simulatedTeams
                .OrderByDescending(t => t.Wins)
                //.ThenByDescending(t => t.TotalScore)
                .Select((t,r) => (t.Name, t.Wins, t.Losses, rank:r))
                .ToList();
            List<TeamRank> trs = new List<TeamRank>();
            foreach(var team in teams)
            {
                var wins = s.First(t => t.Name == team.Name).Wins;
                var max = s.Where(w => w.Wins == wins).Max(x => x.rank);
                var min = s.Where(w => w.Wins == wins).Min(x => x.rank);
                trs.Add(new TeamRank { name = team.Name, MaxRank = max, MinRank = min, scores = simulatedTeams });
            }
            return trs;
        }
        public class TeamRank
        {
            public string name;
            public int MaxRank;
            public int MinRank;
            public Dictionary<int, float> rankStats;
            public List<Team> scores;
        }

        

    }
}

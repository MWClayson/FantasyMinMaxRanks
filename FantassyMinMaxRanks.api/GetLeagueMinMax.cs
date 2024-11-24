using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using FantasyMinMaxRanks.api;
using System.Collections.Generic;
using FantasyMinMaxRanks.api.User;
using FantasyMinMaxRanks;
using System.Linq;
using FantasyMinMaxRanks.api.MatchUp;

namespace FantassyMinMaxRanks.api
{
    public static class GetLeagueMinMax
    {
        [FunctionName("GetLeagueRankStats")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string name = req.Query["leagueId"];
                if (name == null)
                {
                    return new NotFoundObjectResult("NotFound");
                }

                var options = new RestClientOptions(Environment.GetEnvironmentVariable("SleeperAPIRoot"));
                var client = new RestClient(options);

                var league = await client.GetAsync<League>(new RestRequest("/league/" + name));

                if (league.status != "in_season")
                {
                    return new BadRequestObjectResult("League is not in season");
                }

                var lastWeekInLeague = league.settings.playoff_week_start;

                var rosters = await client.GetAsync<List<Roster>>(new RestRequest("/league/" + name + "/rosters"));
                var users = await client.GetAsync<List<User>>(new RestRequest("/league/" + name + "/users"));

                List<Team> teams = rosters.Join(users, r => r.owner_id, u => u.user_id, (r, s) => new Team { Losses = r.stats.losses, Wins = r.stats.wins, TotalScore = r.stats.fpts, Name = s.metadata.team_name ?? s.display_name, roster_id = r.roster_id }).ToList();

                List<List<Matchup>> Matchups = new List<List<Matchup>>();

                for (int i = league.settings.leg; i < lastWeekInLeague; i++)
                {
                    var matchupsResult = await client.GetAsync<List<MatchUpResponse>>(new RestRequest("/league/" + name + "/matchups/" + i));
                    var matchUps = matchupsResult.Select(x => x.matchup_id).Distinct().ToList();
                    List<Matchup> matchupsWeek = new List<Matchup>();
                    foreach (var m in matchUps)
                    {
                        var tA = matchupsResult.Where(x => x.matchup_id == m).Max(r => r.roster_id);
                        var tB = matchupsResult.Where(x => x.matchup_id == m).Min(r => r.roster_id);
                        var matchup = new Matchup
                        {
                            TeamA = teams.Single(x => x.roster_id == tA).Name,
                            TeamB = teams.Single(x => x.roster_id == tB).Name
                        };
                        matchupsWeek.Add(matchup);
                    }
                    Matchups.Add(matchupsWeek);
                }

                RankCaclulator rc = new RankCaclulator(teams, Matchups);

                var result = await rc.CalculateRanks();

                return new OkObjectResult(result);
            }
            catch (Exception ex) 
            {
                log.LogError(ex, "An Error Happened");
                throw;
            }
        }
    }
}

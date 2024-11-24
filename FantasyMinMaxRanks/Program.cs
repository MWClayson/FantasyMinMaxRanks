using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FantasyMinMaxRanks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Team> teamsStatic = new List<Team>
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
            List<List<Matchup>> matchupsStatic = new List<List<Matchup>>
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
            var rCalc = new RankCaclulator(teamsStatic, matchupsStatic);
            
            rCalc.CalculateRanks().Wait();

        }      

    }
}

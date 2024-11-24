using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks.api
{

    public class Roster
    {
        public string[] co_owners { get; set; }
        public object keepers { get; set; }
        public string league_id { get; set; }
        public string owner_id { get; set; }
        public object player_map { get; set; }
        public string[] players { get; set; }
        public string[] reserve { get; set; }
        public int roster_id { get; set; }
        [JsonPropertyName("settings")]
        public Stats stats { get; set; }
        public string[] starters { get; set; }
        public string[] taxi { get; set; }
    }

    public class Stats
    {
        public int fpts { get; set; }
        public int fpts_against { get; set; }
        public int fpts_against_decimal { get; set; }
        public int fpts_decimal { get; set; }
        public int losses { get; set; }
        public int ppts { get; set; }
        public int ppts_decimal { get; set; }
        public int ties { get; set; }
        public int total_moves { get; set; }
        public int waiver_budget_used { get; set; }
        public int waiver_position { get; set; }
        public int wins { get; set; }
    }

}

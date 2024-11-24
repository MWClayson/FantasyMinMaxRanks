using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks.api.MatchUp
{

    public class MatchUpResponse
    {
        public float points { get; set; }
        public string[] players { get; set; }
        public int roster_id { get; set; }
        public object custom_points { get; set; }
        public int matchup_id { get; set; }
        public string[] starters { get; set; }
        public float[] starters_points { get; set; }
    }

}

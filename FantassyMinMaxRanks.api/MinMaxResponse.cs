using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks.api
{
    internal class MinMaxResponse
    {
        public List<RData> RankingData { get;set;}
        public int teamCount { get; set; }
        public List<string> teamNames { get; set; }

    }
    internal class RData
    {
        public string TeamName { get; set; }
        public int MaxPos { get; set; }
        public int MinPos { get; set; }
    }
}

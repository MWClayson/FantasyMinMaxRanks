using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks
{
    public class TeamRank
    {
        public string name;
        public int MaxRank;
        public int MinRank;
        public Dictionary<int, float> rankStats;
        public List<Team> scores;
    }
}

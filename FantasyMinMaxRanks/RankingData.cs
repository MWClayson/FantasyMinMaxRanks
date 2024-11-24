using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks
{
    public class RankingData
    {
        // Store min and max rank for each team
        public int MinRank { get; set; } = int.MaxValue;
        public int MaxRank { get; set; } = -1;
        public double[] distrubution { get; set; } = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
}

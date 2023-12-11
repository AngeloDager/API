
using System.Collections.Generic;

namespace APIClient.AQ1.Model
{
    public class DailyLimits
    {
        public DailyLimits()
        {
            results = new List<ResultDailyLimits>();
        }
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<ResultDailyLimits> results { get; set; }
    }
}

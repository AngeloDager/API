
using System.Collections.Generic;

namespace APIClient.AQ1.Model
{
    public class SonicRate
    {
        public SonicRate()
        {
            results = new List<ResultSonicRate>();
        }
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<ResultSonicRate> results { get; set; }
    }
}


using System.Collections.Generic;

namespace APIClient.AQ1.Model
{
    public class Sonic
    {
        public Sonic()
        {
            results = new List<ResultSonic>();
        }
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<ResultSonic> results { get; set; }
    }
}

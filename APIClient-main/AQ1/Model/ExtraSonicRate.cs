
using System.Collections.Generic;

namespace APIClient.AQ1.Model
{
    public class ExtraSonicRate
    {
        public string zone_guid { get; set; }
        public string zone_name { get; set; }
        public List<Rates> sonic_rates { get; set; }
    }
}

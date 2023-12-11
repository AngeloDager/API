
using System.Collections.Generic;

namespace APIClient.AQ1.Model
{
    public class ExtraSonic
    {
        public List<string> zone_guids { get; set; }
        public List<string> zone_names { get; set; }
        public SonicSettings sonic_settings { get; set; }
    }
}

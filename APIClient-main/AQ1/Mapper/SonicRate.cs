using System;

namespace APIClient.AQ1.Mapper
{
    public class SonicRate
    {
        public int id { get; set; }
        public DateTime? timestamp { get; set; }
        public int user_id { get; set; }
        public string username { get; set; }
        public string user_name { get; set; }
        public string zone_guid { get; set; }
        public string zone_name { get; set; }
        public string day { get; set; }
        public int sonic_rate { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
    }
}

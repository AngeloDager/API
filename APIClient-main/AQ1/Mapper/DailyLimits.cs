using System;

namespace APIClient.AQ1.Mapper
{
    public class DailyLimits
    {
        public int id { get; set; }
        public DateTime? timestamp { get; set; }
        public int user_id { get; set; }
        public string username { get; set; }
        public string user_name { get; set; }
        public string zone_guid { get; set; }
        public string zone_name { get; set; }
        public decimal? limits_all { get; set; }
        public decimal? limits_monday { get; set; }
        public decimal? limits_tuesday { get; set; }
        public decimal? limits_wednesday { get; set; }
        public decimal? limits_thursday { get; set; }
        public decimal? limits_friday { get; set; }
        public decimal? limits_saturday { get; set; }
        public decimal? limits_sunday { get; set; }
        public bool everyday_limit { get; set; }
        public string daily_limit_start { get; set; }
    }
}

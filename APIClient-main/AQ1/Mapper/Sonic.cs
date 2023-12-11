using System;

namespace APIClient.AQ1.Mapper
{
    public class Sonic
    {
        public int id { get; set; }
        public DateTime? timestamp { get; set; }
        public int user_id { get; set; }
        public string username { get; set; }
        public string user_name { get; set; }
        public string zone_guid { get; set; }
        public string zone_name { get; set; }
        public decimal? max_feed_spin_secs { get; set; }
        public decimal? min_feed_spin_secs { get; set; }
        public decimal? feed_pause_min_secs { get; set; }
        public decimal? initial_feed_spin_secs { get; set; }
        public decimal? feed_block_duration_mins { get; set; }
    }
}

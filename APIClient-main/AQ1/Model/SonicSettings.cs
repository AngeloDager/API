
namespace APIClient.AQ1.Model
{
    public class SonicSettings
    {
        public decimal? max_feed_spin_secs { get; set; }
        public decimal? min_feed_spin_secs { get; set; }
        public decimal? feed_pause_min_secs { get; set; }
        public decimal? initial_feed_spin_secs { get; set; }
        public decimal? feed_block_duration_mins { get; set; }
    }
}

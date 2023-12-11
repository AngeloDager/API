
namespace APIClient.AQ1.Model
{
    public class ResultDailyLimits
    {
        public int id { get; set; }
        public string timestamp { get; set; }
        public User user { get; set; }
        public ExtraDailyLimits extra { get; set; }
    }
}


namespace APIClient.AQ1.Model
{
    public class ResultSonic
    {
        public int id { get; set; }
        public string timestamp { get; set; }
        public User user { get; set; }
        public ExtraSonic extra { get; set; }
    }
}

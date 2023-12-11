
namespace APIClient.AQ1.Model
{
    public class ResultSonicRate
    {
        public int id { get; set; }
        public string timestamp { get; set; }
        public User user { get; set; }
        public ExtraSonicRate extra { get; set; }
    }
}

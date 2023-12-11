using APIClient.CommonUtils.Resources;

namespace APIClient.AQ1.Model
{
    public class Zone : IResponse
    {
        public string zone_name { get; set; }
        public string zone_guid { get; set; }
        public string pond_name { get; set; }
        public int pond_id { get; set; }
        public string zone { get; set; }
    }
}

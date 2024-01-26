using PostOffice.API.Data.Models;

namespace PostOffice.Client.Models
{
    public class ServiceOrderExpress
    {
        public string Name { get; set; }
        public float ServicePrice { get; set; }
        public string WeightScope { get; set; }
        public string ZoneDescription { get; set; }
    }
}

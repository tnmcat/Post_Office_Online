
namespace PostOffice.API.DTOs.WeightScope
{
    public class WeightScopeBaseDTO
    {
        public int id { get; set; }
        public float min_weight { get; set; }
        public float max_weight { get; set; }
        public string description { get; set; }
    }
}

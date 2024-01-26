namespace PostOffice.API.DTOs.ParcelType
{
    public class ParcelTypeCreateDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public float max_length { get; set; }
        public float max_width { get; set; }
        public float max_height { get; set; }
        public float over_dimension_rate { get; set; }
    }
}

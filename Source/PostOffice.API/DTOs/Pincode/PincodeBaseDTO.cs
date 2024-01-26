using PostOffice.API.DTOs.Area;

namespace PostOffice.API.DTOs.Pincode
{
    public class PincodeBaseDTO
    {
        public string? pincode { get; set; }
        public string? city_name { get; set; }
        public int area_id { get; set; }
        public AreaBaseDTO AreaBaseDTOs { get; set; }

    }
}
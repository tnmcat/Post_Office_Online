using PostOffice.API.DTOs.Pincode;

namespace PostOffice.API.DTOs.Area
{
    public class AreaBaseDTO
    {
        public int id { get; set; }
        public string? area_name { get; set; }

        public List<PincodeBaseDTO>? PincodeBaseDTOs { get; set; }  
    }
}

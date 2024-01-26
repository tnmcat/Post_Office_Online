namespace PostOffice.API.DTOs.Branch
{
    public class OfficeBranchBaseDTO
    {
        public string Id { get; set; }
        public string BranchName { get; set; }
        public string Pincode { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public string BranchPhone { get; set; }
    }
}

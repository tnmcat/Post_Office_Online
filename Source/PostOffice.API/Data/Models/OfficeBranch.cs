namespace PostOffice.API.Data.Models
{
    public class OfficeBranch
    {
        public string id { get; set; }
        public string branch_name { get; set; }
        public string pincode { get; set; }
        public string district_name { get; set;}
        public string address { get; set; }
        public string branch_phone { get; set; }

        public Pincode? Pincode { get; set; }
    }
}

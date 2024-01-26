namespace PostOffice_Server.Models
{
    public class User
    {
        public int id {  get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string fullname { get; set; }

        public string address_city { get; set; }

        public string address_district { get; set;}

        public string address_ward { get; set;}

        public string address_street { get; set;}

        public string phone { get; set;}

        public int role_id { get; set; }
    }
}

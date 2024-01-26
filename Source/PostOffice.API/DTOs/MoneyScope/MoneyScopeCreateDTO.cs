namespace PostOffice.API.DTOs.MoneyScope
{
    public class MoneyScopeCreateDTO
    {
        public int id { get; set; }
        public float min_value { get; set; }
        public float max_value { get; set; }
        public string? description { get; set; }
    }
}

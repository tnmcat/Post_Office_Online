namespace PostOffice.API.DTOs.MoneyServicePrice
{
    public class MServicePriceBaseDTO
    {

        public int id { get; set; }
        public int zone_type_id { get; set; }
        public int money_scope_id { get; set; }
        public float fee { get; set; }
    }
}

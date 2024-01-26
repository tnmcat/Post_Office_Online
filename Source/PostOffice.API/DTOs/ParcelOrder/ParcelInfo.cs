using PostOffice.API.DTOs.OrderStatus;
using PostOffice.API.DTOs.ParcelService;
using PostOffice.API.DTOs.ParcelType;

namespace PostOffice.API.DTOs.ParcelOrder
{
    public class ParcelInfo
    {
        public int id {  get; set; }
        public ParcelOrderBase parcelOrderBase {  get; set; }
        public ParcelTypeBaseDTO parcelType { get; set; }
        public ParcelServiceBaseDTO parcelService { get; set; }
        public OrderStatusBase orderStatus { get; set; }
    }
}

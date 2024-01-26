   
namespace PostOffice.API.Repositories.OrderStatus
{
 using PostOffice.API.DTOs.OrderStatus;
 using PostOffice.API.Data.Models;

    public interface IOrderStatusRepository
    { 
        Task<OrderStatusBase> GetStatusById(int id);
        Task<List<OrderStatusBase>> GetStatus();
    }
}

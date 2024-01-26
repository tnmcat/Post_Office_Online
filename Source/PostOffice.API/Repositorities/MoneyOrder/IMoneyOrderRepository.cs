

namespace PostOffice.API.Repositories.MoneyOrder
{
    using PostOffice.API.DTOs.MoneyOrder;
    using PostOffice.API.Data.Models;
    using System.Threading.Tasks;
    public interface IMoneyOrderRepository
    {

        Task<List<MoneyOrderBaseDTO>> MoneyOrders();


        Task<bool> UpdateMoneyOrder(MoneyOrderUpdateDTO moneyOrderUpdateDTO, bool isStatus = false);

        Task<MoneyOrderBaseDTO> GetMoneyOrderById(int id);
        Task<bool> CreateMoneyOrder(MoneyOrderCreateDTO moneyOrderDTO);

        Task<List<string>> GetStatus();


    }
}
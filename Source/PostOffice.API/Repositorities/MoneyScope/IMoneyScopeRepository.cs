

namespace PostOffice.API.Repositorities.MoneyScope
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.MoneyScope;
    public interface IMoneyScopeRepository
    {

        Task<List<MoneyScopeBaseDTO>> ListMoneyScope();
        Task<MoneyScopeBaseDTO> GetMoneyScopeById(int id);

        Task<MoneyScopeBaseDTO> GetMoneyScopeByValue(float value);

        Task<bool> UpdateMoneyScope(int id, MoneyScopeUpdateDTO moneyScopeUpdateDTO);

        Task<MoneyScope> CreateMoneyScope(MoneyScopeCreateDTO moneyScopeCreateDTO);


    }
}

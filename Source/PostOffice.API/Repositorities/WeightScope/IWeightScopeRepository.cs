
namespace PostOffice.API.Repositorities.WeightScope
{
    using PostOffice.API.DTOs.WeightScope;
    using PostOffice.API.Data.Models;

    public interface IWeightScopeRepository
    {
        public Task<List<WeightScopeBaseDTO>> GetAllAsync(); // Lấy tất cả các weight scope
        public List<WeightScopeBaseDTO> GetByIdAsync(int id); // Lấy weight scope theo id
        public Task<WeightScope>AddAsync(WeightScopeCreateDTO weightScope); // Thêm weight scope mới
        public Task<WeightScope> UpdateAsync(WeightScopeUpdateDTO weightScopeUpdate); // Cập nhật weight scope hiện có
        public Task DeleteAsync(int id); // Xóa weight scope theo id

        public Task<WeightScope> GetPriceWeight(int id);
    }

}


using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.User;

namespace PostOffice.API.Repositorities.User
{
    public interface IUserRepository
    {
        Task<ApiResult<string>> Authenticate(UserLoginDTO userLogin);
        Task<ApiResult<bool>> RegisterUser(UserRegisterDTO userRegister);
        Task<ApiResult<PagedResult<UserViewDTO>>> GetsUserPaging(GetUserPagingRequest request);
        Task<ApiResult<UserViewDTO>> GetById(Guid id);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateDTO request);

        Task<ApiResult<bool>> UserChangePassword(UserChangePasswordDTO request);
    }
}

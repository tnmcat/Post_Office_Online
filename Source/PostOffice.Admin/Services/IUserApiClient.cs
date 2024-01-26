using NuGet.Protocol.Plugins;
using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.User;


namespace PostOffice.Admin.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(UserLoginDTO request);

        Task<ApiResult<PagedResult<UserViewDTO>>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(UserRegisterDTO registerRequest);

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateDTO request);

        Task<ApiResult<UserViewDTO>> GetById(Guid id);

        Task<ApiResult<bool>> ChangePassword(UserChangePasswordDTO request);
    }
}

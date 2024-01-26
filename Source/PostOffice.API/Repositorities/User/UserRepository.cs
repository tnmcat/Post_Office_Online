using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostOffice.API.Repositorities.User
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserRepository(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            RoleManager<AppRole> roleManager, 
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;

        }

        public async Task<ApiResult<string>> Authenticate(UserLoginDTO userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
			if (user == null) return new ApiErrorResult<string>("Account is not exist");

			var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, true);

            if(!result.Succeeded)
            {
				return new ApiErrorResult<string>("Incorrect Email or Password");
			}

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, user.LastName),                
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.PostalCode, user.PincodeId),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JWT:ValidIssuer"],
               _config["JWT:ValidAudience"],
               claims,
               expires: DateTime.Now.AddHours(3),
               signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }         

        public async Task<ApiResult<bool>> RegisterUser([FromBody]UserRegisterDTO userRegister)
        {
            var user = await _userManager.FindByNameAsync(userRegister.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("This username has been existed");
            }
            if (await _userManager.FindByEmailAsync(userRegister.Email) != null)
            {
                return new ApiErrorResult<bool>("This email has been existed");
            }

            user = new AppUser()
            {
                Create_date = userRegister.Create_date,
                Email = userRegister.Email,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                UserName = userRegister.UserName,
                PhoneNumber = userRegister.PhoneNumber,
                PincodeId = userRegister.PincodeId,
                Address = userRegister.Address
            };

            if(await _roleManager.RoleExistsAsync(userRegister.Role))
            {
                var result = await _userManager.CreateAsync(user, userRegister.Password);

                if(!result.Succeeded)
                {
                    return new ApiErrorResult<bool>("Register failed");
                }
                //add role
                await _userManager.AddToRoleAsync(user, userRegister.Role);
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Role is not exist"); 
            }
        }

        public async Task<ApiResult<PagedResult<UserViewDTO>>> GetsUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Email.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword)
                 || x.FirstName.Contains(request.Keyword)
                 || x.LastName.Contains(request.Keyword)
                 || x.UserName.Contains(request.Keyword)
                 );
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new UserViewDTO()
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Address = x.Address,
                Create_date = x.Create_date,
            }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserViewDTO>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecords = totalRow,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserViewDTO>>(pagedResult);
        }

        public async Task<ApiResult<UserViewDTO>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewDTO>("User is not exist");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewDTO()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Create_date = user.Create_date,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                PincodeId = user.PincodeId,
                Address = user.Address,
                Roles = roles
            };
            return new ApiSuccessResult<UserViewDTO>(userVm);
        }
        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateDTO request)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == request.UserName && x.Id != id))
            {
                return new ApiErrorResult<bool>("UserName have already exist");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
           
           
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            user.PincodeId = request.PincodeId;
            user.Address = request.Address;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Update failed");
        }

        public async Task<ApiResult<bool>> UserChangePassword(UserChangePasswordDTO request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

                 if (user == null)
                {
                return new ApiErrorResult<bool>("Email is not exist");
                }
                //check old password
                if(string.Compare(request.NewPassword, request.ConfirmNewPassword) != 0)
            {
                return new ApiErrorResult<bool>("New Password and Confirm New Password does not match");
            }

               var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);            
                if (result.Succeeded)
                {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Old Password is incorrect");


        }
    }
}

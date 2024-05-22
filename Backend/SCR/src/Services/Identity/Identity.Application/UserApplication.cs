
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Identity.Application.Dtos;
using Identity.Application.Dtos.Requests;
using Identity.Domain.Aggregates.Company;
using Identity.Domain.Aggregates.User;
using Identity.Domain.Aggregates.Role;
using Identity.Domain.Aggregates;

using Identity.Application.Queries;
using Identity.IApplication;
using Identity.IApplication.Dtos.Requests;

namespace Identity.Application
{
    /// <summary>
    /// 应用服务 不应该包含太多的业务代码，如果有则试着拆分多个领域服务方法
    /// 负责功能的描述与暴露。相当于controller
    /// </summary>
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserQuery _userQuery;
        private readonly UserService _userService;

        public UserApplication(IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IRoleRepository roleRepository,
            IUserQuery userQuery,UserService userService
        )
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
            _userQuery = userQuery;
            _userService = userService;
        }

        public async Task CreateUserAsync(RegisterRequest registerRquest)
        {
            
            var encryptPsw = Guid.NewGuid().ToString();

            var psw = User.EncrypyPsw(registerRquest.Password, encryptPsw);
            var user = new User(0, registerRquest.Account, psw, encryptPsw, registerRquest.DefaultCompanyId,
                registerRquest.Mobile);
            
            
            var companies = await _companyRepository.GetCompaniesByIdsAsync(registerRquest.CompanyIds);
            if (companies.Any())
            {
                throw new Exception("user relation company can not find");
            }

            user.ChangeCompanies(companies.Select(t => t.CompanyId).ToList());
            if (registerRquest.RoleIds.Any())
            {
                var roles = await _roleRepository.GetRolesByIdsAsync(registerRquest.RoleIds);
                user.ChangeRoles(roles.Select(t => t.RoleId).ToList());
            }

            await _userService.CheckRegisterUserAsync(user);
            await _userRepository.AddUserAsync(user);
            await _userRepository.UnitOfWork.SaveEntitiesAsync();
        }


        public async Task<UserDto> UserLoginAsync(string userName, string password)
        {
            var user = await _userRepository.FindUserByUserNameAsync(userName);
            if (user == null)
            {
                throw new Exception("can not find user");
            }

            user.LoginByPassword(password);
            await _userRepository.UnitOfWork.SaveEntitiesAsync();
            //TODo 发布用户登录事件
            return ConvertUserDto(user);
        }

        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            return await _userQuery.GetProfileAsync(userId);
        }

        public async Task<PageResult<UserListDto>> GetUsersAsync(UserSearchRequest request)
        {
           return  await _userQuery.GetUsersAsync(request);
        }


        private static UserDto ConvertUserDto(User user)
        {
            if (user == null)
                return null;
            return new UserDto
            {
                Account = user.Account,
                Avatar = user.Avatar,
                Description = user.Descrption,
                UserId = user.UserId,
                Email = user.Email,
            };
        }
    }
}
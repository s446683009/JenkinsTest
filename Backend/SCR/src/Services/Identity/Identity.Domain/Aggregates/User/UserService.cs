
using SCR.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.User
{
    /// <summary>
    /// 领域服务
    /// 领域中的服务表示一个无状态的操作，它用于实现特定于某个领域的任务。 
    /// 当某个操作涉及多个聚合或不适合放在聚合和值对象上时（在一个聚合中调用另一个聚合方法），最好的方式便是使用领域服务
    /// 领域服务不需要抽离
    /// </summary>  
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
         
        }
      
    

        public async Task<bool> CheckRegisterUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Mobile))
            {
                throw new ArgumentNullException(nameof(user.Mobile),"user mobile can't empty");
            }
            if (!VerifyHelper.VerifyMobile(user.Mobile))
            {
                throw new FormatException("user mobile wrong format");
            }
            if (string.IsNullOrWhiteSpace(user.Account))
            {
                throw new ArgumentNullException(nameof(user.Account),"user name can't empty");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentNullException(nameof(user.Password),"user password can't empty");
            }
            if (user.Password.Length < 6)
            {
                throw new ArgumentException("user password length less 6");
            }

            if (user.Companies == null || user.Companies.Any())
            { 
                throw new ArgumentNullException(nameof(user.Companies),"user company can't empty");
            }



            var userAl = await _userRepository.FindUserByUserNameAsync(user.Account);
            if (userAl != null)
            {
                throw new Exception("user name exist");
            }
            user = await _userRepository.FindUserByMobileAsync(user.Mobile);
            if (user != null)
            {
                throw new Exception("user mobile exist");
            }
            return true;
        }

        
    }

}

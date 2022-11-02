using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain;
using Identity.Domain.Aggregates;
using Identity.Domain.Aggregates.User;
using Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.RDB.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public UserRepository(IdentityContext context) {
            _context = context;
        }

        public async Task<User> FindUserByIdAsync(int userId)
        {
            var user=await _context.Users.FindAsync(userId);
            return user;
        }

        public Task<User> FindUserByMobileAsync(string mobile)
        {
            var user =  _context.Users.Where(t=>t.IsDeleted==false&&t.Mobile==mobile).SingleOrDefaultAsync();
            return user;
        }

        public Task<User> FindUserByUserNameAsync(string userName)
        {
            var user = _context.Users.Where(t => t.IsDeleted == false && t.Account == userName).SingleOrDefaultAsync();
            return user;
        }

        public async Task AddUserAsync(User user)
        {
           await _context.Users.AddAsync(user);
        }
    }
}

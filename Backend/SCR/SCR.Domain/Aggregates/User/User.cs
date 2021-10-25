using System;
using System.Collections.Generic;
using System.Text;

namespace SCR.Domain.Aggregates.User
{
    public class User : IEntity
    {
        public string UserId { get; private set; }
        public string Account { get; private set; }
        public string Password { get; private set; }
        public DateTime? CreateTime { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsActived { get; private set; }
        public User(string userId,string account){
            this.Account = account;
            this.UserId = userId;
        }
        public bool ChangePassword(string newPassword) {
            this.Password = newPassword;
            return true;
        }
        
        
    }
}

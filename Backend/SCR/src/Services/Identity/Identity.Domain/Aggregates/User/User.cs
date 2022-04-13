using Identity.Domain.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identity.Domain.Aggregates.User.Entity
{
    public class User : BaseEntity,IAggregateRoot
    {
        public string UserId { get; private set; }
        public string Account { get; private set; }
        public string Password { get; private set; }
        public string PswEncryptCode { get; private set; }
        public DateTime? CreateTime { get; private set; }

        public bool IsActived { get; private set; }
        public virtual ICollection<string> Permissions { get; private set; }
        public virtual ICollection<Company> Companies { get; private set; }
        public virtual ICollection<Role> Roles { get; private set; }
        public User(string userId,string account,string password){
            this.Account = account;
            this.UserId = userId;
            this.Password = password;
        }

        public bool  ChangePassword(string newPassword,string pswEncryptCode) {
            var pswBt = Encoding.UTF8.GetBytes(newPassword);
            var encryptPsw=EncryptHelper.Hmac256(pswEncryptCode, pswBt);
            this.Password = encryptPsw;
            this.PswEncryptCode = PswEncryptCode;
            return true;
        }

        public void SetDisable() {
            this.IsActived = true;
        }
        public void SetEnable()
        {
            this.IsActived = false;
        }
        public bool LoginByEncryptedPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                throw new ArgumentNullException("The encrypted password cannot be empty");
            if (this.Password != encryptedPassword)
                return false;

            this.ModifiedTime = DateTime.Now;

           
            return true;
        }

        public override bool SetDelete() {
            this.IsActived = false;
            this.IsDeleted = true;
            return true;
        }
        public bool ChangeUserRoles(IList<Role> roles) {
            if (roles == null) {
                throw new ArgumentException("User roles can not set empty");
            }
            this.Roles = roles;
            this.ModifiedTime = DateTime.Now;
            this.Permissions.Clear();
            this.Permissions = roles.SelectMany(t=>t.Permissions).ToList();
            return true;
        }
        public bool ChangeUserCompanies(IList<Company> companies)
        {
            if (companies == null)
            {
                throw new ArgumentException("User roles can not set empty");
            }
            this.Companies = companies;
            this.ModifiedTime = DateTime.Now;
            this.Roles.Clear();
            this.Permissions.Clear();
            return true;
        }
    }
}

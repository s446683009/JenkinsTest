using Identity.Domain.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identity.Domain.Aggregates.Entity
{
    public class User : BaseEntity, IAggregateRoot
    {
        public int UserId { get; private set; }
        public string Account { get; private set; }
        public string Password { get; private set; }
        public string PswEncryptCode { get; private set; }

        public string Mobile { get; private set; }
        public string Email { get; private set; }
        public string Descrption { get; private set; }
        public int DefaultCompanyId { get; private set; }
        public bool IsActived { get; private set; }
        public string Avatar { get; private set; }
        public DateTime? LastLoginTime { get; private set; }
        public virtual ICollection<UserCompanyRelation> Companies { get;set;} 
        public virtual ICollection<UserRoleRelation> Roles { get; set; }
        public User(int userId,string account,string password,string pswEncryptCode,
            int defaultCompanyId,
            string mobile
            ){
            this.Account = account;
            this.UserId = userId;
            this.PswEncryptCode = pswEncryptCode;
            this.Password = password;
            this.DefaultCompanyId = defaultCompanyId;
            this.Mobile = mobile;
            this.Companies = new List<UserCompanyRelation>();
            //this.Roles = new List<Role>();
            this.IsActived = true;
            this.CreatedTime = DateTime.Now;
        }

        public bool  ChangePassword(string newPassword,string pswEncryptCode) {

            this.Password = EncrypyPsw(newPassword, pswEncryptCode) ;
            this.PswEncryptCode = PswEncryptCode;
            return true;
        }

        public static string EncrypyPsw(string password,string pswEncryptCode) {
            var pswBt = Encoding.UTF8.GetBytes(password);
            return EncryptHelper.Hmac256(pswEncryptCode, pswBt);
        }

        public void SetDisable() {
            this.IsActived = true;
        }
        public void SetEnable()
        {
            this.IsActived = false;
        }


        public bool LoginByPassword(string password) {
            var pswBt = Encoding.UTF8.GetBytes(password);
            var encryptPsw = EncryptHelper.Hmac256(this.PswEncryptCode, pswBt);
            return this.LoginByEncryptedPassword(encryptPsw);

        }
        public bool LoginByEncryptedPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                throw new ArgumentNullException("The encrypted password cannot be empty");
            if (this.Password != encryptedPassword)
                throw new Exception("账户或密码错误");

            this.ModifiedTime = DateTime.Now;
            this.LastLoginTime = DateTime.Now;
            


            return true;
        }

        public override bool SetDelete() {
            this.IsActived = false;
            this.IsDeleted = true;
            return true;
        }
        public bool ChangeRoles(IList<int> roles) {
            if (roles == null) {
                throw new ArgumentException("User roles can not set empty");
            }
            this.Roles.Clear();
            this.Roles = roles.Select(t => new UserRoleRelation()
            {
                RoleId = t
            }).ToList();
            this.ModifiedTime = DateTime.Now;
          
            return true;
        }
        
        public bool ChangeCompanies(IList<int> companies)
        {
            if (companies == null)
            {
                throw new ArgumentException("User company can not set empty");
            }
            this.Companies.Clear();
            this.Companies = companies.Select(t=>new UserCompanyRelation() { 
                CompanyId=t
            }).ToList();
            this.ModifiedTime = DateTime.Now;
      

            return true;
        }
        public bool SetEmail(string email) {
            this.Email = email;
            this.ModifiedTime = DateTime.Now;
            return true;
        }

        public bool ChangeAvatar(string imgUrl) {
            this.Avatar = imgUrl;
            this.ModifiedTime = DateTime.Now;
            return true;
        }
    }
}

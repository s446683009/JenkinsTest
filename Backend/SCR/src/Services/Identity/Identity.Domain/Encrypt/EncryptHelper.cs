using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Encrypt
{
    public static class EncryptHelper
    {
        public static string Hmac256(string key,byte[] data) {

            var keyBt = Encoding.UTF8.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(keyBt))
            {
                var enData=hmac.ComputeHash(data);
                return Convert.ToBase64String(enData);
            }
        
        }
    }
}

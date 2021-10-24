using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class EncryptDecrypt
    {
        public static string Encrypt(string Password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(Password);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string Decrypt(string Password)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(Password);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}

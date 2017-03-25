using System;
using System.Security.Cryptography;
using System.Text;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Services
{
    public class HmacSha1Service : ICryptoService
    {
        public string Decrypt(string toDecrypt, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string toEncrypt, string key)
        {
            using (var hasher = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(toEncrypt)));
            }
        }
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SupportWidgetXF.Encrypt
{
    public class AESEncrypt : IAESEncrypt
    {
        public AESEncrypt()
        {
        }

        public Task<string> IF_DecryptData(string input, string publicKey)
        {
            throw new NotImplementedException();
        }

        public async Task<string> IF_EncryptData(string input, string publicKey)
        {
            var decrypt = new EncryptHelper(publicKey);
            var result = decrypt.EncryptString(input);
            Debug.WriteLine(result);
            return result;
        }

        //public Task<TResponse> IF_DecryptData<TResponse, TRequest>(TRequest input, string publicKey)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> IF_DecryptData(string input, string publicKey)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<string> IF_EncryptData(string input, string publicKey)
        //{
        //    var decrypt = new EncryptHelper(publicKey);
        //    var result = decrypt.EncryptString(input.ToString());
        //}
    }
}
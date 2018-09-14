using System;
using System.Threading.Tasks;

namespace SupportWidgetXF.Encrypt
{
    public interface IAESEncrypt
    {
        Task<string> IF_DecryptData(string input, string publicKey);
        Task<string> IF_EncryptData(string input, string publicKey);
    }
}

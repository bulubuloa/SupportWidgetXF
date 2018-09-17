using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SupportWidgetXF.Encrypt
{
    public class RijndaelHelper
    {
        #region Bytes
        /// <summary>
        /// encrypt byte array data 
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <param name="passPhrase"></param>
        /// <param name="saltValue"></param>
        /// <returns></returns>
        // Example usage: EncryptBytes(someFileBytes, "SensitivePhrase", "SodiumChloride");
        public static byte[] EncryptBytes(byte[] inputBytes, string passPhrase, string saltValue)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(password.GetBytes(32), password.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();
            return CipherBytes;
        }

        // Example usage: DecryptBytes(encryptedBytes, "SensitivePhrase", "SodiumChloride");
        public static byte[] DecryptBytes(byte[] encryptedBytes, string passPhrase, string saltValue)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(password.GetBytes(32), password.GetBytes(16));

            byte[] plainBytes = null;
            using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
                {
                    plainBytes = new byte[encryptedBytes.Length];
                    int DecryptedCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);
                }
            }
            return plainBytes;
        }
        #endregion
        #region String
        public static string EncryptString(string inputString, string passPhrase, string saltValue)
        {
            if (string.IsNullOrEmpty(inputString))
                throw new ArgumentNullException("cipherText");


            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(password.GetBytes(32), password.GetBytes(16));
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, Encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(inputString);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }
        public static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        // Example usage: DecryptBytes(encryptedBytes, "SensitivePhrase", "SodiumChloride");
        public static string DecryptString(string encryptedString, string passPhrase, string saltValue)
        {
            if (string.IsNullOrEmpty(encryptedString))
                throw new ArgumentNullException("cipherText");

            if (!IsBase64String(encryptedString))
                throw new Exception("The cipherText input parameter is not base64 encoded");
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            var encryptedBytes = Convert.FromBase64String(encryptedString);
            RijndaelCipher.Mode = CipherMode.CBC;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(password.GetBytes(32), password.GetBytes(16));
            string text = null;
            using (var msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, Decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }
            return text;

        }
        #endregion
    }
}
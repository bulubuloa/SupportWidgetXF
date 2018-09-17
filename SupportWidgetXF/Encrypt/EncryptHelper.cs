using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SupportWidgetXF.Encrypt
{
    public sealed class EncryptHelper
    {
        private readonly string _inputKey;
        public EncryptHelper(string inputKey)
        {
            this._inputKey = inputKey;
        }

        #region Consts
        /// <summary>
        /// Change the Inputkey GUID when you use this code in your own program.
        /// Keep this inputkey very safe and prevent someone from decoding it some way!!
        /// </summary>

        internal const string PASSPHASE = "123!@#@!#5678281378213dasduqwbduyboinsa@!#@!uia";
        #endregion

        #region Rijndael Encryption

        /// <summary>
        /// Encrypt the given text and give the byte array back as a BASE64 string
        /// </summary>
        /// <param name="text" />The text to encrypt
        /// <param name="salt" />The pasword salt
        /// <returns>The encrypted text</returns>
        public string EncryptString(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            var aesAlg = NewRijndaelManaged();

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }
            var bytes = msEncrypt.ToArray();
            msEncrypt.Close();
            return Convert.ToBase64String(bytes);
        }
        public byte[] EncryptBytes(byte[] inputBytes)
        {
            if (inputBytes == null)
                throw new ArgumentNullException("inputBytes");

            var aesAlg = NewRijndaelManaged();

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(inputBytes);

            }
            byte[] CipherBytes = msEncrypt.ToArray();
            msEncrypt.Close();
            return msEncrypt.ToArray();

        }
        #endregion

        #region Rijndael Decryption



        // Example usage: DecryptBytes(encryptedBytes, "SensitivePhrase", "SodiumChloride");
        public byte[] DecryptBytes(byte[] encryptedBytes)
        {
            if (encryptedBytes == null)
                throw new ArgumentNullException("encryptedBytes");

            byte[] plainBytes = new byte[encryptedBytes.Length];
            var aesAlg = NewRijndaelManaged();
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (var msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {

                    int DecryptedCount = csDecrypt.Read(plainBytes, 0, plainBytes.Length);
                }
            }
            return plainBytes;
        }

        /// <summary>
        /// Checks if a string is base64 encoded
        /// </summary>
        /// <param name="base64String" />The base64 encoded string
        /// <returns>Base64 encoded stringt</returns>
        public bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        /// <summary>
        /// Decrypts the given text
        /// </summary>
        /// <param name="cipherText" />The encrypted BASE64 text
        /// <param name="salt" />The pasword salt
        /// <returns>The decrypted text</returns>
        public string DecryptString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");

            if (!IsBase64String(cipherText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var aesAlg = NewRijndaelManaged();
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
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

        #region NewRijndaelManaged
        /// <summary>
        /// Create a new RijndaelManaged class and initialize it
        /// </summary>
        /// <param name="salt" />The pasword salt
        /// <returns></returns>
        private RijndaelManaged NewRijndaelManaged()
        {
            var saltBytes = Encoding.ASCII.GetBytes(PASSPHASE);
            var key = new Rfc2898DeriveBytes(_inputKey, saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }
        #endregion

    }
}

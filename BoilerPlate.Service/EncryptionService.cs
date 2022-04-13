using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BoilerPlate.Service
{
    public class EncryptionService : IEncryptionService
    {
        static readonly char[] padding = { '=' };
        public EncryptionService()
        {
        }

        public string Encrypt(string input)
        {
            var enc = EncryptStringToBytes(input);
            return enc;
        }

        public string Encrypt(int input)
        {
            return Encrypt(input.ToString());
        }

        public string Decrypt(string cipherText)
        {
            return DecryptString(cipherText);
        }

        public T Decrypt<T>(string cipherText) where T : struct
        {
            return (T)Convert.ChangeType(DecryptString(cipherText), typeof(T));
        }

        private static string EncryptStringToBytes(string plainText)
        {
            try
            {

                byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Constants.EncryptionSymetrickey, Constants.EncryptionIv);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        plainText = Convert.ToBase64String(ms.ToArray()).TrimEnd(padding).Replace('+', '-').Replace('/', '_');
                    }
                }
                return plainText;
            }
            catch
            {

            }
            return "";
        }

        private static string DecryptString(string cipherText)
        {
            if (!string.IsNullOrEmpty(cipherText))
            {
                string incoming = cipherText.Replace('_', '/').Replace('-', '+');
                switch (cipherText.Length % 4)
                {
                    case 2: incoming += "=="; break;
                    case 3: incoming += "="; break;
                }
                try
                {
                    byte[] cipherBytes = Convert.FromBase64String(incoming);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Constants.EncryptionSymetrickey, Constants.EncryptionIv);
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.Close();
                            }
                            cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                    return cipherText;
                }
                catch
                {

                }
            }

            return "";
        }
    }
}

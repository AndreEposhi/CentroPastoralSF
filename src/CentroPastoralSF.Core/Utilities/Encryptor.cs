using System.Security.Cryptography;
using System.Text;

namespace CentroPastoralSF.Core.Utilities
{
    public class Encryptor
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public Encryptor(string algorithmKey, string algoritmhIV)
        {
            key = Encoding.UTF8.GetBytes(algorithmKey);
            iv = Encoding.UTF8.GetBytes(algoritmhIV);
        }

        public string Encrypt(string text)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            try
            {
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream();
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Decrypt(string text)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            try
            {
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream(Convert.FromBase64String(text));
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);

                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
using CentroPastoralSF.Core.Configurations;
using Microsoft.JSInterop;
using System.Security.Cryptography;
using System.Text;

namespace CentroPastoralSF.Core.Services
{
    public class CryptoService
    {
        private readonly IJSRuntime jsRuntime;

        string key = null!;
        string iv = null!;

        public CryptoService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<string> Decrypt(string encryptedData)
        {
            key = ApplicationConfiguration.EncryptorKey;
            iv = ApplicationConfiguration.EncryptorIV;

            return await jsRuntime.InvokeAsync<string>("decrypt", encryptedData, key, iv);
        }
        public async Task<string> Encrypt(string data)
        {
            key = ApplicationConfiguration.EncryptorKey;
            iv = ApplicationConfiguration.EncryptorIV;

            return await jsRuntime.InvokeAsync<string>("encrypt", data, key, iv);
        }
        private async Task GenerateKeyAndIV()
        {
            key = await jsRuntime.InvokeAsync<string>("generateKey");
            iv = await jsRuntime.InvokeAsync<string>("generateIV");

            ApplicationConfiguration.EncryptorKey = key;
            ApplicationConfiguration.EncryptorIV = iv;
        }

        public string DecryptText(string plainText, string key, string iv)
        {
            int NonceSize = 12; // AES-GCM nonce size in bytes
            int TagSize = 16;   // AES-GCM tag size in bytes

            // Convert base64 strings back to bytes
            byte[] encryptedBytes = Convert.FromBase64String(plainText);
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] nonceBytes = Convert.FromBase64String(iv);

            // Verify nonce size
            if (nonceBytes.Length != NonceSize)
            {
                throw new ArgumentException($"Invalid nonce size. Expected {NonceSize} bytes, got {nonceBytes.Length} bytes.");
            }

            // Split encrypted data into ciphertext and tag
            byte[] tag = new byte[TagSize];
            byte[] ciphertext = new byte[encryptedBytes.Length - TagSize];

            Buffer.BlockCopy(encryptedBytes, 0, ciphertext, 0, encryptedBytes.Length - TagSize);
            Buffer.BlockCopy(encryptedBytes, encryptedBytes.Length - TagSize, tag, 0, TagSize);

            // Create a buffer for the decrypted data
            byte[] decryptedData = new byte[ciphertext.Length];

            // Perform decryption
            using (var aesGcm = new AesGcm(keyBytes))
            {
                aesGcm.Decrypt(
                    nonceBytes,
                    ciphertext,
                    tag,
                    decryptedData);
            }

            // Convert decrypted bytes to string
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
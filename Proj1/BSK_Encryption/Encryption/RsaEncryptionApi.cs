using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.Encryption
{
    /// <summary>
    /// Api that handles Rsa Encryption using keys for given user.
    /// Encryption many to one.
    /// </summary>
    public class RsaEncryptionApi
    {
    
        /// <summary>
        /// Encrypt data using username public key.
        /// </summary>
        /// <param name="data">Data to encrypt(session key).</param>
        /// <param name="username">Destination user</param>
        /// <returns>Encrypted data.</returns>
        public static byte[] Encrypte(byte[] data, string username)
        {
            var rsa = new RSACryptoServiceProvider(Const.KEY_SIZE);

            string publicPath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PUBLIC_KEY_FOLDER, username);
            string publicKeyFile = Path.Combine(publicPath, Const.PUBLIC_KEY_FILENAME);

            using (var reader = new StreamReader(publicKeyFile))
            {
                string input = reader.ReadToEnd();
                rsa.FromXmlString(input);
            }

            return rsa.Encrypt(data, true);
        }

        /// <summary>
        /// Decrypt data using username private key.
        /// </summary>
        /// <param name="data">Data to decrypt(session key).</param>
        /// <param name="username">Allowed user</param>
        /// <param name="keyPharse">Key to decrypt private key</param>
        /// <returns>Decrypted data.</returns>
        public static byte[] Decrypte(byte[] data, string username, byte[] keyPharse)
        {
            var rsa = new RSACryptoServiceProvider(Const.KEY_SIZE);

            string privatePath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PRIVATE_KEY_FOLDER, username);
            string privateKeyFile = Path.Combine(privatePath, Const.PRIVATE_KEY_FILENAME);

            if (!File.Exists(privateKeyFile))
            {
                throw new Exception("No user key");
            }

            try
            {

                using (var inputStream = File.OpenRead(privateKeyFile))
                {
                    var aes = new AesEncryptionApi(CipherMode.CBC, 128, 256, keyPharse);
                    using (var streamdecrypted = aes.DecrypteStream(inputStream))
                    {
                        using (var input = new StreamReader(streamdecrypted))
                        {
                            string inputText = input.ReadToEnd();
                            rsa.FromXmlString(inputText);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw new Exception("Wrong keypharse");
            }
            return rsa.Decrypt(data, true);
        }

        /// <summary>
        /// Generate key and save it.
        /// </summary>
        /// <param name="username">Name of the key owner.</param>
        /// <param name="keyPharse">Key to encrypt private key</param>
        public static void GenerateKey(string username, byte[] keyPharse)
        {
            var rsa = new RSACryptoServiceProvider(4096);
            string publicPath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PUBLIC_KEY_FOLDER, username);
            string privatePath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PRIVATE_KEY_FOLDER, username);

            string publicKeyFile = Path.Combine(publicPath, Const.PUBLIC_KEY_FILENAME);
            string privateKeyFile = Path.Combine(privatePath, Const.PRIVATE_KEY_FILENAME);

            if (Directory.Exists(publicPath))
            {
                Directory.Delete(publicPath, true);
            }

            if (Directory.Exists(privatePath))
            {
                Directory.Delete(privatePath, true);
            }

            Directory.CreateDirectory(publicPath);
            Directory.CreateDirectory(privatePath);

            using (var file = new StreamWriter(publicKeyFile))
            {
                string publicKey = rsa.ToXmlString(false);
                file.Write(publicKey);
            }

            using (var file = File.OpenWrite(privateKeyFile))
            {
                var aes = new AesEncryptionApi(CipherMode.CBC, 128, 256, keyPharse);
                using (var fileOutput = aes.EncrypteStream(file, CryptoStreamMode.Write))
                {
                    using (var output = new StreamWriter(fileOutput))
                    {
                        string privateKey = rsa.ToXmlString(true);
                        output.Write(privateKey);
                    }
                }
            }

        }
    }
}

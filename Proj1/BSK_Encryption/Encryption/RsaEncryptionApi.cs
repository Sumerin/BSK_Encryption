using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.Encryption
{
    class RsaEncryptionApi
    {

        public static byte[] Encrypte(byte[] data, string username)
        {
            var rsa = new RSACryptoServiceProvider(4096);

            string publicPath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PUBLIC_KEY_FOLDER, username);
            string publicKeyFile = Path.Combine(publicPath, Const.PUBLIC_KEY_FILENAME);

            using (var reader = new StreamReader(publicKeyFile))
            {
                string input = reader.ReadToEnd();
                rsa.FromXmlString(input);
            }

            return rsa.Encrypt(data, true);
        }

        public static byte[] Decrypte(byte[] data, string username, byte[] keyPharse)
        {
            var rsa = new RSACryptoServiceProvider(4096);

            string privatePath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PRIVATE_KEY_FOLDER, username);
            string privateKeyFile = Path.Combine(privatePath, Const.PRIVATE_KEY_FILENAME);

            if(!File.Exists(privateKeyFile))
            {
                throw new Exception("No user key");
            }

            try
            {

            using (var inputStream = File.OpenRead(privateKeyFile))
            {
                var aes = new AesEncryptionApi(CipherMode.CBC, 128, 256);
                using (var streamdecrypted = aes.DecrypteStream(inputStream, keyPharse))
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
        /// Generateky and save it 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="keyPharse"></param>
        public static void GenerateKey(string username,byte[] keyPharse)
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
                var aes = new AesEncryptionApi(CipherMode.CBC, 128, 256);
                using (var fileOutput = aes.EncrypteStream(file,keyPharse,CryptoStreamMode.Write))
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

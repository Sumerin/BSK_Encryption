using Microsoft.VisualStudio.TestTools.UnitTesting;
using BSK_Encryption.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace BSK_Encryption.Encryption.Tests
{
    [TestClass()]
    public class AesEncryptionApiTests
    {
        string testPath;
        string resourcePath;

        string encryptedFile;
        string decryptedFile;

        string user = "TestGuys";//!! need to create that account
        string keyPharse = "Test";
        byte[] password = new byte[32];
        string userPublicPath;
        string userPrivatePath;

        [TestInitialize]
        public void Prepare()
        {
            testPath = Path.Combine(Directory.GetCurrentDirectory(), "TestsTemp");
            resourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource");

            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            }
            Directory.CreateDirectory(testPath);

            encryptedFile = Path.Combine(testPath, Path.GetRandomFileName());
            decryptedFile = Path.Combine(testPath, Path.GetRandomFileName());

            userPublicPath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PUBLIC_KEY_FOLDER, user);
            userPrivatePath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PRIVATE_KEY_FOLDER, user);

            ////Folder for public key may need copy
            //if (Directory.Exists(userPublicPath))
            //{
            //    Directory.Delete(userPublicPath, true);
            //}
            //Directory.CreateDirectory(userPublicPath);

            ////Folder for privateKey may need copy
            //if (Directory.Exists(userPrivatePath))
            //{
            //    Directory.Delete(userPrivatePath, true);
            //}
            //Directory.CreateDirectory(userPrivatePath);

        }

        [TestCleanup]
        public void CleanUp()
        {
            //if (Directory.Exists(testPath))
            //{
            //    Directory.Delete(testPath, true);
            //}

            //if (Directory.Exists(userPublicPath))
            //{
            //    Directory.Delete(userPublicPath, true);
            //}

            //if (Directory.Exists(userPrivatePath))
            //{
            //    Directory.Delete(userPrivatePath, true);
            //}
        }

        [TestMethod()]
        public void SmallFiles()
        {
            //Arrange
            string filename = "Julian.jpg";
            string file = Path.Combine(testPath, filename);

            File.Copy(Path.Combine(resourcePath, filename), file);

            var aes = new AesEncryptionApi(CipherMode.CBC, 128, 128);
            aes.Initialize();

            int orginalByte;
            int finnalByte;

            //Act

            //Encrypte
            using (var inputStream = File.OpenRead(file))
            {
                using (var cryptoStream = aes.EncrypteStream(inputStream, password, CryptoStreamMode.Read))
                {
                    using (var outputStream = File.OpenWrite(encryptedFile))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }

            //Decrypte
            using (var inputStream = File.OpenRead(encryptedFile))
            {
                using (var cryptoStream = aes.DecrypteStream(inputStream, password))
                {
                    using (var outputStream = File.OpenWrite(decryptedFile))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }

            //Assert
            using (var inputOrginalStream = File.OpenRead(file))
            {
                using (var inputFinnalStream = File.OpenRead(decryptedFile))
                {
                    Assert.AreEqual(inputOrginalStream.Length, inputFinnalStream.Length);

                    while (inputOrginalStream.Position < inputOrginalStream.Length)
                    {
                        orginalByte = inputOrginalStream.ReadByte();
                        finnalByte = inputFinnalStream.ReadByte();

                        Assert.AreEqual(orginalByte, finnalByte);
                    }
                }
            }

        }

        [TestMethod()]
        public void BigFiles()
        {
            //Arrange
            string filename = "Program.txt";
            string file = Path.Combine(testPath, filename);

            File.Copy(Path.Combine(resourcePath, filename), file);

            var aes = new AesEncryptionApi(CipherMode.CBC, 128, 32);
            aes.Initialize();

            int orginalByte;
            int finnalByte;

            //Act

            //Encrypte
            using (var inputStream = File.OpenRead(file))
            {
                using (var cryptoStream = aes.EncrypteStream(inputStream, password, CryptoStreamMode.Read))
                {
                    using (var outputStream = File.OpenWrite(encryptedFile))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }

            //Decrypte
            using (var inputStream = File.OpenRead(encryptedFile))
            {
                using (var cryptoStream = aes.DecrypteStream(inputStream, password))
                {
                    using (var outputStream = File.OpenWrite(decryptedFile))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }

            //Assert
            using (var inputOrginalStream = File.OpenRead(file))
            {
                using (var inputFinnalStream = File.OpenRead(decryptedFile))
                {
                    Assert.AreEqual(inputOrginalStream.Length, inputFinnalStream.Length);

                    while (inputOrginalStream.Position < inputOrginalStream.Length)
                    {
                        orginalByte = inputOrginalStream.ReadByte();
                        finnalByte = inputFinnalStream.ReadByte();

                        Assert.AreEqual(orginalByte, finnalByte);
                    }
                }
            }

        }
    }
}
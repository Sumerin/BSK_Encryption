using BSK_Encryption.Encryption.OFB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml;

namespace BSK_Encryption.Encryption
{
    public class AesEncryptionApi
    {
        #region field
        private CipherMode cipherMode;
        private byte[] key;
        private byte[] iV;
        private int blockSize;
        private int keySize;
        /// <summary>
        /// List of approved users.
        /// </summary>
        List<User> userList = new List<User>();
        #endregion

        #region Constructors
        public AesEncryptionApi(CipherMode ciphermode, int blockSize, int keySize)
        {
            this.cipherMode = ciphermode;
            this.blockSize = blockSize;
            this.keySize = keySize;
        }

        public AesEncryptionApi(CipherMode ciphermode, int blockSize, int keySize, byte[] password)
        {
            this.cipherMode = ciphermode;
            this.blockSize = blockSize;
            this.keySize = keySize;
            this.key = password;
        }

        private AesEncryptionApi()
        {
        }
        #endregion

        #region methods
        /// <summary>
        /// Reads the header and prepare Aes algorithm.
        /// </summary>
        /// <param name="inputPath">Encrypted file</param>
        /// <param name="user">Authorized User</param>
        /// <returns></returns>
        internal static AesEncryptionApi FromXml(XmlReader reader)
        {
            var aes = new AesEncryptionApi();
            reader.ReadToFollowing("KeySize");
            aes.keySize = reader.ReadElementContentAsInt();

            aes.blockSize = reader.ReadElementContentAsInt();

            aes.cipherMode = Conversion.CipherFromString(reader.ReadElementContentAsString());

            aes.iV = Conversion.ByteArrayFromString(reader.ReadElementContentAsString());


            do
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "User")
                {
                    var user = User.FromXml(reader);
                    aes.userList.Add(user);
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Header")
                {
                    break;
                }

            }
            while (reader.Read());


            return aes;
        }

        /// <summary>
        /// Generates key and IV.
        /// </summary>
        public void Initialize()
        {
            FileInfo info = new FileInfo(Path.GetTempFileName());
            uint dummy, sectorsPerCluster, bytesPerSector;
            int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
            string basePassword = String.Format("l{0}ol{1}{2}{3}{4}{5}", DateTime.Now, dummy, sectorsPerCluster, bytesPerSector, result, info.Name);

            key = GenerateByteArray(basePassword, keySize / 8);

            iV = GenerateByteArray(basePassword.Reverse().ToString(), 16);
        }

        /// <summary>
        /// Generate byte of array based on input parameters.
        /// </summary>
        /// <param name="keypharse">Some random string</param>
        /// <param name="size">length of output array</param>
        /// <returns></returns>
        private byte[] GenerateByteArray(string keypharse, int size)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(keypharse, Const.SALT, Iterations);
            return keyGenerator.GetBytes(size);
        }

        /// <summary>
        /// Add user to the approved users.
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <returns><c>true</c> if user exists, otherwise <c>false</c> </returns>
        public bool addUser(string name)
        {
            string userPath = Path.Combine(Const.KEY_FOLDER_PATH, Const.PUBLIC_KEY_FOLDER, name);

            if (Directory.Exists(userPath) && this.key != null)
            {
                var user = new User(name);
                user.StoreKey(this.key);
                userList.Add(user);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Write current object to file using xml standard.
        /// </summary>
        /// <param name="output">Opened xml output</param>
        public void WriteToXml(XmlWriter output)
        {
            output.WriteStartElement("Header");

            output.WriteElementString("KeySize", keySize.ToString());
            output.WriteElementString("BlockSize", blockSize.ToString());
            output.WriteElementString("CipherMode", cipherMode.ToString());

            string iVConverted = string.Join(".", new List<byte>(iV).ConvertAll(i => ((int)i).ToString()).ToArray());

            output.WriteElementString("IV", iVConverted);

            output.WriteStartElement("Users");
            foreach (User user in userList)
            {
                user.WriteToXml(output);
            }

            output.WriteEndElement();
            output.WriteEndElement();
        }

        /// <summary>
        /// Encrypte input stream.
        /// </summary>
        /// <param name="stream">Input Stream</param>
        /// <returns>Encrypted Stream</returns>
        public Stream EncrypteStream(Stream stream)
        {
            return EncrypteStream(stream, CryptoStreamMode.Read);
        }

        /// <summary>
        /// Encrypte stream.
        /// </summary>
        /// <param name="stream">Input Stream</param>
        /// <returns>Encrypted Stream</returns>
        public Stream EncrypteStream(Stream stream, CryptoStreamMode mode = CryptoStreamMode.Read)
        {
            var myAes = new RijndaelManaged();

            myAes.IV = this.iV == null ? new byte[16] : iV;
            myAes.KeySize = this.keySize;
            myAes.Key = this.key;
            if (cipherMode != CipherMode.OFB)
            {
                myAes.Mode = cipherMode;

                if (cipherMode == CipherMode.CFB)
                {
                    myAes.FeedbackSize = 8;
                    myAes.Padding = PaddingMode.None;
                }

                ICryptoTransform encryptor = myAes.CreateEncryptor();

                return new CryptoStream(stream, encryptor, mode);
            }
            else
            {
                return new OFBStream(stream, myAes, mode);
            }
        }

        /// <summary>
        /// Decrypte input stream.
        /// </summary>
        /// <param name="stream">Input Stream</param>
        /// <param name="userName">Username associated</param>
        /// <param name="keyPharse">keypharse for getting key private</param>
        /// <returns>Decrypte Stream</returns>
        public Stream DecrypteStream(Stream stream, string userName, string keyPharse)
        {

            var user = (from u in userList
                        where u.Name.Equals(userName)
                        select u).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Wrong username");
            }
            this.key = user.LoadKey(keyPharse);

            return DecrypteStream(stream);
        }

        /// <summary>
        /// Decryptye input stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Stream DecrypteStream(Stream stream, CryptoStreamMode mode = CryptoStreamMode.Read)
        {
            var myAes = new RijndaelManaged();
            myAes.IV = this.iV == null ? new byte[16] : iV;
            myAes.KeySize = this.keySize;
            myAes.Key = this.key;
            if (cipherMode != CipherMode.OFB)
            {

                myAes.Mode = cipherMode;
                if (cipherMode == CipherMode.CFB)
                {
                    myAes.FeedbackSize = 8;
                    myAes.Padding = PaddingMode.None;
                }

                ICryptoTransform decryptor = myAes.CreateDecryptor();

                return new CryptoStream(stream, decryptor, mode);
            }
            else
            {
                return new OFBStream(stream, myAes, mode);
            }
        }
        #endregion

        #region imported
        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
            out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
            out uint lpTotalNumberOfClusters);
        #endregion
    }
}

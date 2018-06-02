using System;
using System.Collections.Generic;
using System.Xml;

namespace BSK_Encryption.Encryption
{
    /// <summary>
    /// Container for user/Rsa_password manage.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Username.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Encrypted AES key by user RSA public key.
        /// </summary>
        private byte[] key;
        
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        private User()
        {
        }

        public User(string name)
        {
            this.name = name;
        }

        #region Methods
        /// <summary>
        /// Writes user from xml file.
        /// </summary>
        /// <param name="output">Opened xml file.</param>
        public void WriteToXml(XmlWriter output)
        {
            output.WriteStartElement("User");

            output.WriteElementString("Username", name);
            string keyConverted = string.Join(".", new List<byte>(key).ConvertAll(i => ((int)i).ToString()).ToArray());
            output.WriteElementString("Key", keyConverted);

            output.WriteEndElement();
        }

        /// <summary>
        /// Load config for user from xml file.
        /// </summary>
        /// <param name="input">Opened xml file.</param>
        /// <returns>Object of type <c>User</c> deserilized from file.</returns>
        public static User FromXml(XmlReader input)
        {
            var user = new User();

            input.ReadToFollowing("Username");
            user.name = input.ReadElementContentAsString();

            user.key = Conversion.ByteArrayFromString(input.ReadElementContentAsString());

            return user;
        }

        /// <summary>
        /// Encrypt with public key and store the session key.
        /// </summary>
        /// <param name="key">Key to be stored in user container.</param>
        public void StoreKey(byte[] key)
        {
            this.key = RsaEncryptionApi.Encrypte(key, this.name);
        }



        /// <summary>
        /// Decrypt with private key.
        /// </summary>
        /// <param name="keyPharse">keypharse to decrypte private key</param>
        /// <returns>Decrypted session key</returns>
        public byte[] LoadKey(string keyPharse)
        {
            byte [] keyPharseHash = SHA256EncryptionApi.getHashSha256(keyPharse);
            return RsaEncryptionApi.Decrypte(this.key, this.name, keyPharseHash);
        }


        #endregion
    }
}
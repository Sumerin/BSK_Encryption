using System;
using System.Collections.Generic;
using System.Xml;

namespace BSK_Encryption.Encryption
{
    internal class User
    {
        /// <summary>
        /// Username.
        /// </summary>
        private string name;
        /// <summary>
        /// Encrypted AES key by user RSA public key.
        /// </summary>
        private byte[] key;

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

        public void WriteToXml(XmlWriter output)
        {
            output.WriteStartElement("User");

            output.WriteElementString("Username", name);
            string keyConverted = string.Join(".", new List<byte>(key).ConvertAll(i => ((int)i).ToString()).ToArray());
            output.WriteElementString("Key", keyConverted);

            output.WriteEndElement();
        }

        public static User FromXml(XmlReader input)
        {
            var user = new User();

            input.ReadToFollowing("Username");
            user.name = input.ReadElementContentAsString();
            
            user.key = Conversion.ByteArrayFromString(input.ReadElementContentAsString());

            return user;
        }

        internal void StoreKey(byte[] key)
        {
            this.key = Encrypte(key);
        }

        /// <summary>
        /// Encrypte using user public key
        /// </summary>
        /// <param name="key">session key</param>
        /// <returns>Encrypte session key with public key</returns>
        private byte[] Encrypte(byte[] key)
        {
            return key;
        }

        /// <summary>
        /// TODO
        /// Shows the dialog for pharse input.
        /// </summary>
        /// <returns></returns>
        internal byte[] LoadKey(string keyPharse)
        {
            return Decrypte(this.key, keyPharse);
        }

        /// <summary>
        /// TODO
        /// Decrypte private key using sha-256 of pharse then dectypte sessionKey.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pharse"></param>
        /// <returns></returns>
        private byte[] Decrypte(byte[] key, string pharse)
        {
            return key;
        }
    }
}
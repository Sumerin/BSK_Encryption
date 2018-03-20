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

        public User(string name)
        {
            this.name = name;
        }

        public void WriteToXml(XmlWriter output)
        {
            output.WriteStartElement("User");

            output.WriteElementString("Username", name);
            string keyConverted = string.Join(".", new List<byte>(key).ConvertAll(i=> ((int)i).ToString()).ToArray());
            output.WriteElementString("Key", keyConverted);

            output.WriteEndElement();
        }

        internal void WriteKey(byte[] key)
        {
            this.key = key;
        }
    }
}
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
        private string key;

        public User(string name)
        {
            this.name = name;
        }

        public void WriteToXml(XmlWriter output)
        {

        }
    }
}
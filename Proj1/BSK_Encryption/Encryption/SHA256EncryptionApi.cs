using System;
using System.Security.Cryptography;
using System.Text;

namespace BSK_Encryption.Encryption
{
    internal class SHA256EncryptionApi
    {
        internal static byte[] getHashSha256(string keyPharse)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(keyPharse);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash;
        }
    }
}
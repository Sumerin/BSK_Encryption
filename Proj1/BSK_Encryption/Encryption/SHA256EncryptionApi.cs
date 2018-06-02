using System;
using System.Security.Cryptography;
using System.Text;

namespace BSK_Encryption.Encryption
{
    /// <summary>
    /// Api that handles the hasing method.
    /// </summary>
    public class SHA256EncryptionApi
    {
        /// <summary>
        /// Hash the data using SHA-256.
        /// </summary>
        /// <param name="keyPharse">String to be hashed(keypharse for private key).</param>
        /// <returns>Hashed data.</returns>
        public static byte[] getHashSha256(string keyPharse)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(keyPharse);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WCFDataBaseMacService
{
    public class MD5Hash
    {
        /// <summary>
        /// Calculate md5 hash.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>md5 Hash</returns>
        public virtual string GetMD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Verify password with given salt to the hash
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="salt">salt for given password</param>
        /// <param name="hash"></param>
        /// <returns>
        ///     <c>True</c> on succesfull verification
        ///     <c>False</c> on failed Verification
        /// </returns>
        public virtual bool VerifyMd5Hash(string password, string salt, string hash)
        {
            string calculatedHash = GetMD5Hash(password + salt);

            return calculatedHash.Equals(hash);
        }

    }
}
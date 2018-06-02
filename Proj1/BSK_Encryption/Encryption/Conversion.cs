using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.Encryption
{
    /// <summary>
    /// Handles the conversion form file(string) to functional object. 
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Conversion from string to Cipher Mode used at loading state.
        /// </summary>
        /// <param name="str">Cipher mode in string</param>
        /// <returns>Enum CipherMode.</returns>
        public static CipherMode CipherFromString(string str)
        {
            switch(str)
            {
                case "CBC":
                    return CipherMode.CBC;
                case "ECB":
                    return CipherMode.ECB;
                case "OFB":
                    return CipherMode.OFB;
                case "CFB":
                    return CipherMode.CFB;
            }
            return 0;
        }

        /// <summary>
        /// Conversion from string to array of bytes used at loading state.
        /// </summary>
        /// <param name="str">bytesString</param>
        /// <returns>Array of bytes</returns>
        public static byte[] ByteArrayFromString(string str)
        {
            var octets = str.Split('.');
            byte[] t = new byte[octets.Length];
            int i = 0;
            foreach (var octet in octets)
            {
               byte.TryParse(octet, out t[i++]);
            }
            return t;
        }
     }
}

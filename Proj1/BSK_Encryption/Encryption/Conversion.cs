using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.Encryption
{
    public static class Conversion
    {
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

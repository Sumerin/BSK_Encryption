using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption
{
    public enum CipherMode
    {
        ECB,
        CBC,
        CFB,
        OFB
    }

    internal class DataViewModel
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public CipherMode Cipher { get; set; }
        public int BlockSize { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BSK_Encryption
{

    internal class DataViewModel : INotifyPropertyChanged
    {
        #region Field
        private string inputPath;
        private string outputPath;
        private CipherMode cipher;
        private int blockSize;
        #endregion 
         
        #region Properties
        public string InputPath
        {
            get { return inputPath; }
            set
            {
                inputPath = value;
                OnPropertyChanged();
            }
        }
        public string OutputPath
        {
            get { return outputPath; }
            set
            {
                outputPath = value;
                OnPropertyChanged();
            }
        }
        public CipherMode Cipher
        {
            get { return cipher; }
            set
            {
                cipher = value;
                OnPropertyChanged();
            }
        }
        public int BlockSize
        {
            get { return blockSize; }
            set
            {
                blockSize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Notify Interface
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

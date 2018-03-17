using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace BSK_Encryption.ViewModels
{
    public class EncrypteDataViewModel : DataViewModel
    {
        #region Field
        private CipherMode cipher;
        private int blockSize;
        private List<string> users;
        #endregion

        #region Properties
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
        public string UserNames
        {
            get
            {
                if (users != null)
                {
                    return users.Count > 3 ? string.Join(";", users.GetRange(0, 3)) + "++" : string.Join(";", users);
                }
                return null;
            }

        }
        public List<string> Users
        {
            get
            {
                return users;
            }
            set
            {
                this.users = value;
                OnPropertyChanged("UserNames");
            }
        }
        #endregion
    }
}

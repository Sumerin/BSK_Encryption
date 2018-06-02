using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace BSK_Encryption.ViewModels
{
    /// <summary>
    /// Encrypt context model.
    /// </summary>
    public class EncrypteDataViewModel : DataViewModel
    {
        #region Field
        private CipherMode cipher;
        private int keySize;
        private List<string> users;
        #endregion

        #region Properties
        /// <summary>
        /// Type of given cipher from gui.
        /// </summary>
        public CipherMode Cipher
        {
            get { return cipher; }
            set
            {
                cipher = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Size of key given from gui.
        /// </summary>
        public int KeySize
        {
            get { return keySize; }
            set
            {
                keySize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Logins of users that will be authorized.
        /// Show only first 3 users.
        /// </summary>
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

        /// <summary>
        /// List of users that will be authorized.
        /// </summary>
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

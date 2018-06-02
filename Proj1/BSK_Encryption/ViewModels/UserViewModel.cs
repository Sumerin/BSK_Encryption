using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK_Encryption.ViewModels
{
    /// <summary>
    /// Singleton to remember authorized users in process.
    /// </summary>
    public class UserViewModel
    {
        private static UserViewModel instance;
        public static UserViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserViewModel();
                }
                return instance;
            }
        }

        public ObservableCollection<UserGridElement> AllUsers { get; set; }
    }
}

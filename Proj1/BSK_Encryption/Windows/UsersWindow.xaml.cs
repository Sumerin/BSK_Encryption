using BSK_Encryption.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace BSK_Encryption.Windows
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        internal List<string> users;

        public UsersWindow()
        {
            this.DataContext = UserViewModel.Instance;
            this.users = new List<string>();

            Initialize();
            InitializeComponent();
        }

        /// <summary>
        /// set the public key users visiable to the UI.
        /// </summary>
        private void Initialize()
        {
            if (UserViewModel.Instance.AllUsers == null)
            {
                UserViewModel.Instance.AllUsers = new ObservableCollection<UserGridElement>();
            }

            string path = Path.Combine(Const.KEY_FOLDER, Const.PUBLIC_KEY_FOLDER);
            foreach (string username in Directory.EnumerateDirectories(path))
            {
                var user = new UserGridElement(Path.GetFileName(username));

                if (!UserViewModel.Instance.AllUsers.Contains(user))
                {
                    UserViewModel.Instance.AllUsers.Add(user);
                }
            }
        }

        /// <summary>
        /// Pick only those which was checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            foreach (UserGridElement user in UserViewModel.Instance.AllUsers)
            {
                if(user.IsChecked)
                {
                    users.Add(user.UserName);
                }
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}

using System.Windows;

namespace BSK_Encryption.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handler
        private void Encrypte_Click(object sender, RoutedEventArgs e)
        {
            new EncrypteWindow().Show();
        }

        private void Decrypte_Click(object sender, RoutedEventArgs e)
        {
            new DecrypteWindow().Show();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Proj2.ServiceReference1;

namespace Proj2
{
    public static class Globals
    {
        public static AccessServiceClient client = null;
    }
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.client == null)
            {
                Globals.client = new AccessServiceClient();
            }
            string accessType;


            if (!Globals.client.Login(login.Text, password.Password))
            {
                return;
            }

            accessType = Globals.client.GetKonta().Where(c => c.Login == login.Text).Select(c => c.Clear).First().ToString();

            if (accessType.Equals("2") == true)
            {
                WorkerWin worker = new WorkerWin();
                App.Current.MainWindow = worker;
                this.Close();
                worker.Show();
                return;
            }

            if (accessType.Equals("3") == true)
            {
                AdminWin admin = new AdminWin();
                App.Current.MainWindow = admin;
                this.Close();
                admin.Show();
                return;
            }


            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Key_Pressed(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter) == false)
                return;

            if (Globals.client == null)
            {
                Globals.client = new AccessServiceClient();
            }
            string accessType;


            if (!Globals.client.Login(login.Text, password.Password))
            {
                return;
            }

            accessType = Globals.client.GetKonta().Where(c => c.Login == login.Text).Select(c => c.Clear).First().ToString();

            if (accessType.Equals("2") == true)
            {
                WorkerWin worker = new WorkerWin();
                App.Current.MainWindow = worker;
                this.Close();
                worker.Show();
                return;
            }

            if (accessType.Equals("3") == true)
            {
                AdminWin admin = new AdminWin();
                App.Current.MainWindow = admin;
                this.Close();
                admin.Show();
                return;
            }


            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int cl=1;
            if (Globals.client == null)
            {
                Globals.client = new AccessServiceClient();
            }

            if (login.Text[0] == 'q')
                cl = 2;
            if (login.Text[0] == 'z')
                cl = 3;

            Globals.client.Register(login.Text, password.Password, cl);
        }
    }
}

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

namespace Proj2
{
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
            if (login.Text.Equals("1") == true)
            {
                WorkerWin worker = new WorkerWin();
                App.Current.MainWindow = worker;
                this.Close();
                worker.Show();
                return;
            }

            if (login.Text.Equals("2") == true)
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

            if (login.Text.Equals("1") == true)
            {
                WorkerWin worker = new WorkerWin();
                App.Current.MainWindow = worker;
                this.Close();
                worker.Show();
                return;
            }


            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        
    }
}

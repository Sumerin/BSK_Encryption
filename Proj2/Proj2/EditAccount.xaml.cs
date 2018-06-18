using Proj2.ServiceReference1;
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
    /// Logika interakcji dla klasy EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        public EditAccount()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!pas.Password.Equals(pasp.Password))
                return;
            Konto k = new Konto() { Clear = combo.SelectedIndex+1, Login = log.Text };
            Globals.client.SetKonto(k, new int[] { 3, 3, 3, 3 }, pas.Password);
            int id = Globals.client.GetKonta().Where(c => c.Login.Equals(log.Text)).First().ID;

            Klient kl = new Klient() { Imie = im.Text, Nazwisko = naz.Text, ID_Konto=id, Adres = adr.Text };
            Globals.client.SetKlient(kl, new int[] { 3, 3, 3, 3 });
            if(prac.IsChecked==true)
            {
                Pracownik pr = new Pracownik() { Imie = im.Text, Nazwisko = naz.Text, ID_Konto = id, Data_zaczecia = DateTime.Now, Stanowisko = "Test" };
                Globals.client.SetPracownik(pr, new int[] { 3, 3, 3, 3, 3 });
            }

            this.Close();
        }
    }
}

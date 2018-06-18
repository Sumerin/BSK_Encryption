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
    /// Logika interakcji dla klasy EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        string name = "";
        string price = "";
        string availability = "";
        bool isEdit = true;
        public EditProduct()
        {
            InitializeComponent();
            isEdit = false;
        }

        public EditProduct(Product p)
        {
            InitializeComponent();
            if (p.Name == null)
                nam.IsEnabled = false;
            else
                name = p.Name;
            nam.Text = name;
            if (p.Price == null)
                pri.IsEnabled = false;
            else
                price = p.Price.ToString();
            pri.Text = price;
            if (p.Availability == null)
                ava.IsEnabled = false;
            else
                availability = p.Availability.ToString();
            ava.Text = availability;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Produkt p = new Produkt() { Nazwa = nam.Text, Cena = Decimal.Parse(pri.Text), Dostepnosc = Int32.Parse(ava.Text) };
            int cl = Globals.client.GetClear() ?? default(int);

            if(Globals.client.SetProdukt(p, new int[] { cl, cl, taj.SelectedIndex+1, cl }))
            {
                this.Close();
            }
        }

        
    }
}

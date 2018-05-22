using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Proj2.ServiceReference1;



namespace Proj2
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public float Price { get; set; }
        public int Availability { get; set; }
    }

    public class OrderedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Price { get; set; }
        public int? Number { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Client { get; set; }

        public string Adress { get; set; }
        public string State { get; set; }
        public List<OrderedProduct> Products { get; set; }
    }
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Produkt> dboProducts = Globals.client.GetProdkuty().ToList<Produkt>();
            foreach(Produkt product in dboProducts)
            {
                products.Items.Add(product);
            }
            List<Zamowienia> dboOrders = Globals.client.GetZamowienia().ToList<Zamowienia>();
            foreach(Zamowienia z in dboOrders)
            {

                var klienty = Globals.client.GetKlienty().Where(c => c.ID == z.ID_Klienta).FirstOrDefault();
                Order o = new Order { Id = z.ID,Client=klienty.Imie + " " + klienty.Nazwisko,Adress=klienty.Adres, State = z.Status.ToString(), Products = new List<OrderedProduct>() };
                var lista = Globals.client.GetZam_prody().Where(c => c.ID_Zamowienia == z.ID).ToList<zam_prod>();
                foreach(zam_prod e in lista)
                {
                    var prod = dboProducts.Where(c => c.ID == e.ID_Produkt).FirstOrDefault<Produkt>();
                    o.Products.Add(new OrderedProduct { Id = prod.ID, Name = prod.Nazwa, Price = prod.Cena, Number = e.Ilosc });
                }
                orders.Items.Add(o);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            TextBox t = sender as TextBox;
            Produkt p = t.DataContext as Produkt;
            int? avail = p.Dostepnosc;
            int num = Int32.Parse(t.Text+e.Text);
            e.Handled = !(avail >= num);
        }

        private void DodajDoKoszyka(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (basket.Items.Contains(b.DataContext)==false)
            {
                int index=basket.Items.Add(b.DataContext);
            }
                
        }

        private void ShowEdit(object sender, RoutedEventArgs e)
        {
            EditOrder edit = new EditOrder();
            edit.Show();
        }

        private void ClearBasket(object sender, RoutedEventArgs e)
        {
            basket.Items.Clear();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {

            
        }

        private void ShowOrder(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as ListView;
            Order o = obj.SelectedValue as Order;
            orderProducts.Items.Clear();
            if (o != null)
            {
                foreach(OrderedProduct or in o.Products)
                {
                    orderProducts.Items.Add(or);
                }
            }
        }
    }
}

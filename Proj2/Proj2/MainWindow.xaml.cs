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

        public decimal? Price { get; set; }
        public int? Availability { get; set; }
        public int Number { get; set; }
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
            Reload(null, null);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            TextBox t = sender as TextBox;
            Product p = t.DataContext as Product;
            int? avail = p.Availability;
            int num = Int32.Parse(t.Text+e.Text);
            e.Handled = !(avail >= num);
            if (e.Handled == false)
            {
                p.Number = num;
                CountBasketPrice();
            }
        }

        private void DodajDoKoszyka(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (basket.Items.Contains(b.DataContext)==false)
            {
                int index=basket.Items.Add(b.DataContext);
                CountBasketPrice();
            }
                
        }

        private void CountBasketPrice()
        {
            decimal? sum = 0;
            foreach (Product p in basket.Items)
            {
                sum += p.Price * p.Number;                
            }
            suma.Content = sum.ToString();
        }

        private void ShowEdit(object sender, RoutedEventArgs e)
        {
            Order o = ((FrameworkElement)sender).DataContext as Order;
            EditOrder edit = new EditOrder(o);
            edit.Show();
        }

        private void ClearBasket(object sender, RoutedEventArgs e)
        {
            basket.Items.Clear();
            suma.Content = (0m).ToString();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            Zamowienia order = new Zamowienia() { Status = 1, Data_zlozenia = DateTime.Now, ID_Klienta = Globals.client.MyKlientId() };
            Globals.client.SetZam(order, new int[] { 1, 1, 1 });

            var or = Globals.client.GetZamowienia().Last();

            foreach(Product p in basket.Items)
            {
                zam_prod z = new zam_prod() { ID_Produkt = p.Id, ID_Zamowienia = or.ID, Ilosc = p.Number };
                Globals.client.SetZam_prody(z,new int[] { 1, 1 });
            }

            ClearBasket(sender,e);
            
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

        private void Reload(object sender, RoutedEventArgs e)
        {
            products.Items.Clear();
            List<Produkt> dboProducts = Globals.client.GetProdkuty().ToList<Produkt>();
            foreach (Produkt product in dboProducts)
            {
                Product pro = new Product();
                pro.Id = product.ID;
                pro.Name = product.Nazwa;
                pro.Price = product.Cena;
                pro.Availability = product.Dostepnosc;
                pro.Number = 0;
                products.Items.Add(pro);
            }
            orders.Items.Clear();
            orderProducts.Items.Clear();
            List<Zamowienia> dboOrders = Globals.client.GetZamowienia().ToList<Zamowienia>();
            foreach (Zamowienia z in dboOrders)
            {

                var klienty = Globals.client.GetKlienty().Where(c => c.ID == z.ID_Klienta).FirstOrDefault();
                Order o = new Order { Id = z.ID, Client = klienty.Imie + " " + klienty.Nazwisko, Adress = klienty.Adres, State = z.Status.ToString(), Products = new List<OrderedProduct>() };
                var lista = Globals.client.GetZam_prody().Where(c => c.ID_Zamowienia == z.ID).ToList<zam_prod>();
                foreach (zam_prod er in lista)
                {
                    var prod = dboProducts.Where(c => c.ID == er.ID_Produkt).FirstOrDefault<Produkt>();
                    o.Products.Add(new OrderedProduct { Id = prod.ID, Name = prod.Nazwa, Price = prod.Cena, Number = er.Ilosc });
                }
                orders.Items.Add(o);
            }
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            products.Items.Clear();
            List<Produkt> dboProducts = Globals.client.GetProdkuty().Where(c => c.Nazwa.Contains(srec.Text)).ToList<Produkt>();
            foreach (Produkt product in dboProducts)
            {
                Product pro = new Product();
                pro.Id = product.ID;
                pro.Name = product.Nazwa;
                pro.Price = product.Cena;
                pro.Availability = product.Dostepnosc;
                pro.Number = 0;
                products.Items.Add(pro);
            }
        }
    }
}

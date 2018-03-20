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

public class Product {
    public int Id { get; set; }
    public string Name { get; set; }

    public float Price { get; set; }
    public int Availability { get; set; }
}

public class OrderedProduct
{
    public int Id { get; set; }
    public string Name { get; set; }

    public float Price { get; set; }
    public int Number { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string Client { get; set; }

    public string Adress { get; set; }
    public string State { get; set; }
    public List<OrderedProduct> Products { get; set; }
}

namespace Proj2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            products.Items.Add(new Product { Id = 1, Name = "Kartofle mniam mniam",Price=5.50f, Availability = 10});
            products.Items.Add(new Product { Id = 2, Name = "Ziemniak mniam mniam", Price = 6.50f, Availability = 10});

            Order o = new Order { Id = 1, Client = "tomek tomek", Adress = "pomoe 21",State="Wysłano", Products = new List<OrderedProduct>() };
            o.Products.Add(new OrderedProduct { Id = 1, Name = "Kartofle mniam mniam", Price = 5.50f, Number = 3 });
            orders.Items.Add(o);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            TextBox t = sender as TextBox;
            Product p = t.DataContext as Product;
            int avail = p.Availability;
            int num = Int32.Parse(t.Text+e.Text);
            e.Handled = !(avail >= num);
        }

        private void DodajDoKoszyka(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (basket.Items.Contains(b.DataContext)==false)
            {
                basket.Items.Add(b.DataContext);
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

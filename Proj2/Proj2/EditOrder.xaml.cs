using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string name="";
        string address="";
        string state="";
        
        public EditOrder()
        {
            InitializeComponent();
        }

        public EditOrder(Order o)
        {
            InitializeComponent();
            if (o.Client == null)
                nam.IsEnabled = false;
            else
                name = o.Client;
            nam.Text = name;
            if (o.Adress == null)
                addr.IsEnabled = false;
            else
                address = o.Adress;
            addr.Text = address;
            if (o.State == "")
                st.IsEnabled = false;
            else
                state = o.State.ToString();
            st.Text = state;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}

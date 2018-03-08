using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BSK_Encryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Container for binding values
        /// </summary>
        DataViewModel viewModel;

        public MainWindow()
        {
            viewModel = new DataViewModel();
            this.DataContext = viewModel;

            InitializeComponent();
        }

        #region Event Handler 
        /// <summary>
        /// Handles event on both browser buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (sender.Equals(source))
                {
                    viewModel.InputPath = openFileDialog.FileName;
                }
                else if(sender.Equals(dest))
                {
                    viewModel.OutputPath = openFileDialog.FileName;
                }
                else
                {
                    throw new Exception("Wtf?, What have u click?");
                }
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}

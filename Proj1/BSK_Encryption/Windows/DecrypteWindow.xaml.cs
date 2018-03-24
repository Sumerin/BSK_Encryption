using BSK_Encryption.Encryption;
using BSK_Encryption.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;

namespace BSK_Encryption.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DecrypteWindow : Window
    {
        /// <summary>
        /// Container for binding values
        /// </summary>
        DecrypteDataViewModel viewModel;

        public DecrypteWindow()
        {
            viewModel = new DecrypteDataViewModel();
            this.DataContext = viewModel;

            InitializeComponent();
        }

        #region Event Handler 

        /// <summary>
        /// Temporary start method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();

            AesEncryptionApi aes = PrepareAesEncryption(tempFile);
            DecrypteFile(tempFile, aes);
        }

        private void DecrypteFile(string tempFile, AesEncryptionApi aes)
        {
            using (var input = File.OpenRead(tempFile))
            {
                using (var decryptedStream = aes.DecrypteStream(input, viewModel.User, "Test"))
                {
                    using (var output = File.OpenWrite(viewModel.OutputPath))
                    {
                        decryptedStream.CopyTo(output);
                    }
                }
            }
        }

        private AesEncryptionApi PrepareAesEncryption(string tempFile)
        {
            AesEncryptionApi aes;
            using (var input = File.OpenRead(viewModel.InputPath))
            {
                using (var reader = XmlReader.Create(input))
                {
                    aes = AesEncryptionApi.FromXml(reader);

                    using (var output = File.OpenWrite(tempFile))
                    {
                        input.Position = (reader as IXmlLineInfo).LinePosition + "</Header>".Length;//Set proper position beacues xmlReader loads to much

                        input.CopyTo(output);
                    }
                }
            }

            return aes;
        }

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
                else if (sender.Equals(dest))
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

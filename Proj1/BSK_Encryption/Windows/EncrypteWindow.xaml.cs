using BSK_Encryption.Encryption;
using BSK_Encryption.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace BSK_Encryption.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EncrypteWindow : Window
    {
        /// <summary>
        /// Container for binding values
        /// </summary>
        EncrypteDataViewModel viewModel;

        public EncrypteWindow()
        {
            viewModel = new EncrypteDataViewModel();
            viewModel.IsNotRunning = true;
            this.DataContext = viewModel;

            InitializeComponent();
        }

        #region Event Handler 

        /// <summary>
        /// Temporary start method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Progress = 0;
                viewModel.IsNotRunning = false;
                var aes = new AesEncryptionApi(viewModel.Cipher, viewModel.BlockSize, 32);
                aes.Initialize();
                if (viewModel.Users == null || viewModel.Users?.Count == 0)
                {
                    MessageBox.Show("No Recipient was assigned", "Error", MessageBoxButton.OK);
                    return;
                }
                foreach (string nickname in viewModel.Users)
                {
                    aes.addUser(nickname);
                }
                using (var output = File.Open(viewModel.OutputPath, FileMode.Create))
                {

                    using (var xmlOutput = XmlWriter.Create(output))
                    {
                        xmlOutput.WriteStartDocument();

                        aes.WriteToXml(xmlOutput);

                        xmlOutput.Flush();

                        xmlOutput.WriteEndDocument();
                    }
                    using (var source = File.Open(viewModel.InputPath, FileMode.Open))
                    {
                        using (var sourceEncypted = aes.EncrypteStream(source))
                        {
                            Task copyTask = sourceEncypted.CopyToAsync(output);

                            new Thread(() => StartUpdatingprogress(source, copyTask))
                                .Start();

                            await copyTask;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            finally
            {
                viewModel.IsNotRunning = true;
            }
        }

        private void StartUpdatingprogress(FileStream source, Task copyTask)
        {
            long length = source.Length;
            do
            {
                double position = (double)source.Position;
                viewModel.Progress = (position / length) * 100;
            }
            while (!copyTask.IsCompleted);
            viewModel.Progress = 100;
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

        /// <summary>
        /// Open dialog and let add existing user as recipient.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new UsersWindow() { Owner = this };
            if (userWindow.ShowDialog() == true)
            {
                this.viewModel.Users = userWindow.users;
            }
        }
        #endregion

        #region Methods
        #endregion

    }
}

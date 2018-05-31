using BSK_Encryption.Encryption;
using BSK_Encryption.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            string tempFile = System.IO.Path.GetTempFileName();

            AesEncryptionApi aes = await PrepareAesEncryptionAsync(tempFile);
            DecrypteFile(tempFile, aes);
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
                viewModel.InputPath = openFileDialog.FileName;

                int lastDot = openFileDialog.FileName.LastIndexOf('.');

                viewModel.OutputPath = openFileDialog.FileName.Substring(0, lastDot) + "decrypt" + openFileDialog.FileName.Substring(lastDot);

            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Decrypte file form temporary file.
        /// </summary>
        /// <param name="tempFile">Temporary file containg only encrypted data</param>
        /// <param name="aes">Configured api</param>
        private async void DecrypteFile(string tempFile, AesEncryptionApi aes)
        {
            try
            {
                using (var input = File.OpenRead(tempFile))
                {
                    using (var decryptedStream = aes.DecrypteStream(input, viewModel.User, passwordBox.Password))
                    {
                        using (var output = File.OpenWrite(viewModel.OutputPath))
                        {
                            Task copyTask = decryptedStream.CopyToAsync(output);

                            var th = new Thread(() => StartUpdatingprogress(input, copyTask, 50, 100));
                            th.Start();

                            await copyTask;
                            th.Join();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Updating progress bar need to run asynchronously or in another thread
        /// </summary>
        /// <param name="source">Input file stream.</param>
        /// <param name="copyTask">Task which completiction is being reported</param>
        /// <param name="start">range of progress</param>
        /// <param name="end">range of progress</param>
        private void StartUpdatingprogress(FileStream source, Task copyTask, int start, int end)
        {
            long length = source.Length;
            do
            {
                double position = (double)source.Position;
                viewModel.Progress = ((position / length) * 50) + start;
            }
            while (!copyTask.IsCompleted);
            viewModel.Progress = end;
        }

        /// <summary>
        /// Asynchronously preperation of the aes and data;
        /// </summary>
        /// <param name="tempFile">Temporary file to wich data are coopied</param>
        /// <returns>Task with configured AesEncryptionApi as a result</returns>
        private Task<AesEncryptionApi> PrepareAesEncryptionAsync(string tempFile)
        {
            return Task.Run<AesEncryptionApi>(() => PrepareAesEncryption(tempFile));
        }


        /// <summary>
        /// Preperation of the aes and data;
        /// </summary>
        /// <param name="tempFile">Temporary file to wich data are coopied</param>
        /// <returns>Configured AesEncryptionApi</returns>
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

                        Task copyTask = input.CopyToAsync(output);

                        var th = new Thread(() => StartUpdatingprogress(input, copyTask, 0, 50));
                        th.Start();
                        copyTask.Wait();
                        th.Join();
                    }
                }
            }

            return aes;
        }

        #endregion


    }
}

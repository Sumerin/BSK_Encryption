﻿using BSK_Encryption.Encryption;
using BSK_Encryption.ViewModels;
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

namespace BSK_Encryption.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            byte[] hashedPassword = SHA256EncryptionApi.getHashSha256(Password.Password);
            RsaEncryptionApi.GenerateKey(Username.Text, hashedPassword);
            this.Close();
        }
    }
}

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
using System.Security.Cryptography;
using System.IO;

namespace Week6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AesCryptoServiceProvider aes;
        byte[] cipherData;
        

        public MainWindow()
        {
            InitializeComponent();
            aes = new AesCryptoServiceProvider();
            //aes.Padding = PaddingMode.Zeros;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.ECB;
        }

        private void Encrypt_Clicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPlain.Text))
                return;

            string plain = tbPlain.Text.Replace(' ', '+');
            int padding = plain.Length % 4;
            if (padding != 0)
            {
                plain += new String('+', 4 - padding);
            }

            //byte[] data = Encoding.UTF8.GetBytes(tbPlain.Text);
            byte[] data = Convert.FromBase64String(plain);
            string cipher = "";

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                }
                cipherData = memoryStream.ToArray();
                //cipher = Encoding.UTF8.GetString(cipherData);
                cipher = Convert.ToBase64String(cipherData);
            }
            tbCypher.Text = cipher;

        }

        private void Decrypt_Clicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCypher.Text))
                return;

            //byte[] data = Encoding.UTF8.GetBytes(tbCypher.Text);
            string cipher = tbCypher.Text.Replace(' ', '+');

            byte[] data = Convert.FromBase64String(cipher);
            string plain = "";

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    //cryptoStream.Write(cipherData, 0, cipherData.Length);
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                }

                //plain = Encoding.UTF8.GetString(memoryStream.ToArray());
                plain = Convert.ToBase64String(memoryStream.ToArray());
            }
            tbPlain.Text = plain;
        }
    }
}

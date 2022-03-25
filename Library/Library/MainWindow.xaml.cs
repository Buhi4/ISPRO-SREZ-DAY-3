using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            
            
            var request = (HttpWebRequest)WebRequest.Create("https://localhost:7256/Login");

            var postData = "login=" + Uri.EscapeDataString(tbLog.Text);
            postData += "&password=" + Uri.EscapeDataString(sha256(tbPass.Text));
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

            People people = new People();
            people.Show ();
            this.Close();




             string sha256(string inputString)
            {
                var crypt = new SHA256Managed();
                string hash = String.Empty;
                byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(inputString));
                foreach (byte theByte in crypto)
                {
                    hash += theByte.ToString("x2");
                }
                return hash;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bPass.Visibility == Visibility.Hidden)
            {
                bPass.Password = tbPass.Text;
                bPass.Visibility = Visibility.Visible;
                tbPass.Visibility = Visibility.Hidden;
            }
            else
            {
                tbPass.Text = bPass.Password;
                tbPass.Visibility = Visibility.Visible;
                bPass.Visibility = Visibility.Hidden;

            }
        }

        
    }
}

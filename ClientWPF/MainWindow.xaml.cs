using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        
        public string ip ;
        public int port;
        public MainWindow()
        {
        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            if ((textbox2.Text != "127.0.0.1") && (textbox2.Text != "localhost"))
            {
                textbox2.Clear();
                textbox2.AppendText("Вы ввели невернв IP");
            }
            else
            {
                if (textbox2.Text == "localhost")
                {
                    ip = "127.0.0.1";
                }
                else
                {
                    ip = textbox2.Text;
                }
                port = Int32.Parse(textbox3.Text);

            }
                string pattern = @"^\s*$";
                var message = textbox1.Text.ToLower();
          
                if (Regex.IsMatch(message, pattern))
                {
                    textbox1.AppendText("Пользователь ввел пустую строку");
                    message = textbox1.Text;
                }
                var data = Encoding.UTF8.GetBytes(message);

                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                tcpSocket.Connect(tcpEndPoint);
                tcpSocket.Send(data);

                var buffer = new byte[256];
                var size = 0;
                bool fl = true;
                var answer = new StringBuilder();
                do
                {
                    size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (tcpSocket.Available > 0);
                textbox1.Clear();
                textbox1.AppendText(answer.ToString());
                Console.WriteLine(answer);
                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
                if (message == "end")
                {
                    fl = false;
                }
        }
        private void TextBox(StringBuilder Answer)
        {
        }
        private void OnClick1(object sender, RoutedEventArgs e)
        {
            textbox1.Clear();
        }
    } 
}

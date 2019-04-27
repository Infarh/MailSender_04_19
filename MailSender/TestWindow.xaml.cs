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

using System.Net;
using System.Net.Mail;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            var user_name = UserNameEdit.Text;
            var password = PasswordEdit.SecurePassword;

            try
            {
                using (var client = new SmtpClient("smtp.yandex.ru"))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(user_name, password);

                    using (var msg = new MailMessage("shmachilin@yandex.ru", "shmachilin@gmail.com"))
                    {
                        msg.Subject = $"Заголовок сообщения {DateTime.Now}";
                        msg.Body = $"Тело сообщения {DateTime.Now}";
                        msg.IsBodyHtml = false;

                        client.Send(msg);
                    }
                }

                MessageBox.Show("Почта отправлена!");
            }
            catch(Exception error)
            {
                MessageBox.Show(
                    error.Message,
                    "Ошибка в процессе отправки почты!", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}

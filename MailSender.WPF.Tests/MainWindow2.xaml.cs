using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MailSender.WPF.Tests
{
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
        }

        private void ThreadStartClick(object Sender, RoutedEventArgs E)
        {
            var thread = new Thread(ThreadMethod);
            thread.Start();
        }

        private void ThreadMethod()
        {
            for (var i = 0; i < 100000; i++)
            {
                Thread.Sleep(10);
                TextValue.Dispatcher.Invoke(() => TextValue.Text = i.ToString());
                //TextValue.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => TextValue.Text = i.ToString()));
                //TextValue.Text = i.ToString();
            }
        }
    }
}

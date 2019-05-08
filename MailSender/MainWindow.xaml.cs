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
using MailSender.Components;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnExitClick(object Sender, RoutedEventArgs E)
        {
            Close();
        }

        private void OnLeftButtonClick(object Sender, EventArgs E)
        {
            if (!(Sender is TabItemsControl tab_control)) return;

            if (tab_control.LeftButtonVisible)
                MainTabControl.SelectedIndex--;

            tab_control.LeftButtonVisible = MainTabControl.SelectedIndex > 0;
            tab_control.RightButtonVisible = MainTabControl.SelectedIndex < MainTabControl.Items.Count;
        }

        private void OnRightButtonClick(object Sender, EventArgs E)
        {
            if (!(Sender is TabItemsControl tab_control)) return;


            if (tab_control.RightButtonVisible)
                MainTabControl.SelectedIndex++;

            tab_control.LeftButtonVisible = MainTabControl.SelectedIndex > 0;
            tab_control.RightButtonVisible = MainTabControl.SelectedIndex < MainTabControl.Items.Count - 1;
        }
    }
}

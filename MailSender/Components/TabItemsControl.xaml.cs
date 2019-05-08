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

namespace MailSender.Components
{
    public partial class TabItemsControl : UserControl
    {
        public event EventHandler LeftButtonClick;
        protected virtual void OnLeftButtonClick(EventArgs e) => LeftButtonClick?.Invoke(this, e);

        public event EventHandler RightButtonClick;
        protected virtual void OnRightButtonClick(EventArgs e) => RightButtonClick?.Invoke(this, e);

        public bool LeftButtonVisible
        {
            get => LeftArrowButton.Visibility == Visibility.Visible;
            set => LeftArrowButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool RightButtonVisible
        {
            get => RightArrowButton.Visibility == Visibility.Visible;
            set => RightArrowButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public TabItemsControl() => InitializeComponent();

        private void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            if(!(E.Source is Button button)) return;

            switch (button.Name)
            {
                case "LeftArrowButton":
                    OnLeftButtonClick(EventArgs.Empty);
                    break;
                case "RightArrowButton":
                    OnRightButtonClick(EventArgs.Empty); 
                    break;
            }
        }
    }
}

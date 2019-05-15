using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.MVVM;

namespace MailSender.WPF.Tests
{
    class MainWindow2ViewModel : ViewModel
    {
        private string _TextProperty = "Hello World!!!";

        public string TextProperty
        {
            get => _TextProperty;  
            set => Set(ref _TextProperty, value);
            //set
            //{
            //    if(Equals(_TextProperty, value)) return;
            //    _TextProperty = value;
            //    OnPropertyChanged("TextProperty");
            //}
        }
    }
}

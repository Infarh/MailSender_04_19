using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.lib.Data.Linq2SQL;
using MailSender.lib.Services.Interfaces;

namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRecipientsData _RecipientsData;

        private string _Title = "Рассыльщик почты v1";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _Status = "К спаму готов!";

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        private CollectionViewSource _FiltredRecipientsSource;

        private void OnRecipientsFiltred(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Recipient recipient) || string.IsNullOrWhiteSpace(_RecipientNameFilterText)) return;

            if (recipient.Name is null 
                || recipient.Name.IndexOf(_RecipientNameFilterText, StringComparison.OrdinalIgnoreCase) < 0)
                e.Accepted = false;
        }

        public ICollectionView FiltredRecipients => _FiltredRecipientsSource?.View;

        private ObservableCollection<Recipient> _Recipients;

        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            private set
            {
                if (!Set(ref _Recipients, value)) return;

                if (_FiltredRecipientsSource != null)
                    _FiltredRecipientsSource.Filter -= OnRecipientsFiltred;
                _FiltredRecipientsSource = new CollectionViewSource { Source = value };
                _FiltredRecipientsSource.Filter += OnRecipientsFiltred;

                RaisePropertyChanged(nameof(FiltredRecipients));
            }
        }

        private Recipient _SelectedRecipient;

        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        private string _RecipientNameFilterText;

        public string RecipientNameFilterText
        {
            get => _RecipientNameFilterText;
            set
            {
                if(!Set(ref _RecipientNameFilterText, value)) return;
                _FiltredRecipientsSource?.View?.Refresh();
            }
        }

        #region Команды

        public ICommand RefreshDataCommand { get; }

        public ICommand WriteRecipientDataCommand { get; }

        public ICommand CreateNewRecipientCommand { get; }

        #endregion

        public MainWindowViewModel(IRecipientsData RecipientsData)
        {
            _RecipientsData = RecipientsData;

            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
            WriteRecipientDataCommand = new RelayCommand<Recipient>(OnWriteRecipientDataCommandExecuted, CanWriteRecipientDataCommandExecute);
            CreateNewRecipientCommand = new RelayCommand(OnCreateNewRecipientCommandExecuted, CanCreateNewRecipientCommandExecute);
        }

        private bool CanCreateNewRecipientCommandExecute() => true;

        private void OnCreateNewRecipientCommandExecuted()
        {
            var new_recipient = new Recipient { Name = "Recipient", Email = "recipient@address.com" };
            var id = _RecipientsData.Add(new_recipient);
            if (id == 0) return;
            Recipients.Add(new_recipient);
            SelectedRecipient = new_recipient;
        }

        private bool CanWriteRecipientDataCommandExecute(Recipient recipient) => recipient != null;

        private void OnWriteRecipientDataCommandExecuted(Recipient recipient)
        {
            _RecipientsData.Edit(recipient);
            _RecipientsData.SaveChanges();
        }

        private bool CanRefreshDataCommandExecute() => true;

        private void OnRefreshDataCommandExecuted() => LoadData();

        private void LoadData()
        {
            Recipients = new ObservableCollection<Recipient>(_RecipientsData.GetAll());
        }
    }
}

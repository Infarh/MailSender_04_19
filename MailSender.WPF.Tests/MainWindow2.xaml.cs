using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private async void ThreadStartClick(object Sender, RoutedEventArgs E)
        {
            if (!(Sender is Button button)) return;

            button.IsEnabled = false;

            var data = "Hello World!";

            IProgress<double> progress = new Progress<double>(p => ProgressBarInformer.Value = p);

            var cancellation = new CancellationTokenSource();
            Interlocked.Exchange(ref _ProcessCancellationSource, cancellation)?.Cancel();

            var cancel = cancellation.Token;

            TextValue.Text = "Вычисление...";

            try
            {
                var result = await Calculator(data, progress, cancel).ConfigureAwait(true);
                TextValue.Text = result.ToString();
            }
            catch (OperationCanceledException e)
            {
                Trace.WriteLine("Операция вычисления отменена");
                TextValue.Text = "Операция вычисления отменена";
                progress.Report(0);
            }

            button.IsEnabled = true;

        }

        private async Task<int> Calculator(string Data, IProgress<double> Progress = null, CancellationToken Cancel = default)
        {
            const int iteration_count = 100;
            for (var i = 0; i < iteration_count; i++)
            {
                Cancel.ThrowIfCancellationRequested();
                Progress?.Report((double)i / iteration_count);
                await Task.Delay(10).ConfigureAwait(false);
            }

            Progress?.Report(1);
            return Data.Length;
        }

        private CancellationTokenSource _ProcessCancellationSource = new CancellationTokenSource();
        private void OnCancelButtonClick(object Sender, RoutedEventArgs E)
        {
            _ProcessCancellationSource.Cancel();
        }
    }
}

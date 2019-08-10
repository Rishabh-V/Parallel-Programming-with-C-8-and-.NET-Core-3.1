using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stocks.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search stock click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            var ticker = new Stopwatch();
            ticker.Start();
            search.Content = "Cancel";
            taskProgress.IsIndeterminate = false;
            taskProgress.Value = 0;
            taskProgress.Maximum = 100;

            //On clicking of Search/Canacel checking to cancel opearation or perform search
            if (cts != null)
            {
                cts.Cancel();
                cts = null;
                return;
            }

            cts = new CancellationTokenSource();

            //Delegate on cancellation token when there is a cancellation, executes on calling thread's context in this case UI
            cts.Token.Register(() =>
            {
                progressMessage.Content = "Search is cancelled"; // Not very useful in this case as same benefit can be achieved for the code after await
            });

            //Cancellation needs to be handled gracefully

            //Progres reporting
            var progress = new Progress<double>(percent =>
            {
                taskProgress.Value = percent;
            });
            try
            {
                stockData.ItemsSource = await GetDataFromAPIAsync(searchText.Text, cts.Token, progress);
                logs.Text += "API returned data" + Environment.NewLine;                
            }
            catch (OperationCanceledException ex)
            {
                logs.Text = ex.Message;
            }
            catch (Exception ex)
            {
                logs.Text = ex.Message;
            }
            finally
            {
                cts = null;
            }
            progressMessage.Content = $"Elapsed time - {ticker.ElapsedMilliseconds}ms";
            search.Content = "Search";

        }

        /// <summary>
        /// Async method to retieve data from stocks API
        /// </summary>
        /// <param name="intputSearchtext">Search text</param>
        /// <param name="ctsAPI">Cancellation token</param>
        /// <returns>Binding source</returns>
        private async Task<IEnumerable<Stock>> GetDataFromAPIAsync(string intputSearchtext, CancellationToken ctsAPI, IProgress<double> progress = null)
        {
            Uri requestUri = new Uri("https://localhost:44394/api/Stocks");
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(requestUri, ctsAPI);
                progress?.Report(75); // A simple way to report progress, here we are assuming 75% of task is completed.
                await Task.Delay(5000);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                progress?.Report(100); //REporting 100% task completeion
                var data = JsonConvert.DeserializeObject<IEnumerable<Stock>>(content);
                return data.Where(price => price.StockName == intputSearchtext);
            }
        }
    }
}

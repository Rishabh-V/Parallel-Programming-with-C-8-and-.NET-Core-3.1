using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stocks.Windows
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts = null;
        BindingSource bindingSource1 = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search stock click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Search_Click(object sender, EventArgs e)
        {
            long highCPUCount = 0;
            stockData.Rows.Clear();
            stockData.Refresh();            
            var ticker = new Stopwatch();
            ticker.Start();
            search.Text = "Cancel";

            #region Synchronous Calls            

            //var request = WebRequest.Create("https://localhost:44394/api/StockSynchronous");
            //var response = request.GetResponse();
            //Stream dataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(dataStream);
            //string responseFromServer = reader.ReadToEnd();
            //var data = JsonConvert.DeserializeObject<IEnumerable<Stock>>(responseFromServer);

            //bindingSource1.DataSource = data.Where(price => price.StockName == searchText.Text);
            //stockData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            #endregion

            #region Async Calls

            //On clicking of Search/Cancel checking to cancel opearation or perform search
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
                progressMessage.Text = "Search is cancelled" ; // Not very useful in this case as same benefit can be achieved for the code after await
            });
            
            //Cancellation needs to be handled gracefully
            try
            {
                # region "Scenario 1"
                //stockData.DataSource = await GetDataFromAPIAsync(searchText.Text, cts.Token);
                //Logs.Text += "API returned data" + Environment.NewLine;
                #endregion
                # region "Scenario 2"
                highCPUCount = await DoHighCPUIntense(cts.Token);
                Logs.Text += $"Counted till {highCPUCount.ToString()}" + Environment.NewLine;
                #endregion
            }
            catch (OperationCanceledException ex)
            {
                Logs.Text = ex.Message;
            }
            finally
            {
                cts = null;
            }
            #endregion

            progressMessage.Text = $"Elapsed time - {ticker.ElapsedMilliseconds}ms";
            search.Text = "Search";
        }

        /// <summary>
        /// Async method to retieve data from stocks API
        /// </summary>
        /// <param name="intputSearchtext">Search text</param>
        /// <param name="ctsAPI">Cancellation token</param>
        /// <returns>Binding source</returns>
        private async Task<BindingSource> GetDataFromAPIAsync(string intputSearchtext, CancellationToken ctsAPI)
        {            
            Uri requestUri = new Uri("https://localhost:44394/api/Stocks");
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(requestUri, ctsAPI);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<IEnumerable<Stock>>(content);
                bindingSource1.DataSource = data.Where(price => price.StockName == intputSearchtext);
            }
            return bindingSource1;
        }

        /// <summary>
        /// Event handler for navigation to add stock form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStock_Click(object sender, EventArgs e)
        {
            addStockForm form = new addStockForm();
            form.Show(this);
            Hide();
        }

        /// <summary>
        /// Async method doing high CPU operation
        /// </summary>
        /// <returns></returns>
        private async Task<long> DoHighCPUIntense(CancellationToken token)
        {
            long counter = 0;
            search.Text = "Stop";
            Task<long> output = Task.Run(() =>
            {
                while (true)
                {
                    counter++;
                    if (token.IsCancellationRequested)
                    {
                        counter++;
                        break;
                    }
                }
                return counter;
            }, token);
            try
            {
                await output;
            }
            catch (AggregateException agEx)
            {
                throw agEx;
            }
            return counter;
        }
    }
}

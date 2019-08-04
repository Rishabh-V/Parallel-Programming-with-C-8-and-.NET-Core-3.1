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
                progressMessage.Text = "Search is cancelled" ;
            });
            
            //Cancellation needs to be handled gracefully
            try
            {

                stockData.DataSource = await GetDataFromAPIAsync(searchText.Text, cts.Token);
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

            progressMessage.Text = $"Loaded stocks for {searchText.Text} in {ticker.ElapsedMilliseconds}ms";
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
            BindingSource bindingSource1 = new BindingSource();
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
    }
}

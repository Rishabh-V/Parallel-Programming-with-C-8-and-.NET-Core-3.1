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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stocks.Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Search_Click(object sender, EventArgs e)
        {
            BindingSource bindingSource1 = new BindingSource();
            var ticker = new Stopwatch();
            ticker.Start();

            #region Synchronous Calls            

            var request = WebRequest.Create("https://localhost:44394/api/StockSynchronous");
            var response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var data = JsonConvert.DeserializeObject<IEnumerable<Stock>>(responseFromServer);

            bindingSource1.DataSource = data.Where(price => price.StockName == searchText.Text);
            stockData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            stockData.DataSource = bindingSource1;
            #endregion

            #region Async Calls
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:44394/api/StockS");

                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<IEnumerable<Stock>>(content);
                bindingSource1.DataSource = data.Where(price => price.StockName == searchText.Text);
            }
            stockData.DataSource = bindingSource1;
            #endregion

            progressMessage.Text = $"Loaded stocks for {searchText.Text} in {ticker.ElapsedMilliseconds}ms";
        }
    }
}

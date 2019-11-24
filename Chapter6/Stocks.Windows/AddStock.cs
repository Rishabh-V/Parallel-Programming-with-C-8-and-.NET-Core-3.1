using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stocks.Windows
{
    public partial class addStockForm : Form
    {
        public addStockForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveStock_Click(object sender, EventArgs e)
        {
            bool exceptionOccured = false;
            errorMessage.Text = "";
            #region asycn void            
            try
            {
                await SaveDataAsyncVoid();
            }
            catch (Exception ex)
            {
                exceptionOccured = true;
                //This is never caught
                errorMessage.Text = $"Exception occured in SaveDataAsyncVoid method - {ex.Message} \n Innerstack \n {ex.StackTrace}";
            }
            finally
            {                
                setID.Text = "";
                setStockName.Text = "";
                setStockPrice.Text = "";
                setStockVolume.Text = "";
            }
            #endregion
            if (!exceptionOccured)
                errorMessage.Text = $"SaveStock_Click completed";
        }

        /// <summary>
        /// Async method to save data through API
        /// </summary>        
        private async Task SaveDataAsyncVoid()
        {
            using (HttpClient client = new HttpClient())
            {
                Stock data = new Stock()
                {
                    Id = Convert.ToInt32(setID.Text),
                    StockName = setStockName.Text,
                    Price = Convert.ToDouble(setStockPrice.Text),
                    TradeDate = DateTime.Today.Date.AddDays(-1),
                    Volume = Convert.ToInt32(setStockVolume.Text)

                };
                var myContent = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync("https://localhost:44394/api/stocks", byteContent);
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }

                errorMessage.BeginInvoke((MethodInvoker)delegate () {
                    errorMessage.Text = "Data saved successfully";
                });
            }
        }

        private void BackToForm1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }
    }
}

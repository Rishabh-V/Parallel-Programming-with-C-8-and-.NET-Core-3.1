using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace ProfilingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpClient httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.txtUrl.Text = "https://bpbonline.com/collections/c-sharp";
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            await GoodCodeAsync();
        }

        private void BadCode()
        {
            try
            {
                httpClient.BaseAddress = new Uri(this.txtUrl.Text);
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
                var result = httpClient.GetAsync("/").Result;
                result.EnsureSuccessStatusCode();
                var output = result.Content.ReadAsStringAsync().Result;
                this.lstData.Items.Add(output);
                this.lstData.UpdateLayout();
            }
            catch (Exception ex)
            {
                this.lstData.Items.Add(ex.ToString());
            }
        }

        private async Task GoodCodeAsync()
        {
            try
            {
                httpClient.BaseAddress = new Uri(this.txtUrl.Text);
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
                var result = await httpClient.GetAsync("/");
                result.EnsureSuccessStatusCode();
                var output = await result.Content.ReadAsStringAsync();
                this.lstData.Items.Add(output);
            }
            catch (Exception ex)
            {
                this.lstData.Items.Add(ex.ToString());
            }
        }
    }
}

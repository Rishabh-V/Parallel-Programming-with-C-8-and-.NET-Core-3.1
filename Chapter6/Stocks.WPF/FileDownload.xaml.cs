using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Linq;
using System.Diagnostics;

namespace Stocks.WPF
{
    /// <summary>
    /// Interaction logic for FileDownload.xaml
    /// </summary>
    public partial class FileDownload : Window
    {
        CancellationTokenSource cts = null;
        CancellationTokenSource cts1 = null;
        const string fileName = "largefile.zip";

        public FileDownload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FileDownload_Click(object sender, RoutedEventArgs e)
        {
            fileDownload.Content = "Cancel";
            taskProgress.Visibility = Visibility.Visible;
            taskProgress.IsIndeterminate = false;
            taskProgress.Value = 0;
            taskProgress.Maximum = 100;
            logs.Text = "";

            //On clicking of Search/Cancel checking to cancel operation or perform search
            if (cts != null)
            {
                cts.Cancel();
                cts = null;
                return;
            }

            cts = new CancellationTokenSource();

            //Progress reporting
            var progress = new Progress<ProgressReport>(percent =>
            {
                taskProgress.Value = percent.progressPercentage;
                logs.Text += $"{percent.bytesToRead}/{percent.totalBytes} downloaded!!{Environment.NewLine}";
                logs.Text += $"Elapsed time - {percent.elapsedTime}ms{Environment.NewLine}";
            });
            //var progress = new Progress<ProgreesReport>();
            //progress.ProgressChanged += (s, e) =>
            //{
            //    taskProgress.Value = e.progressPercentage;
            //    logs.Text += $"{e.bytesToRead}/{e.totalBytes} downloaded!!{Environment.NewLine}";
            //    logs.Text += $"Elapsed time - {e.elapsedTime}ms{Environment.NewLine}";
            //};

            try
            {
                await DownloadLargeFilAsync("https://github.com/Ravindra-a/largefile/archive/master.zip", "largefile.zip", cts.Token, progress);
                logs.Text += $"File {fileName} downloaded successfully!!{Environment.NewLine}";
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
                taskProgress.Visibility = Visibility.Hidden;
                fileDownload.Content = "File Download";
                totalTimeTaken.Content = "Download largefile.zip completed";
            }
        }

        /// <summary>
        /// Async Download Method
        /// </summary>
        /// <param name="fileToDownload">File to dwonload</param>
        /// <param name="fileName">Name of file to write locally</param>
        /// <param name="token">Cancellation token</param>
        /// <param name="progress">Progress reporting</param>
        /// <returns></returns>
        private async Task DownloadLargeFilAsync(string fileToDownload, string fileName, CancellationToken token, IProgress<ProgressReport> progress = null)
        {
            var ticker = new Stopwatch();
            ticker.Start();
            byte[] buffer = new byte[8192];
            int bytes = 0;
            string fileToWriteTo = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileName);
            using (HttpClient client = new HttpClient())
            {

                string url = fileToDownload;
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token))
                {
                    response.EnsureSuccessStatusCode();
                    long totalLength = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : 34632982; //Once in a while github returns response without content length header 
                    //hence in that case defaulting to actual file size
                    using (Stream stream = await response.Content.ReadAsStreamAsync(), fileStreamToWrite = new FileStream(fileToWriteTo, FileMode.Create, FileAccess.Write, FileShare.None, 1024, true))
                    {
                        for (; ; )
                        {
                            int dataToRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                            if (dataToRead == 0)
                            {
                                break;
                            }
                            else
                            {
                                await fileStreamToWrite.WriteAsync(buffer, 0, dataToRead); //Writing stream to disk as and when chunk is downloaded
                                var data = new byte[dataToRead];
                                buffer.ToList().CopyTo(0, data, 0, dataToRead);
                                bytes += dataToRead;
                                if (progress != null) //For calling methods that do no want to report progress
                                {
                                    if (((bytes * 100) / totalLength) % 5 == 0) //reporting progress for every 5%
                                    {
                                        progress.Report(new ProgressReport()
                                        {
                                            progressPercentage = (bytes * 1d) / (totalLength * 1d) * 100,
                                            bytesToRead = bytes,
                                            totalBytes = totalLength,
                                            elapsedTime = ticker.ElapsedMilliseconds
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FileDownload1_Click(object sender, RoutedEventArgs e)
        {
            fileDownload1.Content = "Cancel";
            taskProgress1.Visibility = Visibility.Visible;
            taskProgress1.IsIndeterminate = false;
            taskProgress1.Value = 0;
            taskProgress1.Maximum = 100;
            logs1.Text = "";

            //On clicking of Search/Canacel checking to cancel opearation or perform search
            if (cts1 != null)
            {
                cts1.Cancel();
                cts1 = null;
                return;
            }

            cts1 = new CancellationTokenSource();

            //Progres reporting
            var progress = new Progress<ProgressReport>(percent =>
            {
                taskProgress1.Value = percent.progressPercentage;
                logs1.Text += $"{percent.bytesToRead}/{percent.totalBytes} downloaded!!{Environment.NewLine}";
                logs1.Text += $"Elapsed time - {percent.elapsedTime}ms{Environment.NewLine}";
            });

            try
            {
                await DownloadLargeFilAsync("https://github.com/Ravindra-a/largefile/archive/master.zip", "largefile1.zip", cts1.Token, progress);
                logs1.Text += $"File {fileName} downloaded successfully!!{Environment.NewLine}";
            }
            catch (OperationCanceledException ex)
            {
                logs1.Text = ex.Message;
            }
            catch (Exception ex)
            {
                logs1.Text = ex.Message;
            }
            finally
            {
                cts1 = null;
                taskProgress1.Visibility = Visibility.Hidden;
                fileDownload1.Content = "File Download";
                totalTimeTaken1.Content = "Download largefile1.zip completed";
            }
        }
    }

    public class ProgressReport
    {
        public double progressPercentage { get; set; }
        public long totalBytes { get; set; }
        public int bytesToRead { get; set; }
        public long elapsedTime { get; set; }

    }

}

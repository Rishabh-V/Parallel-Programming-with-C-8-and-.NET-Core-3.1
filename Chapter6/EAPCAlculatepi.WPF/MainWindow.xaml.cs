using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EAPCAlculatepi.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculatepi pi;
        CancellationTokenSource cts = null;
        CalculatepiCompletedEventArgs calculatepiCompletedEventArgs = null;
        public MainWindow()
        {
            InitializeComponent();
            pi = new Calculatepi();
            pi.CalculatepiCompleted += new CalculatepiCompletedEventHandler(Pi_CalculatePiCompleted);
        }

        private void Calculatepi1000_Click(object sender, RoutedEventArgs e)
        {
            if (calculatepi1000.Content.ToString().Contains("pi"))
            {
                calculatepi1000.Content = "Cancel";
                pi.CalculatepiAsync(1000, sender, "Calculate pi (1000)");
            }
            else
            {
                pi.CancelAsync("Calculate pi (1000)");
                calculatepi1000.Content = "Calculate pi (1000)";
            }
        }

        void Pi_CalculatePiCompleted(object sender, CalculatepiCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                output.Text += $"{e.UserState.ToString()} places - {e.Result}, time taken is {e.TimeTaken.ToString()} Milliseconds{Environment.NewLine}";
            }
            else
            {
                output.Text += $"{e.UserState.ToString()} places is cancelled, time taken is {e.TimeTaken.ToString()} Milliseconds{Environment.NewLine}";
            }
            ((Button)e.Sender).Content = e.UserState.ToString();
        }

        private void Calculatepi900_Click(object sender, RoutedEventArgs e)
        {
            if (calculatepi900.Content.ToString().Contains("pi"))
            {
                calculatepi900.Content = "Cancel";
                pi.CalculatepiAsync(900, sender, "Calculate pi (900)");
            }
            else
            {
                pi.CancelAsync("Calculate pi (900)");
                calculatepi900.Content = "Calculate pi (900)";
            }
        }

        private async void Calculatepi1000TAP_Click(object sender, RoutedEventArgs e)
        {
            Calculatepi calculatepi = new Calculatepi();
            calculatepi1000TAP.Content = "Cancel";
            //On clicking of Cancel checking to cancel opearation 
            if (cts != null)
            {
                cts.Cancel();
                cts = null;
                return;
            }

            cts = new CancellationTokenSource();            

            //Cancellation needs to be handled gracefully
            try
            {
                calculatepiCompletedEventArgs = await TAPWrappertoAPMAsync(calculatepi, 1000, sender, "Calculate pi (1000) TAP", cts.Token);
                output.Text += $"{calculatepiCompletedEventArgs.UserState.ToString()} - {calculatepiCompletedEventArgs.Result}, time taken is {calculatepiCompletedEventArgs.TimeTaken.ToString()} Milliseconds{Environment.NewLine}";
            }
            catch (OperationCanceledException)
            {
                output.Text += $"Calculate pi(1000) TAP is cancelled";
            }
            finally
            {
                cts = null;
                calculatepiCompletedEventArgs = null;
            }

            calculatepi1000TAP.Content = "Calculate pi(1000) TAP";
        }

        private Task<CalculatepiCompletedEventArgs> TAPWrappertoAPMAsync(Calculatepi calculatepi, int numsteps, object sender, object operationState, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<CalculatepiCompletedEventArgs>();
            //Delegate on cancellation token when there is a cancellation, executes on calling thread's context in this case UI
            token.Register(() =>
            {
                calculatepi.CancelAsync(operationState);
            });
            calculatepi.CalculatepiCompleted += (_, e) =>
            {
                if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                    return;
                }
                else if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                    return;
                }
                else
                    tcs.TrySetResult(e);
            };

            // Register for the event and *then* start the operation.
            calculatepi.CalculatepiAsync(numsteps, sender, operationState);
            return tcs.Task;
        }
    }
}

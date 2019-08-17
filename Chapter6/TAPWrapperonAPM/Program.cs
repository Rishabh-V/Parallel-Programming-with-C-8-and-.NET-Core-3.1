using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAPWrapperonAPM
{
    class Program
    {
        static Byte[] bytes = new Byte[8196];
        static CancellationTokenSource cts = new CancellationTokenSource();
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Managed Thread Id in Main is : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.            
            Stopwatch watch = new Stopwatch();
            watch.Start();
            FileStream fs = new FileStream(@"../../../TextFile.txt", FileMode.Open, FileAccess.Read, FileShare.Read, bytes.Length, true);
            Console.Write("Enter wait time in seconds before cancelling operation ");
            int waitTime = Convert.ToInt32(Console.ReadLine());
            cts.CancelAfter(waitTime * 1000);
            int numBytesRead = 0;
            try
            {
                numBytesRead = await ReadAsyncAPMWrapper(fs, bytes, 0, bytes.Length, cts.Token);
                Console.WriteLine("Operation completed");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Operation cancelled - {ex.Message}");
            }
            finally
            {
                cts = null;
                fs.Close();
                Console.WriteLine($"Number of bytes - {numBytesRead}");
                Console.WriteLine($"File contents - {Encoding.Default.GetString(bytes)}");
            }
            Console.ReadKey();
        }

        static Task<int> ReadAsync(FileStream fs, byte[] buffer, int offset, int count)
        {
            return Task<int>.Factory.FromAsync(fs.BeginRead, fs.EndRead, buffer, offset, count, null);
        }

        /// <summary>
        /// TAP Wrapper over BeginRead and EndRead of FileStream
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        static Task<int> ReadAsyncAPMWrapper(FileStream fs, byte[] buffer, int offset, int count, CancellationToken token)
        {
            var taskCompletionSource = new TaskCompletionSource<int>();
            //Registering cancellation token, although this is not a elegant way to cancel as it doesn't handle IO resource cleanly.
            // also this doesn't stop beginread 
            token.Register(() => taskCompletionSource.TrySetCanceled());
            fs.BeginRead(buffer, offset, count, iAsyncResult =>
            {
                try
                {
                    Thread.Sleep(5000); //If user input has wait more than this complete operation else cancel.
                    if (token.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }
                    var state = iAsyncResult.AsyncState as FileStream;
                    var read = state.EndRead(iAsyncResult);
                    taskCompletionSource.TrySetResult(read);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }
            }, fs);
            return taskCompletionSource.Task;
        }
    }
}

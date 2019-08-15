using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace APMFileRead
{
    class Program
    {
        static Byte[] bytes = new Byte[100];
        static void Main(string[] args)
        {
            Console.WriteLine($"Managed Thread Id in Main is : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.            
            Stopwatch watch = new Stopwatch();
            watch.Start();
            FileStream fs = new FileStream(@"../../../TextFile.txt", FileMode.Open, FileAccess.Read, FileShare.Read, bytes.Length, FileOptions.Asynchronous);
            Console.WriteLine($"Begin reading file, Elapsed time - {watch.ElapsedMilliseconds}");
            #region Async using APM
            //IAsyncResult result = fs.BeginRead(bytes, 0, bytes.Length, null, null);
            //while (!result.IsCompleted) // Proceeding with doing some other operation while file is being read
            //{
            //    Console.WriteLine($"Do something else in main method while reading file, Elapsed time - {watch.ElapsedMilliseconds}");
            //}
            //int numBytesRead = fs.EndRead(result);
            //Console.WriteLine($"End reading file, Number of bytes - {numBytesRead}, Elapsed time - {watch.ElapsedMilliseconds}");
            #endregion

            #region Sync
            //int numBytesRead = fs.Read(bytes, 0, bytes.Length);
            //Console.WriteLine($"Do something else in main method while reading file, Elapsed time - {watch.ElapsedMilliseconds}");
            //Console.WriteLine($"End reading file, Number of bytes - {numBytesRead}, Elapsed time - {watch.ElapsedMilliseconds}");
            #endregion

            //fs.Close();
            //Console.WriteLine($"File contents - {Encoding.Default.GetString(bytes)}");
            watch.Stop();
            #region Async using APM, callback
            fs.BeginRead(bytes, 0, bytes.Length, EndRead, fs);
            Console.WriteLine($"Do something else in main method while reading file, Elapsed time - {watch.ElapsedMilliseconds}");
            #endregion region
           
            
            
            Console.ReadKey();


            //fs.BeginRead(bytes, 0, bytes.Length, EndRead, fs);
        }

        /// <summary>
        /// Callback method
        /// </summary>
        /// <param name="asyncResult"></param>
        private static void EndRead(IAsyncResult asyncResult)
        {
            Console.WriteLine($"Managed Thread Id in endread is : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.
            FileStream fs = (FileStream)asyncResult.AsyncState;
            Int32 numBytesRead = fs.EndRead(asyncResult);
            Console.WriteLine($"Number of bytes - {numBytesRead}");
            Console.WriteLine(Encoding.UTF8.GetString(bytes));
            fs.Close();
        }
    }
}

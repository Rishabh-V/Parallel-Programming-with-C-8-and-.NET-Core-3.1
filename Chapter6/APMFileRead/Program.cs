using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace APMFileRead
{
    class Program
    {
        static IAsyncResult m_ar;
        static void Main(string[] args)
        {
            Console.WriteLine($"Managed Thread Id in Main is : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.
            ReadFile(@"../../../largefile");
            Console.WriteLine("Do sync operation in main method");
            Console.ReadKey();
        }
        private static void ReadFile(String fileName)
        {
            Stream stream;
            Byte[] bytes = new Byte[100];

            Stream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 100, FileOptions.Asynchronous);
            stream = fs;
            m_ar = fs.BeginRead(bytes, 0, 100, EndRead, stream);
            Console.WriteLine($"Managed Thread Id in constructor is : {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Do something else in ReadMultipleFiles method");
        }

        private static void EndRead(IAsyncResult ar)
        {
            Console.WriteLine($"Managed Thread Id in endread is : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.
            Stream mstream = (Stream)ar.AsyncState;
            Int32 numBytesRead = mstream.EndRead(m_ar);
            mstream.Close();
        }
    }
}

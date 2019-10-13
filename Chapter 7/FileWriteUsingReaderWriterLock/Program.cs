using System;
using System.Threading.Tasks;

namespace FileWriteUsingReaderWriterLock
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Writing file to disk");
            FileWrite fileupload = new FileWrite();
            await fileupload.PerformFileOperation();
            Console.WriteLine("Writing file to disk completed");
            Console.Read();
        }
    }
}

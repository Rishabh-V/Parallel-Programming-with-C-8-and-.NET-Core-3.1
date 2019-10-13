using System;
using System.Threading.Tasks;

namespace FileUploadMutex
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Writing file to disk");
            FileUpload fileupload = new FileUpload();
            await fileupload.CreateorUpdateFiles();
            Console.WriteLine("Writing file to disk completed");
            Console.Read();

        }
    }
}

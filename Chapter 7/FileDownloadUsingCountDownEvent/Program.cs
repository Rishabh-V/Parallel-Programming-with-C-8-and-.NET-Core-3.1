using System;
using System.Threading;

namespace FileDownloadUsingCountDownEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcom to file downloader, please enter number of parallel threads file download needs to occur");
            int numberOFThreadsProcessingFileDownload = Convert.ToInt32(Console.ReadLine());
            FileDownloadSimulator fileDownloadSimulator = new FileDownloadSimulator(numberOFThreadsProcessingFileDownload);
            for (int i = 0; i < numberOFThreadsProcessingFileDownload; i++)
            {
                int captured = i;
                Thread t = new Thread(() => fileDownloadSimulator.SimulateFileDownload(captured));
                t.Start();
            }
            Console.WriteLine(fileDownloadSimulator.FileMerge());
            Console.ReadLine();

        }
    }
}

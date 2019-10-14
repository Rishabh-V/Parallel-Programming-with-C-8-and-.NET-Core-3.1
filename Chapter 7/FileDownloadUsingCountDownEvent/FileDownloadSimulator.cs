using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FileDownloadUsingCountDownEvent
{
    public class FileDownloadSimulator
    {
        CountdownEvent fileManager;
        ConcurrentDictionary<int, string> tempFileResponse = new ConcurrentDictionary<int, string>();
        public FileDownloadSimulator(int numberOFThreadsProcessingFileDownload)
        {
            fileManager = new CountdownEvent(numberOFThreadsProcessingFileDownload);
        }

        public void SimulateFileDownload(int threadID)
        {
            Thread.Sleep(200);
            tempFileResponse.TryAdd(threadID, $"Line {threadID + 1} of file.\t");
            Console.WriteLine($"Finished processing {threadID}");
            fileManager.Signal();
        }

        public string FileMerge()
        {
            fileManager.Wait();
            Console.WriteLine("Finished Processing,priting contents");
            StringBuilder fileContents = new StringBuilder();
            for (int i = 0; i < tempFileResponse.Count; i++)
            {
                string output;
                tempFileResponse.TryGetValue(i, out output);
                fileContents.Append(output);
            }
            return fileContents.ToString();
        }

    }
}

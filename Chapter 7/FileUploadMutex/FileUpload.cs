using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploadMutex
{
    public class FileUpload
    {
        private async Task WriteTextAsync(string fileName)
        {
            string text = $"Mutex is just like lock (full form mutually exclusive lock), however scope of locking spawns across processes i.e. " +
    "if multiple instances of same process running mutex can be used to execute a code block by a single thread across processes.";
            byte[] encoding = Encoding.Unicode.GetBytes(text);
            await Task.Delay(1);
            using (var mutex = new Mutex(false, fileName))
            {
                mutex.WaitOne();
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 64, useAsync: true))
                {
                    fs.Write(encoding, 0, encoding.Length);
                }
                mutex.ReleaseMutex();
            }

        }

        public async Task CreateorUpdateFiles()
        {
            var tasks = new Task[50];
            for (int i = 1; i <= tasks.Length; i++)
            {
                tasks[i - 1] = WriteTextAsync($"File{i % 5}.txt");
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            await Task.WhenAll(tasks);
            Console.WriteLine($"Time ellapsed {timer.ElapsedMilliseconds}");
        }


    }
}

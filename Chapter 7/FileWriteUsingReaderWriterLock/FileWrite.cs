using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWriteUsingReaderWriterLock
{
    public class FileWrite
    {
        Stopwatch timer; //To compare performance with Monitor
        public FileWrite()
        {
            timer = new Stopwatch();
            timer.Start();
        }
        const string fileName = "SampleReadLock.txt";
        object locker = new object();
        ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();

        private void ReadFile()
        {
            if (File.Exists(fileName))
            {
                readerWriterLockSlim.EnterReadLock();
                //lock (locker)
                //{
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 2048, useAsync: true))
                {
                    using (System.IO.StreamReader rdr = new System.IO.StreamReader(fs))
                    {
                        Thread.Sleep(500); //Used to perform timer calculation,
                        Console.WriteLine(rdr.ReadToEnd());
                    }
                }
                //}
                readerWriterLockSlim.ExitReadLock();
            }
        }

        private void WriteFile(int lineNumber)
        {
            //lock (locker)
            //{
            readerWriterLockSlim.EnterWriteLock();
            string text = $"Line {lineNumber} ReadWriteLock" + Environment.NewLine;
            byte[] encoding = Encoding.ASCII.GetBytes(text);
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Write, 2048, useAsync: true))
            {
                fs.Write(encoding, 0, encoding.Length);
            }
            readerWriterLockSlim.ExitWriteLock();
            //}
        }

        private void ReadorUpdateFile()
        {
            string fileContent = String.Empty;
            if (File.Exists(fileName))
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();
                //First read the contents and if specific content exists then print on console else write into file
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 2048, useAsync: true))
                {
                    using (System.IO.StreamReader rdr = new System.IO.StreamReader(fs))
                    {
                        fileContent = rdr.ReadToEnd();
                    }
                }
                if (!(fileContent.Contains("Line 15")))
                {
                    readerWriterLockSlim.EnterWriteLock();
                    using (FileStream fswrite = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Write, 2048, useAsync: true))
                    {
                        byte[] encoding = Encoding.ASCII.GetBytes($"Line 15 ReadWriteLock" + Environment.NewLine);
                        fswrite.Write(encoding, 0, encoding.Length);
                    }
                    readerWriterLockSlim.ExitWriteLock();
                }
                else
                {
                    Thread.Sleep(500); //Used to perform timer calculation,
                    Console.WriteLine(fileContent);
                }
                readerWriterLockSlim.ExitUpgradeableReadLock();
            }
        }

        public async Task PerformFileOperation()
        {
            var tasks = new Task[31];
            for (int i = 0; i < tasks.Length; i++)
            {
                if (i % 10 == 0) //Calling write every tenth time
                {
                    tasks[i] = Task.Run(() => WriteFile(i + 1));
                    Thread.Sleep(1000); //Used to perform timer calculation
                }
                else if (i == 15 || i == 21) //Calling upsert twice
                {
                    tasks[i] = Task.Run(() => ReadorUpdateFile());
                }
                else //Calling read most of the time
                {
                    tasks[i] = Task.Run(() => ReadFile());
                }
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Time ellapsed {timer.ElapsedMilliseconds}"); //Displaying time taken for execution
            readerWriterLockSlim.Dispose();
        }


    }

}

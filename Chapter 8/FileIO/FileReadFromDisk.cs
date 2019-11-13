using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace FileIO
{

    public class FileReadFromDisk
    {
        private readonly IFileReader _streamReader;
        
        //Thread safe collection to store exceptions occurred during parallel processing
        ConcurrentBag<Exception> errors = new ConcurrentBag<Exception>();

        public FileReadFromDisk(IFileReader streamReader)
        {
            this._streamReader = streamReader;
        }


        public List<Employee> ReadFileandProcessTask(string filePath, int bonusAmountRule)
        {
            #region Create file
            //if (!File.Exists(filePath))
            //{
            //    Random rand = new Random();
            //    using (FileStream fs = new FileStream(filePath, FileMode.Create))
            //    {
            //        using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
            //        {
            //            for (int i = 1; i <= 1000000; i++)
            //            {
            //                sw.Write($"{i}\tEmployee id -{rand.Next()}\t Bonus -{rand.Next() / 10000} {Environment.NewLine}");
            //            }
            //        }
            //    }
            //}
            #endregion


            // Using ConcurrentBag for thread safety
            var employeeDetails = new ConcurrentBag<Employee>();

            // Blocking collection so that multiple consumers do not end up corrupting data
            var employeeData = new BlockingCollection<string>();
            // Single Producer
            var readLines = ReadDataFromFile(filePath, employeeData);

            // Multiple Consumers
            var processLines = SerializeEmployeeData(employeeData, employeeDetails);

            Task.WaitAll(readLines, processLines);

            // Throw the exceptions here after the loop completes.
            if (errors.Count > 0)
            {
                throw new AggregateException(errors);
            }

            //Business logic - Get all users with bonus greater than 50000
            return employeeDetails.Where(x => x.Bonus >= bonusAmountRule).ToList();
        }

        public Task ReadDataFromFile(string filePath, BlockingCollection<string> employeeData)
        {
            return Task.Factory.StartNew(() =>
            {
                using (StreamReader sr = this._streamReader.GetFileReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        employeeData.Add(sr.ReadLine());
                    }
                }

                // Notify consumers that addition is completed
                employeeData.CompleteAdding();
            });
        }

        private Task SerializeEmployeeData(BlockingCollection<string> employeeData, ConcurrentBag<Employee> employeeDetails)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    Parallel.ForEach(employeeData.GetConsumingEnumerable(), line =>
                    {
                        string[] lineFields = line.Split('\t');
                        int employeeID, bonus;
                        int.TryParse(lineFields[1].Substring(lineFields[1].IndexOf('-') + 1), out employeeID);
                        int.TryParse(lineFields[2].Substring(lineFields[2].IndexOf('-') + 1), out bonus);
                        employeeDetails.Add(new Employee { EmployeeID = employeeID, Bonus = bonus });
                    });

                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            });
        }



    }
}

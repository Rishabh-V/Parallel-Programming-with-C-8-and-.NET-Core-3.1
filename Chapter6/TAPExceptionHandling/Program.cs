using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Data;

namespace TAPExceptionHandling
{
    class Program
    {
        //static async Task Main(string[] args)
        static void Main(string[] args)
        {
            #region Single exception handling
            //var task = GetDataAsync();
            //try
            //{
            //    var data = await task;
            //    Console.WriteLine(data);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception occured in GetDataAsync method - {ex.Message} \n Innerstack \n {ex.StackTrace}");
            //    List<String> errors = task.Exception.Flatten().InnerExceptions.Select(x => x.Message).ToList();
            //    int counter = 0;
            //    foreach (string error in errors)
            //    {
            //        counter++;
            //        Console.WriteLine($"{counter}).Error - {error}");
            //    }
            //}
            #endregion

            #region Multiple task exception handling
            //var taskfromAPI = GetDataAsyncNested();
            //var taskFromFile = GetDataAsyncFromAnotherSource();
            //var tasks = new List<Task<string>>();
            //tasks.Add(taskfromAPI);
            //tasks.Add(taskFromFile);
            //var allTasks = Task.WhenAll(tasks);
            //try
            //{
            //    await allTasks;
            //}
            //catch
            //{
            //    List<Tuple<string, string>> errors = allTasks.Exception.Flatten().InnerExceptions.Select(x => new Tuple<string,string>(x.Message, x.StackTrace)).ToList();
            //    int counter = 0;
            //    foreach (Tuple<string, string> error in errors)
            //    {
            //        counter++;
            //        Console.WriteLine($"{counter}).Error - {error.Item1} \n Innerstack \n {error.Item2} \n");
            //    }
            //}

            #endregion

            #region Exception handling for async performed without await (Task.Wait())
            var tasks = new List<Task>();
            var task = GetDataAsync();
            tasks.Add(task);
            var task2 = Task.Run(() => DoHighCPUIntense());
            tasks.Add(task2);
            try
            {
                Task.WhenAll(tasks).Wait();
            }
            catch (AggregateException agEx)
            {
                List<Tuple<string, string>> errors = agEx.Flatten().InnerExceptions.Select(x => new Tuple<string, string>(x.Message, x.StackTrace)).ToList();
                int counter = 0;
                foreach (Tuple<string, string> error in errors)
                {
                    counter++;
                    Console.WriteLine($"{counter}).Error - {error.Item1} \n Innerstack \n {error.Item2} \n");
                }
            }
            #endregion

            Console.Read();
        }

        /// <summary>
        /// Async method doing high CPU operation
        /// </summary>
        /// <returns></returns>
        private static string DoHighCPUIntense()
        {
            String location = @"C:\";

            Task<string> output = Task.Run(() =>
                {
                    List<string> files = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        files.AddRange(Directory.GetFiles(location, "*.txt", SearchOption.AllDirectories).ToList());
                    }
                    return files.FirstOrDefault();
                });
            try
            {
                output.Wait();
            }
            catch (AggregateException agEx)
            {
                //Further handle method can be used to do specific action based on the type of exception
                agEx.Handle(x =>
                {
                    if (x is UnauthorizedAccessException)
                    {
                        Console.WriteLine("Specific action for UnauthorizedAccessException");
                    }
                    return true;
                });
            }
            return string.Empty;
        }

        /// <summary>
        /// Async method to retrieve data from API
        /// </summary>
        /// <returns></returns>
        static async Task<string> GetDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://localhost:44394/api"); // Giving a non existing API method to generate exception
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Data retrieved from API");
                    return content;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Dummy nested method
        /// </summary>
        /// <returns></returns>
        static async Task<String> GetDataAsyncNested()
        {
            return await GetDataAsync();
        }

        /// <summary>
        /// Async method to retrieve data from API
        /// </summary>
        /// <returns></returns>
        static async Task<string> GetDataAsyncFromAnotherSource()
        {
            try
            {
                using (var stream = new StreamReader(File.OpenRead(@"nonexistingfile.txt")))
                {
                    var fileText = await stream.ReadToEndAsync();
                    Console.WriteLine("Reading from file completed");
                    return fileText;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

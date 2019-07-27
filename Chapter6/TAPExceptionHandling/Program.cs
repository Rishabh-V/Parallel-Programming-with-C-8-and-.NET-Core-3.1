using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace TAPExceptionHandling
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Single exception handling
            //var task = GetDataAsync();
            //try
            //{
            //    //var data = await task;
            //    //Console.WriteLine(data);
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine($"Exception occured in GetDataAsync method - {ex.Message} \n Innerstack \n {ex.StackTrace}");
            //    List<String> errors = task.Exception.Flatten().InnerExceptions.Select(x => x.Message).ToList();
            //    int counter = 0;
            //    foreach (string error in errors)
            //    {
            //        counter++;
            //        Console.WriteLine($"{counter}).Error - {error}");
            //    }
            //}
            #endregion

            //#region Multiple task exception handling
            var taskfromAPI = GetDataAsyncNested();
            var taskFromFile = GetDataAsyncFromAnotherSource();
            var tasks = new List<Task<string>>();
            tasks.Add(taskfromAPI);
            tasks.Add(taskFromFile);
            var allTasks = Task.WhenAll(tasks);
            try
            {
                await allTasks;
            }
            catch
            {
                List<String> errors = allTasks.Exception.Flatten().InnerExceptions.Select(x => x.Message).ToList();
                int counter = 0;
                foreach (string error in errors)
                {
                    counter++;
                    Console.WriteLine($"{counter}).Error - {error}");
                }
            }

            //#endregion

            Console.Read();
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

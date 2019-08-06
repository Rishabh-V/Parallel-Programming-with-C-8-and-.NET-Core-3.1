using System;
using System.Threading.Tasks;

namespace TaskWithResult
{
    /// <summary>
    /// Demonstrates the task with result.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int number = 1000;
            Task<int> task = new Task<int>(SumOfNumbers, number);
            task.Start();
            task.Wait();
            Console.WriteLine($"The sum of first {number} is {task.Result}");
            Console.ReadLine();
        }

        /// <summary>
        /// Calculates the sum of numbers from 0 to the specified number.
        /// </summary>
        /// <param name="numberTo">The number till which sum needs to be calculated.</param>
        /// <returns></returns>
        static int SumOfNumbers(object numberTo)
        {
            var num = (int)numberTo;
            int sum = 0;
            if (num > 0)
            {
                for (int i = 0; i <= num; i++)
                {
                    sum += i;
                }
            }

            return sum;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace CreateWaterUsingSemaphore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Water water = new Water();
            while (true)
            {
                Console.WriteLine("Please enter sequence of Hydrogen and Oxygen molecules or e to Exit");
                string input = Console.ReadLine();
                if (input == "e")
                    break;
                await water.BuildWater(input);
            }
        }

    }
}

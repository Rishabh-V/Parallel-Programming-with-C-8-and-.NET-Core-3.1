using System;

namespace EAPCalculatepi
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculatepi pi = new Calculatepi();
            pi.CalculatepiCompleted += new CalculatepiCompletedEventHandler(Pi_CalculatePrimeCompleted);
            Console.WriteLine("Calculating pi to 1000 places started");
            pi.CalculatepiAsync(1000, 1000);
            Console.WriteLine("Calculating pi to 900 places started");
            pi.CalculatepiAsync(900, 900);
            Console.WriteLine("do something else");
            Console.ReadKey();
        }

        static void Pi_CalculatePrimeCompleted(object sender, CalculatepiCompletedEventArgs e)
        {
            Console.WriteLine($"Calculated pi to {e.UserState.ToString()} places - {e.Result}, time taken is {e.TimeTaken.ToString()} Milliseconds");
        }
    }
}

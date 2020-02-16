using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calculator
{
    public class MathClass
    {
        public int Divide(int numerator, int? denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
            else if (!denominator.HasValue)
            {
                throw new ArgumentNullException(); 
            }
            else
            {
                //return numerator / denominator;
                int remainder;
                return Math.DivRem(numerator, denominator.Value, out remainder);
            }
        }

        public async Task<int> DivideAsync(int numerator, int? denominator)
        {
            try
            {
                var t = Task.Run(() =>
                {
                    return Divide(numerator, denominator);
                });
                return await t;
                //return await t.ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
    }
}

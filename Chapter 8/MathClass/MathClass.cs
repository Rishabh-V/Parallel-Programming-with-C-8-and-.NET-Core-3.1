using System;
using System.Threading.Tasks;

namespace Calculator
{
    public class MathClass
    {
        public int Divide(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
            else
            {
                //return numerator / denominator;
                int remainder;
                return Math.DivRem(numerator, denominator, out remainder);
            }
        }

        public async Task<int> DivideAsync(int numerator, int denominator)
        {
            var t = Task.Run(() =>
            {
                return Divide(numerator, denominator);
            });
            return await t;
        }


    }
}

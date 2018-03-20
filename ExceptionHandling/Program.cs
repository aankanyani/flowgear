using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plexware.Assessment.ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(Divide(4.5m, 0m));
                Console.ReadKey();
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static Decimal Divide(Decimal numerator, Decimal denominator)
        {
            return numerator / denominator;
        }
    }
}

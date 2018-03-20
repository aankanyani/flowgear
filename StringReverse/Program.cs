using System;

namespace Plexware.Assessment.StringReverse
{
    class Program
    {
        static void Main(string[] args)
        {
            String value = "The quick brown fox jumped over the lazy dog's tail";
            string output = null;

            for (int i = value.Length - 1; i >= 0; i--)
            {
                output += value[i];
            }

            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}

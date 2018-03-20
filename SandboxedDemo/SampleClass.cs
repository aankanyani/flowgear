using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandboxedDemo
{
    public class SampleClass
    {
        public void TestMethod(bool trySomethingIllegal)
        {
            Console.WriteLine("In TestMethod");

            if (trySomethingIllegal)
            {
                System.IO.File.ReadAllBytes("d:\\temp\\file.txt");

                throw new Exception("Oops - looks like we were allowed to perform this action!");
            }
        }
    }
}

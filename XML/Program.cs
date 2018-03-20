using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace Plexware.Assessment.XML
{
    class Program
    {
        static void Main(string[] args)
        {

            XDocument xdoc = XDocument.Load(@"..\..\Question.xml");
            decimal sum = 0;
            foreach (XElement element in xdoc.Descendants())
            {
                if (element.LastAttribute != null)
                {
                    Console.WriteLine(decimal.Parse(element.LastAttribute.Value, CultureInfo.InvariantCulture));
                    sum += decimal.Parse(element.LastAttribute.Value, CultureInfo.InvariantCulture);
                }
            }
            Console.WriteLine("sum is: " + sum);
        }
    }
}

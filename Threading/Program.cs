using System;
using System.Threading;

namespace Plexware.Assessment.Threading
{
    class Program
    {
        
        private const int minSleep = 0;
        private const int maxSleep = 500;
        private const int numThreads = 4;
        private const int numIterations = 2;
        private static int threadsAccessingResource = 0;
        private static Mutex mut = new Mutex();

        static void Main()
        {
            // Create the threads that will use the protected resource.
            for (int i = 0; i < numThreads; i++)
            {
                Thread myThread = new Thread(new ThreadStart(RunThread));
                myThread.Name = String.Format("Thread{0}", i + 1);
                myThread.Start();
            }

            Console.ReadKey();
        }

        //Run threads
        private static void RunThread()
        {
            for (int i = 0; i < numIterations; i++)
            {
                mut.WaitOne();
                UseResource();
                mut.ReleaseMutex();
            }
        }


        // This method represents a resource that must be synchronized
        // so that only one thread at a time can enter.
        private static void UseResource()
        {
            threadsAccessingResource++;

            if (threadsAccessingResource > 1)
                throw new ApplicationException("Multiple threads are accessing resource simultaneously!");

            Console.WriteLine("{0} has entered the protected area",
                Thread.CurrentThread.Name);

            // Simulate some work.
            Thread.Sleep(new Random().Next(minSleep, maxSleep));

            Console.WriteLine("{0} is leaving the protected area\r\n",
                Thread.CurrentThread.Name);

            threadsAccessingResource--;
        }
    }
}
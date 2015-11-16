using System;
using System.Threading;
using System.Threading.Tasks;

namespace UnobservedTaskException
{
    class Program
    {
        /// <summary>
        /// This code demonstrates how to handle unobserved exceptions 
        /// which thrown when task gets collected/finilized by the GC
        /// </summary>
        /// <param name="arguments"></param>
        static void Main(string[] arguments)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Console.WriteLine("Exception from " + s.GetType());
                Console.WriteLine(e.ToString());
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Console.WriteLine("Unobserved Task exception");
                Console.WriteLine(e.Exception.ToString());
                //if uncommented, AppDomain exception will not be thrown
                //e.SetObserved();  
            };

            Task task1 = new Task(() =>
            {
                Console.WriteLine("Task1 started");
                throw new ArgumentException("Task1 arguments");
            });

            Task task2 = new Task(() =>
            {
                Console.WriteLine("Task2 started");
                throw new ArgumentException("Task2 arguments");
            });

            try
            {
                task1.Start();
                task2.Start();

                task2.Wait(); //throws task2 exception 
                // task1 exception still unobserved
            }
            catch (Exception e)
            {
                Console.WriteLine("Main() Exception");
                Console.WriteLine(e.ToString());
            }

            Thread.Sleep(1000);
            // if commented, GC will not collect task object -> unobserved exception will not be thrown
            task1 = null;
            task2 = null; 

            Console.WriteLine("Collect");

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("Waiting...");
            Console.ReadKey();
        }
    }
}

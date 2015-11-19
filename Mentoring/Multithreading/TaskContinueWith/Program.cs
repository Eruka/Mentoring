using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinueWith
{
    class Program
    {
        /// <summary>
        /// This code demonstrates work of continuation tasks with different options
        /// and shows one of approaches of handling exceptions via extention method 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //demonstration of separating of continuation tasks depending on the result of root task execution
            // via TaskContinuationOptions enumeration
            Task task0 = CreateTaskWithContinuationTaskWithChekingRootTask();
            task0.Start();

            //demonstration of separating of continuation tasks depending on the result of root task execution
            //via cheking state of root task
            Task task1 = CreateTaskWithContinuationTaskOnFaultedAndOnCompleted();
            task1.Start();

            //demonstration of difference between expectations of the root task and continuation task
            Task task2 = CreateTaskWithContinuationTaskWithDelay();
            task2.Start();
            task2.Wait(); // will wait for competion of the root task only

            Console.WriteLine("Main thread : Continuation task for t2 still is not completed");

            Task rootTask = new Task(() =>
            {
                Console.WriteLine("t3: Root task completed");
            });

            Task task3 = CreateContinuationTaskWithDelay(rootTask);

            rootTask.Start();
            task3.Wait(); // will wait for competion of the continuation task too

            Console.WriteLine("Main thread : Root task and continuation tasks t3 are completed");

            //demonstration work of continuation tasks with different options
            //todo: research behavior with mentor
            Task task4 = CreateTaskWithMultipleContinuationTasks();
            task4.Start();

            //todo:
            //demonstration of handling exceptions via extention method
            //demonstration of cancellation tasks
            //investigate unwrap

            Console.ReadKey();
        }

        private static Task CreateTaskWithContinuationTaskWithChekingRootTask()
        {
            Task task = new Task(() =>
            {
                Console.WriteLine("t0: Root task started");
                Thread.Sleep(1000);
                Console.WriteLine("t0: Root task completed");
            });

            task.ContinueWith((t) =>
            {
                if (t.IsCanceled)
                {
                    Console.WriteLine("t0: Continuation task: Root task cancelled");
                }
                else if (t.IsFaulted)
                {
                    Console.WriteLine("t0: Continuation task: Root task faulted");
                }
                else
                    Console.WriteLine("t0: Continuation task: Root task completed");

            });

            return task;
        }

        private static Task CreateTaskWithContinuationTaskOnFaultedAndOnCompleted()
        {
            Task task = new Task(() =>
            {
                Console.WriteLine("t1: Root task started");
                throw new ArgumentException("t1: Root task Exception");
            });

            task.ContinueWith(t =>
            {
                Console.WriteLine("t1: Continuation task: Root task faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            task.ContinueWith(t =>
            {
                Console.WriteLine("t1: Continuation task: Root task completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return task;
        }

        private static Task CreateTaskWithContinuationTaskWithDelay()
        {
            Task task = new Task(() =>
            {
                Console.WriteLine("t2: Root Task started");
                Thread.Sleep(1000);
                Console.WriteLine("t2: Root Task completed");
            });

            task.ContinueWith(t =>
            {
                Console.WriteLine("t2: Continuation Task started");
                Thread.Sleep(1000);
                Console.WriteLine("t2: Continuation Task completed");
            });

            return task;
        }

        private static Task CreateContinuationTaskWithDelay(Task rootTask)
        {
            Task continuationTask = rootTask.ContinueWith(t =>
            {
                Console.WriteLine("t3: Continuation Task started");
                Thread.Sleep(1000);
                Console.WriteLine("t3: Continuation Task completed");
            });

            return continuationTask;
        }

        private static Task CreateTaskWithMultipleContinuationTasks()
        {
            Task task = new Task(() =>
            {
                Console.WriteLine("MultipleContinuation: Root task started");
            });

            task.ContinueWith(t =>
            {
                Console.WriteLine("MultipleContinuation: task1 started");
                Thread.Sleep(2000);
                Console.WriteLine("MultipleContinuation: task1 ended");
            })
            .ContinueWith(t =>
            {
                Console.WriteLine("MultipleContinuation: task2 started");
                throw new ArgumentException("MultipleContinuation: task2 exception");
                Console.WriteLine("MultipleContinuation: task2 ended");
            })
            // will be skipped
            .ContinueWith(t =>
            {
                Console.WriteLine("MultipleContinuation: task3 started");
                Thread.Sleep(1000);
                Console.WriteLine("MultipleContinuation: task3 ended");
            }, TaskContinuationOptions.OnlyOnRanToCompletion)
            // will be executed (why?)
            .ContinueWith(t =>
            {
                Console.WriteLine("MultipleContinuation: task4 started");
                Thread.Sleep(100);
                Console.WriteLine("MultipleContinuation: task4 ended");
            });
            return task;
        }

    }
}

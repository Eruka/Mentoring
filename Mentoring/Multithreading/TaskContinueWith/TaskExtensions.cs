using System;
using System.Linq;
using System.Threading.Tasks;

namespace TaskContinueWith
{
    public static class TaskExtensions
    {

        public static void Finally(this Task task, Action finalAction,
                                   TaskScheduler scheduler = null)
        {
            if (finalAction == null)
                throw new ArgumentNullException("finalAction");

            task.ContinueWith(t => finalAction(), scheduler ?? TaskScheduler.Default);
        }

        public static Task Catch<TException>(this Task task, Action<TException> exceptionHandler,
                                             TaskScheduler scheduler = null) where TException : Exception
        {
            if (exceptionHandler == null)
                throw new ArgumentNullException("exceptionHandler");

            task.ContinueWith(t =>
            {
                if (t.IsCanceled || !t.IsFaulted || t.Exception == null)
                    return;

                var exception =
                    t.Exception.Flatten().InnerExceptions.FirstOrDefault() ?? t.Exception;

                if (exception is TException)
                {
                    exceptionHandler((TException)exception);
                }
            }, scheduler ?? TaskScheduler.Default);

            return task;
        }
    }
}

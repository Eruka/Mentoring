using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskCancellationTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var task = Task.Run(() =>
            {
                Thread.Sleep(300);

                // throwing exception without cancellationToken -> task.Status is Faulted instead Cancelled
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();

            }, cancellationToken);

            Thread.Sleep(200);

            cts.Cancel();

            Thread.Sleep(200);

            Assert.AreEqual(TaskStatus.Faulted, task.Status);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var task = Task.Run(() =>
            {
                while (true)
                {
                    // throwing exception without cancellationToken -> task.Status is Faulted instead Cancelled
                    // but status is Cancelled (why?)
                    if (cancellationToken.IsCancellationRequested)
                        throw new OperationCanceledException();
                }

            }, cancellationToken);

            Thread.Sleep(200);

            cts.Cancel();

            Thread.Sleep(200);

            Assert.AreEqual(TaskStatus.Faulted, task.Status);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var task = Task.Run(() =>
            {
                Thread.Sleep(500);

                // throwing exception with cancellationToken -> task.Status is Cancelled -> Ok
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException(cancellationToken);

            }, cancellationToken);

            Thread.Sleep(200);

            cts.Cancel();

            Thread.Sleep(400);

            Assert.AreEqual(TaskStatus.Canceled, task.Status);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var task = Task.Run(() =>
            {
                // throwing exception when cancellationToken is not cancelled -> task.Status is Faulted instead Cancelled
                if (!cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException(cancellationToken);

            }, cancellationToken);

            Thread.Sleep(100);

            Assert.AreEqual(TaskStatus.Faulted, task.Status);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var cts2 = new CancellationTokenSource();
            var cancellationToken2 = cts2.Token;

            var task = Task.Run(() =>
            {
                Thread.Sleep(500);

                // throwing exception with another's cancellationToken -> task.Status is Faulted instead Cancelled
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException(cancellationToken2);

            }, cancellationToken);

            Thread.Sleep(200);

            cts.Cancel();

            Thread.Sleep(400);

            Assert.AreEqual(TaskStatus.Faulted, task.Status);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var task = Task.Run(() =>
            {
                Thread.Sleep(500);

                // task cancellation via ThrowIfCancellationRequested method -> status is Cancelled if cancellationToken was cancelled
                cancellationToken.ThrowIfCancellationRequested();

            }, cancellationToken);

            Thread.Sleep(200);

            cts.Cancel();

            Thread.Sleep(400);

            Assert.AreEqual(TaskStatus.Canceled, task.Status);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var task = Task.Run(() =>
            {
                Thread.Sleep(100);

                // task cancellation via ThrowIfCancellationRequested method -> status is RanToCompletion if cancellationToken was not cancelled -> Ok
                cancellationToken.ThrowIfCancellationRequested();

            }, cancellationToken);

            Thread.Sleep(200);

            Assert.AreEqual(TaskStatus.RanToCompletion, task.Status);
        }
    }
}

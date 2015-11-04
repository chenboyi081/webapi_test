using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task并行编程_2015_11_03
{
    class Program
    {
        static void Main(string[] args)
        {
            //参考：http://www.cnblogs.com/yanyangtian/archive/2010/05/25/1743218.html

            #region .NET 4 并行（多核）编程系列之三 从Task的取消 
            #region 6可以使用Task的IsCancelled属性来判断task是否被取消了
            //// create the cancellation token source
            //CancellationTokenSource tokenSource1 = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token1 = tokenSource1.Token;

            //// create the first task, which we will let run fully
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        token1.ThrowIfCancellationRequested();
            //        Console.WriteLine("Task 1 - Int value {0}", i);
            //    }
            //}, token1);

            //// create the second cancellation token source
            //CancellationTokenSource tokenSource2 = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token2 = tokenSource2.Token;

            //// create the second task, which we will cancel
            //Task task2 = new Task(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
            //        token2.ThrowIfCancellationRequested();
            //        Console.WriteLine("Task 2 - Int value {0}", i);
            //    }
            //}, token2);

            //// start all of the tasks
            //task1.Start();
            //task2.Start();

            //// cancel the second token source
            //tokenSource2.Cancel();
            //// write out the cancellation detail of each task
            //Console.WriteLine("Task 1 cancelled? {0}", task1.IsCanceled);
            //Console.WriteLine("Task 2 cancelled? {0}", task2.IsCanceled);
            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine();
            #endregion

            #region 5创建组合的取消Task的Token
            //// create the cancellation token sources
            //CancellationTokenSource tokenSource1 = new CancellationTokenSource();
            //CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            //CancellationTokenSource tokenSource3 = new CancellationTokenSource();

            //// create a composite token source using multiple tokens
            //CancellationTokenSource compositeSource =
            //    CancellationTokenSource.CreateLinkedTokenSource(
            //tokenSource1.Token, tokenSource2.Token, tokenSource3.Token);

            //// create a cancellable task using the composite token
            //Task task = new Task(() =>
            //{
            //    // wait until the token has been cancelled
            //    compositeSource.Token.WaitHandle.WaitOne();
            //    // throw a cancellation exception
            //    throw new OperationCanceledException(compositeSource.Token);
            //}, compositeSource.Token);

            //// start the task
            //task.Start();

            //// cancel one of the original tokens
            //tokenSource2.Cancel();

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            #region  4取消多个Task
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;

            //// create the tasks
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
            //        token.ThrowIfCancellationRequested();
            //        Console.WriteLine("Task 1 - Int value {0}", i);
            //    }
            //}, token);

            //Task task2 = new Task(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
            //        token.ThrowIfCancellationRequested();
            //        Console.WriteLine("Task 2 - Int value {0}", i);
            //    }
            //}, token);
            //// wait for input before we start the tasks
            //Console.WriteLine("Press enter to start tasks");
            //Console.WriteLine("Press enter again to cancel tasks");
            //Console.ReadLine();

            //// start the tasks
            //task1.Start();
            //task2.Start();

            //// read a line from the console.
            //Console.ReadLine();

            //// cancel the task
            //Console.WriteLine("Cancelling tasks");
            //tokenSource.Cancel();
            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            #region 3用Wait Handle还检测Task是否被取消
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;

            //// create the task
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
            //        if (token.IsCancellationRequested)
            //        {
            //            Console.WriteLine("Task cancel detected");
            //            throw new OperationCanceledException(token);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Int value {0}", i);
            //        }
            //    }
            //}, token);

            //// create a second task that will use the wait handle
            //Task task2 = new Task(() =>
            //{
            //    // wait on the handle
            //    token.WaitHandle.WaitOne();
            //    // write out a message
            //    Console.WriteLine(">>>>> Wait handle released");
            //});

            //// wait for input before we start the task
            //Console.WriteLine("Press enter to start task");
            //Console.WriteLine("Press enter again to cancel task");
            //Console.ReadLine();
            //// start the tasks
            //task1.Start();
            //task2.Start();

            //// read a line from the console.
            //Console.ReadLine();

            //// cancel the task
            //Console.WriteLine("Cancelling task");
            //tokenSource.Cancel();

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            #region 2用委托delegate来检测Task是否被取消
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;

            //// create the task
            //Task task = new Task(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
            //        if (token.IsCancellationRequested)
            //        {
            //            Console.WriteLine("Task cancel detected");
            //            throw new OperationCanceledException(token);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Int value {0}", i);
            //        }
            //    }
            //}, token);

            //// register a cancellation delegate
            //token.Register(() =>
            //{
            //    Console.WriteLine(">>>>>> Delegate Invoked\n");
            //});

            //// wait for input before we start the task
            //Console.WriteLine("Press enter to start task");
            //Console.WriteLine("Press enter again to cancel task");
            //Console.ReadLine();

            //// start the task
            //task.Start();
            //// read a line from the console.
            //Console.ReadLine();

            //// cancel the task
            //Console.WriteLine("Cancelling task");
            //tokenSource.Cancel();

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            #region 1创建一个可以取消的task，并且通过轮询不断的检查是否要取消task
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;
            //// create the task

            //Task task = new Task(() =>
            //{
            //    for (int i = 0; i < int.MaxValue; i++)
            //    {
            //        if (token.IsCancellationRequested)
            //        {
            //            Console.WriteLine("Task cancel detected");
            //            throw new OperationCanceledException(token);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Int value {0}", i);
            //        }
            //    }
            //}, token);

            //// wait for input before we start the task
            //Console.WriteLine("Press enter to start task");
            //Console.WriteLine("Press enter again to cancel task");
            //Console.ReadLine();

            //// start the task
            //task.Start();

            //// read a line from the console.
            //Console.ReadLine();

            //// cancel the task
            //Console.WriteLine("Cancelling task");
            //tokenSource.Cancel();

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion 
            #endregion


        }
    }
}

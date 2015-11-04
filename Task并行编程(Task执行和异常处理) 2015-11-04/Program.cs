using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task并行编程_Task执行和异常处理__2015_11_04
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  1.1等待单独的一个Task执行完成
             *  我们可以用Wait()方法来一直等待一个Task执行完成。当task执行完成，或者被cancel，或者抛出异常，这个方法才会返回。可以使用Wait()方法的不同重载。举个例子:
             *  
             * 从下面的例子可以看出，wait方法子task执行完成之后会返回true。
             * 注意：当在执行的task内部抛出了异常之后，这个异常在调用wait方法时会被再次抛出。后面再"异常处理篇"会讲述。
             * 
             */

            #region 代码1.1
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();
            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;
            //// create and start the first task, which we will let run fully
            //Task task = createTask(token);
            //task.Start();

            //// wait for the task
            //Console.WriteLine("Waiting for task to complete.");
            //task.Wait();
            //Console.WriteLine("Task Completed.");

            //// create and start another task
            //task = createTask(token);
            //task.Start();
            //Console.WriteLine("Waiting 2 secs for task to complete.");
            //bool completed = task.Wait(2000);
            //Console.WriteLine("Wait ended - task completed: {0}", completed);

            //// create and start another task
            //task = createTask(token);
            //task.Start();
            //Console.WriteLine("Waiting 2 secs for task to complete.");
            //completed = task.Wait(2000, token);
            //Console.WriteLine("Wait ended - task completed: {0} task cancelled {1}",
            //completed, task.IsCanceled);

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            /*  1.2.等待多个task
             *  我们也可以用WaitAll()方法来一直到等待多个task执行完成。只有当所有的task执行完成，或者被cancel，或者抛出异常，这个方法才会返回。WiatAll()方法和Wait()方法一样有一些重载。
             *  注意：如果在等在的多个task之中，有一个task抛出了异常，那么调用WaitAll()方法时就会抛出异常。
             *  
             * 
             */

            #region 代码1.2
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();
            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;
            //// create the tasks
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        // check for task cancellation
            //        token.ThrowIfCancellationRequested();
            //        // print out a message
            //        Console.WriteLine("Task 1 - Int value {0}", i);
            //        // put the task to sleep for 1 second
            //        token.WaitHandle.WaitOne(1000);
            //    }
            //    Console.WriteLine("Task 1 complete");
            //}, token);
            //Task task2 = new Task(() =>
            //{
            //    Console.WriteLine("Task 2 complete");
            //}, token);

            //// start the tasks
            //task1.Start();
            //task2.Start();
            //// wait for the tasks
            //Console.WriteLine("Waiting for tasks to complete.");
            //Task.WaitAll(task1, task2);
            //Console.WriteLine("Tasks Completed.");
            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            /*  1.3.等待多个task中的一个task执行完成
             * 
             * 可以用WaitAny()方法来等待多个task中的一个task执行完成。通俗的讲就是：有很多的task在运行，调用了WaitAny()方法之后，只要那些运行的task其中有一个运行完成了，那么WaitAny()就返回了。
             * 
             */

            #region 代码1.3
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();
            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;
            //// create the tasks
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        // check for task cancellation
            //        token.ThrowIfCancellationRequested();
            //        // print out a message
            //        Console.WriteLine("Task 1 - Int value {0}", i);
            //        // put the task to sleep for 1 second
            //        token.WaitHandle.WaitOne(1000);
            //    }
            //    Console.WriteLine("Task 1 complete");
            //}, token);
            //Task task2 = new Task(() =>
            //{
            //    Console.WriteLine("Task 2 complete");
            //}, token);
            //// start the tasks
            //task1.Start();
            //task2.Start();
            //// wait for the tasks
            //Console.WriteLine("Waiting for tasks to complete.");
            //int taskIndex = Task.WaitAny(task1, task2);
            //Console.WriteLine("Task Completed. Index: {0}", taskIndex);
            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion


            /*   2.1处理基本的异常。
             *   在操作task的时候，只要出现了异常，.NET Framework就会把这些异常记录下来。例如在执行Task.Wait(),Task.WaitAll(),Task.WaitAny(),Task.Result.不管那里出现了异常，最后抛出的就是一个System.AggregateException.
             *   　System.AggregateException时用来包装一个或者多个异常的，这个类时很有用的，特别是在调用Task.WaitAll()方法时。因为在Task.WaitAll()是等待多个task执行完成，如果有任意task执行出了异常，那么这个异常就会被记录在System.AggregateException中，不同的task可能抛出的异常不同，但是这些异常都会被记录下来。
             *   　下面就是给出一个例子：在例子中，创建了两个task，它们都抛出异常。然后主线程开始运行task，并且调用WaitAll()方法，然后就捕获抛出的System.AggregateException,显示详细信息。
             *   　
             *     从下面的例子可以看出，为了获得被包装起来的异常，需要调用System.AggregateException的InnerExceptions属性，这个属性返回一个异常的集合，然后就可以遍历这个集合。
             *     而且从下面的例子可以看到：Exeception.Source属性被用来指明task1的异常时ArgumentOutRangeException.
             */

            #region 代码2.1
            //// create the tasks
            //Task task1 = new Task(() =>
            //{
            //    ArgumentOutOfRangeException exception = new ArgumentOutOfRangeException();
            //    exception.Source = "task1";
            //    throw exception;
            //});
            //Task task2 = new Task(() =>
            //{
            //    throw new NullReferenceException();
            //});
            //Task task3 = new Task(() =>
            //{
            //    Console.WriteLine("Hello from Task 3");
            //});
            //// start the tasks
            //task1.Start(); task2.Start(); task3.Start();
            //// wait for all of the tasks to complete
            //// and wrap the method in a try...catch block
            //try
            //{
            //    Task.WaitAll(task1, task2, task3);
            //}
            //catch (AggregateException ex)
            //{
            //    // enumerate the exceptions that have been aggregated
            //    foreach (Exception inner in ex.InnerExceptions)
            //    {
            //        Console.WriteLine("Exception type {0} from {1}",
            //        inner.GetType(), inner.Source);
            //    }
            //}
            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            /*   2.2使用迭代的异常处理Handler
             * 
             *   　一般情况下，我们需要区分哪些异常需要处理，而哪些异常需要继续往上传递。AggregateException类提供了一个Handle()方法，我们可以用这个方法来处理
             *   　AggregateException中的每一个异常。在这个Handle()方法中，返回true就表明，这个异常我们已经处理了，不用抛出，反之。
             *   　 在下面的例子中，抛出了一个OperationCancelException，在之前的task的取消一文中，已经提到过：当在task中抛出这个异常的时候，实际上就是这个task发送了取消的请求。下面的代码中，描述了如果在AggregateException.Handle()中处理不同的异常。
             */

            #region 代码2.2
            //// create the cancellation token source and the token
            //CancellationTokenSource tokenSource = new CancellationTokenSource();
            //CancellationToken token = tokenSource.Token;
            //// create a task that waits on the cancellation token
            //Task task1 = new Task(() =>
            //{
            //    // wait forever or until the token is cancelled
            //    token.WaitHandle.WaitOne(-1);
            //    // throw an exception to acknowledge the cancellation
            //    throw new OperationCanceledException(token);
            //}, token);
            //// create a task that throws an exception
            //Task task2 = new Task(() =>
            //{
            //    throw new NullReferenceException();
            //});
            //// start the tasks
            //task1.Start(); task2.Start();
            //// cancel the token
            //tokenSource.Cancel();
            //// wait on the tasks and catch any exceptions
            //try
            //{
            //    Task.WaitAll(task1, task2);
            //}
            //catch (AggregateException ex)
            //{
            //    // iterate through the inner exceptions using
            //    // the handle method
            //    ex.Handle((inner) =>
            //    {
            //        if (inner is OperationCanceledException)
            //        {

            //            // ...handle task cancellation...
            //            return true;
            //        }
            //        else
            //        {
            //            // this is an exception we don't know how
            //            // to handle, so return false
            //            return false;
            //        }
            //    });
            //}
            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion
        }

        static Task createTask(CancellationToken token)
        {
            return new Task(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    // check for task cancellation
                    token.ThrowIfCancellationRequested();
                    // print out a message
                    Console.WriteLine("Task - Int value {0}", i);
                    // put the task to sleep for 1 second
                    token.WaitHandle.WaitOne(1000);
                }
            }, token);
        }
    }
}

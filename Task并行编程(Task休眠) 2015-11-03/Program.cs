﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task并行编程_Task休眠__2015_11_03
{
    class Program
    {
        static void Main(string[] args)
        {
            /*    a.       使用CancellationToken的Wait Handle：
             * 
             *   在.NET 4并行编程中，让一个Task休眠的最好的方式就是使用CancellationToken的等待操作(Wait Handle)。而且操作起来也很简单：首先创建一个CancellationTokenSource的实例，然后通过这个实例的Token属性得到一个CancellationToken的实例，然后在用CancellationToken的WaitHandle属性，然后调用这个这个属性的WaitOne()方法。其实在之前讲述”Task的取消”一文中就已经使用过。
             *    WaitOne()方法有很多的重载方法来提供更多的功能，例如可以传入一个int的整数，表明要休眠多长的时间，单位是微秒，也可以传入一个TimeSpan的值。如果调用了CancellationToken的Cancel()方法，那么休眠就立刻结束。就是因为这个原因，我们之前的文章讲过，WaitOne()可以作为检测Task是否被取消的一个方案
             *    
             *  在下面的代码中，task在休眠了10秒钟之后就打印出一条信息。在例子中，在我们敲一下键盘之后，CancellationToken就会被Cancel，此时休眠就停止了，task重新唤醒，只不过是这个task将会被cancel掉。
             *  有一点要注意：WaitOne()方法只有在设定的时间间隔到了，或者Cancel方法被调用，此时task才会被唤醒。如果如果cancel()方法被调用而导致task被唤醒，那么CancellationToken.WaitHandle.WaitOne()方法就会返回true，如果是因为设定的时间到了而导致task唤醒，那么CancellationToken.WaitHandle.WaitOne()方法返回false。
             */

            #region 代码a
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;

            //// create the first task, which we will let run fully
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < Int32.MaxValue; i++)
            //    {
            //        // put the task to sleep for 10 seconds
            //        bool cancelled = token.WaitHandle.WaitOne(10000);
            //        // print out a message
            //        Console.WriteLine("Task 1 - Int value {0}. Cancelled? {1}",
            //        i, cancelled);
            //        // check to see if we have been cancelled
            //        if (cancelled)
            //        {
            //            throw new OperationCanceledException(token);
            //        }
            //    }
            //}, token);
            //// start task
            //task1.Start();

            //// wait for input before exiting
            //Console.WriteLine("Press enter to cancel token.");
            //Console.ReadLine();

            //// cancel the token
            //tokenSource.Cancel();

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            /*
             *    b.       task休眠的第二种方法：使用传统的Sleep。
             * 　我们现在已经知道了：其实TPL(并行编程)的底层还是基于.NET的线程机制的。所以还是可以用传统的线程技术来使得一个task休眠：调用静态方法—Thread.Sleep(),并且可以传入一个int类型的参数，表示要休眠多长时间。
             * 　
             */

            #region 代码b
            //// create the cancellation token source
            //CancellationTokenSource tokenSource = new CancellationTokenSource();

            //// create the cancellation token
            //CancellationToken token = tokenSource.Token;

            //// create the first task, which we will let run fully
            //Task task1 = new Task(() =>
            //{
            //    for (int i = 0; i < Int32.MaxValue; i++)
            //    {
            //        // put the task to sleep for 10 seconds
            //        Thread.Sleep(1000);

            //        // print out a message
            //        Console.WriteLine("Task 1 - Int value {0}", i);
            //        // check for task cancellation
            //        token.ThrowIfCancellationRequested();
            //    }
            //}, token);
            //// start task
            //task1.Start();

            //// wait for input before exiting
            //Console.WriteLine("Press enter to cancel token.");
            //Console.ReadLine();

            //// cancel the token
            //tokenSource.Cancel();

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            /*   c.       第三种休眠方法：自旋等待.
             *   这种方法也是值得推荐的。之前的两种方法，当他们使得task休眠的时候，这些task已经从Scheduler的管理中退出来了，不被再内部的Scheduler管理(Scheduler，这里只是简单的提下，因为后面的文章会详细讲述，这里只要知道Scheduler是负责管理线程的)，因为休眠的task已经不被Scheduler管理了，所以Scheduler必须做一些工作去决定下一步是哪个线程要运行，并且启动它。为了避免Scheduler做那些工作，我们可以采用自旋等待：此时这个休眠的task所对应的线程不会从Scheduler中退出，这个task会把自己和CPU的轮转关联起来，我们还是用代码示例讲解吧。
             *   
             * 　代码中我们在Thread.SpinWait()方法中传入一个整数，这个整数就表示CPU时间片轮转的次数，至于要等待多长的时间，这个就和计算机有关了，不同的计算机，CPU的轮转时间不一样。自旋等待的方法常常于获得同步锁，后续会讲解。使用自旋等待会一直占用CPU，而且也会消耗CPU的资源，更大的问题就是这个方法会影响Scheduler的运作。
             */

            #region 代码c
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create the cancellation token
            CancellationToken token = tokenSource.Token;

            // create the first task, which we will let run fully
            Task task1 = new Task(() =>
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    // put the task to sleep for 10 seconds
                    Thread.SpinWait(10000);
                    // print out a message
                    Console.WriteLine("Task 1 - Int value {0}", i);
                    // check for task cancellation
                    token.ThrowIfCancellationRequested();
                }
            }, token);

            // start task
            task1.Start();

            // wait for input before exiting
            Console.WriteLine("Press enter to cancel token.");

            Console.ReadLine();
            // cancel the token
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine(); 
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task基础部分完结篇_2015_11_04
{
    class Program
    {
        static void Main(string[] args)
        {
            /*   1.  获取Task的状态
             *   在.NET并行编程还有一个已经标准化的操作就是可以获取task的状态，通过Task.Status属性来得到的，这个属性返回一个System.Threading.Tasks.TaskStatus的枚举值。
             *   如下：
             *    Created:表明task已经被初始化了，但是还没有加入到Scheduler中。
             *    WatingForActivation：task正在等待被加入到Scheduler中。
             *    WaitingToRun:已经被加入到了Scheduler，等待执行。
             *    Running：task正在运行
             *    WaitingForChildrenToComplete:表明父task正在等待子task运行结束。
             *    RanToCompletion:表明task已经执行完了，但是还没有被cancel，而且也这个task也没有抛出异常。
             *    Canceled:表明task已经被cancel了。（大家可以参看之前讲述取消task的文章）
             *    Faulted:表明task在运行的时候已经抛出了异常。
             * 
             */

            /*   2.执行晚加载的Task(Lazily Task)
             *   晚加载，或者又名延迟初始化，主要的好处就是避免不必要的系统开销。在并行编程中，可以联合使用Lazy变量和Task<>.Factory.StartNew()做到这点。(Lazy变量时.NET 4中的一个新特性，这里大家不用知道Lazy的具体细节)
             *   Lazy变量只有在用到的时候才会被初始化。所以我们可以把Lazy变量和task的创建结合：只有这个task要被执行的时候才去初始化。
             *   下面还是通过例子来讲解：
             *   
             * 首先我们回想一下，在之前的系列文章中我们是怎么定义一个task的：直接new，或者通过task的factory来创建，因为创建task的代码是在main函数中的，所以只要new了一个task，那么这个task就被初始化。
             * 现在如果用了Lazy的task，那么现在我们初始化的就是那个Lazy变量了，而没有初始化task，(初始化Lazy变量的开销小于初始化task)，只有当调用了lazyData.Value时，Lazy变量中包含的那个task才会初始化。（这里欢迎大家提出自己的理解）
             */

            #region 代码 2
            //// define the function
            //Func<string> taskBody = new Func<string>(() =>
            //{
            //    Console.WriteLine("Task body working...");
            //    return "Task Result";
            //});

            //// create the lazy variable
            //Lazy<Task<string>> lazyData = new Lazy<Task<string>>(() =>
            //Task<string>.Factory.StartNew(taskBody));

            //Console.WriteLine("Calling lazy variable");
            //Console.WriteLine("Result from task: {0}", lazyData.Value.Result);

            //// do the same thing in a single statement
            //Lazy<Task<string>> lazyData2 = new Lazy<Task<string>>(
            //() => Task<string>.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("Task body working...");
            //    return "Task Result";
            //}));

            //Console.WriteLine("Calling second lazy variable");
            //Console.WriteLine("Result from task: {0}", lazyData2.Value.Result);

            //// wait for input before exiting
            //Console.WriteLine("Main method complete. Press enter to finish.");
            //Console.ReadLine(); 
            #endregion

            /*    3.常见问题的解决方案
             *     Task 死锁
             *     描述：如果有两个或者多个task(简称TaskA)等待其他的task（TaskB）执行完成才开始执行，但是TaskB也在等待TaskA执行完成才开始执行，这样死锁就产生了。
             *     解决方案：避免这个问题最好的方法就是：不要使的task来依赖其他的task。也就是说，最好不要你定义的task的执行体内包含其他的task。
             *     例子：在下面的例子中，有两个task，他们相互依赖：他们都要使用对方的执行结果。当主程序开始运行之后，两个task也开始运行，但是因为两个task已经死锁了，所以主程序就一直等待。
             * 
             */

            #region 代码 死锁
            // define an array to hold the Tasks
            Task<int>[] tasks = new Task<int>[2];

            // create and start the first task
            tasks[0] = Task.Factory.StartNew(() =>
            {
                // get the result of the other task,
                // add 100 to it and return it as the result
                return tasks[1].Result + 100;
            });

            // create and start the second task
            tasks[1] = Task.Factory.StartNew(() =>
            {
                // get the result of the other task,
                // add 100 to it and return it as the result
                return tasks[1].Result + 100;
            });


            // wait for the tasks to complete
            Task.WaitAll(tasks);

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine(); 
            #endregion

        }
    }
}

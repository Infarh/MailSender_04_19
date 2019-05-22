using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class ThreadsTest
    {
        public static void Start()
        {
            //var thread = new Thread(new ThreadStart(ThreadMethod));
            var thread = new Thread(ThreadMethod);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Highest;
            //thread.Start();

            var parameter_thread = new Thread(ParametrizedThreadMethod);
            parameter_thread.IsBackground = true;
            //parameter_thread.Start("Hello World!");

            //var parameter2 = "123";
            //var parameter_thread2 = new Thread(() => ParametrizedThreadMethod(parameter2));
            //parameter_thread2.Start();

            var threads = new List<Thread>();
            for (var i = 0; i < 10; i++)
            {
                var i0 = i;
                threads.Add(new Thread(() => ParametrizedThreadMethod($"Message {i0}")));
            }

            //threads.ForEach(t => t.Start());

            //var thread2 = new Thread(new ThreadTask { Data = "123" }.ThreadMethod);
            var thread2task = new ThreadTask { Data = "123" };
            var thread2 = new Thread(thread2task.ThreadMethod);
            thread2.Start();

            Console.WriteLine("Нажмите Enter для завершения фонового потока");
            Console.ReadLine();

            _DoWork = false;
            //thread.Join(); // дождаться завершения потока
            if (!thread.Join(200))
            {
                thread.Abort();
                //thread.Interrupt();
            }
        }

        private static bool _DoWork = true;

        private static void ThreadMethod()
        {
            try
            {
                for (var i = 0; _DoWork & i < 100_000; i++)
                {
                    Thread.Sleep(100);
                    Console.Title = $"Iteration {i} - {DateTime.Now}";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ParametrizedThreadMethod(object parameter)
        {
            var msg = parameter as string ?? throw new ArgumentException("Параметр не является строкой");

            for (var i = 0; _DoWork & i < 100_000; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"Message({i}): {msg} : thread id: {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        private static void ParametrizedThreadMethod2(string msg)
        {
            for (var i = 0; _DoWork & i < 100_000; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"Message({i}): {msg} : thread id: {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        private static void ParametrizedThreadMethod3(int index)
        {
            for (var i = 0; _DoWork & i < 100_000; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"Message({i}): index {index}");
            }
        }
    }
}

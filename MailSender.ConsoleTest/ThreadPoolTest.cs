using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class ThreadPoolTest
    {
        public static void Start()
        {
            var processor_count = Environment.ProcessorCount;

            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(7, 7);

            for (var i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadPoolMethod);
            }
        }

        private static void ThreadPoolMethod(object state)
        {
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Message {i} from {Thread.CurrentThread.ManagedThreadId} thread");
            }
        }
    }
}

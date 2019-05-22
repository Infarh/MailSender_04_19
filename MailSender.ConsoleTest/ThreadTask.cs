using System;
using System.Threading;

namespace MailSender.ConsoleTest
{
    internal class ThreadTask
    {
        public string Data { get; set; }

        public void ThreadMethod()
        {
            var data = Data;
            for (var i = 0; i < 100_000; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"Message({i}): {data} : thread id: {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }
}
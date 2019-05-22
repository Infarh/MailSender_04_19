using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MailSender.ConsoleTest
{
    internal static class ThreadManagment
    {
        public static void Start()
        {
            //Mutex mutex = new Mutex(true, "TestMutex", out var is_created_new);

            //Semaphore semaphore = new Semaphore(0, 5);

            //for (var i = 0; i < 20; i++)
            //{
            //    var i0 = i;
            //    var thread = new Thread(() =>
            //    {
            //        semaphore.WaitOne();

            //        for (var j = 0; j < 10; j++)
            //        {
            //            Console.WriteLine($"Thread id: {Thread.CurrentThread.ManagedThreadId} - {i}");
            //        }

            //        semaphore.Release();
            //    });
            //    thread.Start();
            //}

            //AutoResetEvent auto_reset_event = new AutoResetEvent(false);
            //ManualResetEvent manual_reset_event = new ManualResetEvent(false);

            //for (var i = 0; i < 10; i++)
            //{
            //    var thread = new Thread(() =>
            //    {
            //        Console.WriteLine($"Thread id:{Thread.CurrentThread.ManagedThreadId} - started");
            //        auto_reset_event.WaitOne();
            //        Console.WriteLine($"Thread id:{Thread.CurrentThread.ManagedThreadId} - completed");
            //    });
            //    thread.Start();
            //}

            //for (var i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("Для продолжения выполнения потока нажмите Enter");
            //    Console.ReadLine();
            //    auto_reset_event.Set();
            //}
        }
    }
}

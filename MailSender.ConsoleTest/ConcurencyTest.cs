using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class ConcurencyTest
    {
        public static void Start()
        {
            for (var i = 0; i < 10; i++)
            {
                var thread = new Thread(SyncThreadMethod);
                thread.Start();
            }

            var result = new List<int>();
            for (var i = 0; i < 100; i++)
            {
                var i0 = i;
                #region Очень плохо!
                //var thread = new Thread(() =>
                //       {
                //           result.Add(DigitsCount(i0));
                //       }); 
                #endregion

                var thread = new Thread(() =>
                {
                    var count = DigitsCount(i0);
                    lock (result)
                        result.Add(count);
                });

                thread.Start();
            }



        }

        private static int DigitsCount(int N)
        {
            return 42;
        }

        public static readonly object SyncRoot = new object();
        private static void ThreadMethod()
        {
            Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId} - ");
            for (var i = 0; i < 10; i++)
                Console.Write($"{i}, ");
            Console.WriteLine("10");
        }

        private static void SyncThreadMethod()
        {
            lock (SyncRoot)
            {
                Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId} - ");
                for (var i = 0; i < 10; i++)
                    Console.Write($"{i}, ");
                Console.WriteLine("10");
            }
        }

        private static void SyncThreadMethod2()
        {
            Monitor.Enter(SyncRoot);
            try
            {
                Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId} - ");
                for (var i = 0; i < 10; i++)
                    Console.Write($"{i}, ");
                Console.WriteLine("10");
            }
            finally
            {
                if (Monitor.IsEntered(SyncRoot))
                    Monitor.Exit(SyncRoot);
            }
        }
    }

    [Synchronization]
    internal class Logger : ContextBoundObject, IDisposable
    {
        private readonly TextWriter _Writer;

        public Logger(TextWriter Writer) => _Writer = Writer;

        public Logger(string FileName) : this(new StreamWriter(File.OpenWrite(FileName))) { }

        public void Log(string msg) => _Writer.WriteLine($"{DateTime.Now} - {msg}");

        public void Dispose() => _Writer.Dispose();
    }

    internal class Logger2 : IDisposable
    {
        private readonly TextWriter _Writer;

        public Logger2(TextWriter Writer) => _Writer = Writer;

        public Logger2(string FileName) : this(new StreamWriter(File.OpenWrite(FileName))) { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Log(string msg) => _Writer.WriteLine($"{DateTime.Now} - {msg}");

        public void Dispose() => _Writer.Dispose();
    }
}

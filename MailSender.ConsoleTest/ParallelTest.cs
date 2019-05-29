using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class ParallelTest
    {
        public static void Start()
        {
            //Parallel.Invoke(/*new ParallelOptions { MaxDegreeOfParallelism = 2 },*/
            //    TestMethod, TestMethod, TestMethod, TestMethod,
            //    () => Console.WriteLine($"Ещё один метод в потоке id:{Thread.CurrentThread.ManagedThreadId}"));

            //Parallel.For(0, 10, TestMethodWithData);

            //Parallel.For(0, 100, i => Console.WriteLine("Data:{0} - thread id:{1}", i, Thread.CurrentThread.ManagedThreadId));

            //var parallel_for_result = Parallel.For(0, 100,/* new ParallelOptions { MaxDegreeOfParallelism = 2 },*/ (i, state) =>
            //{
            //    Console.WriteLine("Data:{0} - thread id:{1}", i, Thread.CurrentThread.ManagedThreadId);
            //    if(i > 20)
            //        state.Break();
            //});

            //Console.WriteLine($"Выполнено итераций {parallel_for_result.LowestBreakIteration}");

            //IEnumerable<string> messages = Enumerable.Range(1, 100).Select(i => $"Message {i}");

            //Parallel.ForEach(messages, msg => Console.WriteLine($"msg:{msg} in thread id: {Thread.CurrentThread.ManagedThreadId} task id:{Task.CurrentId}"));
            //var parallel_foreach_result = Parallel.ForEach(messages, 
            //    //new ParallelOptions { MaxDegreeOfParallelism = 2 },
            //    (msg, state) =>
            //    {
            //        if(msg.EndsWith("20"))
            //            state.Break();
            //        Console.WriteLine($"msg:{msg} in thread id: {Thread.CurrentThread.ManagedThreadId} task id:{Task.CurrentId}");
            //    });
            //Console.WriteLine($"Выполнено итераций {parallel_foreach_result.LowestBreakIteration}");

            //ParallelQuery<string> parallel_message_source = messages.AsParallel();

            //var parallel_query_cancellation = new CancellationTokenSource();
            //messages.AsParallel()
            //   .WithDegreeOfParallelism(2)                                 // Количетсво потоков
            //   .WithExecutionMode(ParallelExecutionMode.ForceParallelism)  // Режим возможности распараллеливания
            //   .WithMergeOptions(ParallelMergeOptions.AutoBuffered)        // Режим буфферизации данных
            //   .WithCancellation(parallel_query_cancellation.Token)        // Маркер отмены операций
            //   .Where(msg => int.Parse(msg.Split(' ')[1]) % 2 == 0)
            //   .Select(msg => $"Message \"{msg}\" processed in thread id:{Thread.CurrentThread.ManagedThreadId}")
            //   //.AsSequential()
            //   .ForAll(Console.WriteLine);

            //parallel_query_cancellation.Cancel(); // Так можно было бы в параллельном потоке прервать процесс обработки выше

            //foreach (var msg_length in messages.AsParallel().Select(msg => msg.Length))
            //{
            //    Console.WriteLine(msg_length);
            //}


            //Action test_method_delegate = TestMethod;

            //test_method_delegate();        // синхронный вызов
            //test_method_delegate.Invoke(); // тоже синхронный вызов

            //test_method_delegate.BeginInvoke(result => { }, null); // Асинхронный, параллельный вызов метода

            //__Calculator = new Func<string, int>(Calculator);
            //var data = "Hello World!";
            //__Calculator.BeginInvoke(data, CalculatorResultProcessor, data);
        }

        private static void TestMethod()
        {
            Console.WriteLine($"Метод запущен в потоке id:{Thread.CurrentThread.ManagedThreadId}, task id:{Task.CurrentId}");

            Thread.Sleep(1500);

            Console.WriteLine($"Метод завершён в потоке id:{Thread.CurrentThread.ManagedThreadId}, task id:{Task.CurrentId}");
        }

        private static void TestMethodWithData(int i)
        {
            Console.WriteLine($"Метод (i:{i}) запущен в потоке id:{Thread.CurrentThread.ManagedThreadId}, task id:{Task.CurrentId}");

            Thread.Sleep(1500);

            Console.WriteLine($"Метод (i:{i}) завершён в потоке id:{Thread.CurrentThread.ManagedThreadId}, task id:{Task.CurrentId}");
        }

        private static int Calculator(string Data)
        {
            Thread.Sleep(1500);
            return Data.Length;
        }

        private static Func<string, int> __Calculator;
        private static void CalculatorResultProcessor(IAsyncResult result)
        {
            var data = (string) result.AsyncState;
            var calculator_result = __Calculator.EndInvoke(result);
            Console.WriteLine($"Результат вычисления значения {calculator_result} - данные: {data}");
        }
    }
}
                                                                                                          
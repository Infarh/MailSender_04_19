using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class TaskTest
    {
        public static void Start()
        {
            //var task = new Task(TestMethod);
            ////task.CreationOptions
            ////task.
            //task.Start();

            //var test_task = Task.Run(TestMethod);
            //Console.WriteLine("Первая задача запущена");

            var data = "Hello World!";
            ////var task_int = new Task<int>(() => Calculator(data));
            ////task_int.Start();
            //var task_int = Task.Run(() => Calculator(data));
            //Console.WriteLine("Вторая задача запущена");

            ////test_task.Wait();
            ////task_int.Wait();
            //Task.WaitAll(test_task, task_int);   // Ждёт пока все задачи не завершатся
            ////Task.WaitAny(test_task, task_int); // Ждёт пока не завершится первая из перечисленных задач

            //var task_int_result = task_int.Result;

            //Console.WriteLine("Result {0}", task_int_result);

            var calculator_task = Task.Factory.StartNew(obj => Calculator((string)obj), null);

            var calculator_task_continuation = calculator_task.ContinueWith(
                t => Console.WriteLine($"Результат выполнения {t.Result}"),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            calculator_task.ContinueWith(
                t => Console.WriteLine($"В процессе выполнения возникла ошибка\r\n{t.Exception}"),
                TaskContinuationOptions.OnlyOnFaulted);

            calculator_task_continuation.ContinueWith(t => Console.WriteLine("Теперь точно всё..."));

            //__Calculator = new Func<string, int>(Calculator);

            //var calculator_task2 = Task.Factory.FromAsync(__Calculator.BeginInvoke, __Calculator.EndInvoke, data, null);
        }

        private static void TestMethod()
        {
            Console.WriteLine($"Метод запущен в потоке id:{Thread.CurrentThread.ManagedThreadId}, task id:{Task.CurrentId}");

            Thread.Sleep(3000);

            Console.WriteLine($"Метод завершён в потоке id:{Thread.CurrentThread.ManagedThreadId}, task id:{Task.CurrentId}");
        }

        public static int Calculator(string Data)
        {
            if(Data is null)
                throw new ArgumentNullException(nameof(Data));

            Console.WriteLine($"Calculator({Data}) => thread id: {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1500);
            return Data.Length;
        }

        private static Func<string, int> __Calculator;
        private static void CalculatorResultProcessor(IAsyncResult result)
        {
            var data = (string)result.AsyncState;
            var calculator_result = __Calculator.EndInvoke(result);
            Console.WriteLine($"Результат вычисления значения {calculator_result} - данные: {data}");
        }
    }
}

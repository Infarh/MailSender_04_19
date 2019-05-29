using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    internal static class AsyncAwaitTest
    {
        public static void Start()
        {
            //Console.WriteLine($"Start() -> thread id: {Thread.CurrentThread.ManagedThreadId}");

            //var data = "Hello World!";
            //var fault_data = (string) null;

            //StartAsync(data);

            //Console.WriteLine($"Start() completed -> thread id: {Thread.CurrentThread.ManagedThreadId}");

            IEnumerable<string> messages = Enumerable.Range(1, 20).Select(i => $"Message {i}");

            //MessagesProcessorAsync(messages);
            MessageLengthCalculatorAsync(messages);
        }

        private static async void StartAsync(string Data)
        {
            Console.WriteLine($"StartAsync() -> thread id: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() => TaskTest.Calculator(Data));

            Console.WriteLine($"StartAsync() continue -> thread id: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() => TaskTest.Calculator(Data));

            Console.WriteLine($"StartAsync() completed -> thread id: {Thread.CurrentThread.ManagedThreadId}");
        }

        private static async void MessagesProcessorAsync(IEnumerable<string> Messages)
        {
            //var tasks = new List<Task>();

            //foreach (var msg in Messages)
            //{
            //    var msg_local = msg;
            //    tasks.Add(Task.Run(() => Console.WriteLine($"Process ({msg_local}) in thread id: {Thread.CurrentThread.ManagedThreadId} task id: {Task.CurrentId}")));
            //}

            var tasks = Messages.Select(msg => Task.Run(() => Console.WriteLine($"Process ({msg}) in thread id: {Thread.CurrentThread.ManagedThreadId} task id: {Task.CurrentId}")));

            await Task.WhenAll(tasks);

            Console.WriteLine("Обработка всех сообщений завершена!");
        }

        private static async void MessageLengthCalculatorAsync(IEnumerable<string> Messages)
        {
            //var tasks = Messages.Select(msg => GetMessageLengthAsync(msg));
            var tasks = Messages.Select(GetMessageLengthAsync);

            //tasks = tasks.ToArray();//Принудительный запуск сформированных задач

            var result_task = Task.WhenAll(tasks);
            Console.WriteLine("Некоторая работа в паралель процессу обработки сообщений");

            var result = await result_task;

            Console.WriteLine("Полная длина всех сообщений {0}", result.Sum());
        }

        private static async Task<int> GetMessageLengthAsync(string Msg)
        {
            //Thread.Sleep(1500);
            //Thread.SpinWait(1000);
            Console.WriteLine($"Сформирована задача расчёта длины сообщения {Msg} - trid:{Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1500);
            Console.WriteLine($"Задача расчёта длины сообщения завершилась {Msg} - trid:{Thread.CurrentThread.ManagedThreadId}");
            return Msg.Length;
            //return Task.FromResult(Msg.Length);
        }

    }
}

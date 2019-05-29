using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace MailSender.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParallelTest.Start();
            //TaskTest.Start();           
            AsyncAwaitTest.Start();


            Console.WriteLine("Основной поток завершился. Нажмите Enter для выхода.");
            Console.ReadLine();
        }
    }
}

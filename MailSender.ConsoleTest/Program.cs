using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadsTest.Start();
            //ThreadPoolTest.Start();
            //ConcurencyTest.Start();
            ThreadManagment.Start();


            lock (ConcurencyTest.SyncRoot)
                Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
        }
    }
}

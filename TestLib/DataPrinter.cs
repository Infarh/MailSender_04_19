using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib
{
    public class DataPrinter
    {
        private string _Prefix;

        public int IntData { get; set; }

        public DataPrinter(string Prefix) => _Prefix = Prefix;

        public void Print(string Data) => Console.WriteLine("{0}:{1} - {2}", _Prefix, Data, IntData);
    }

    internal class PrivateDataPrinter : DataPrinter
    {
        private string _DataValue = "Hello World!";

        public PrivateDataPrinter() : base("prefix") { }

        private void PrivatePrint() => Print(_DataValue);
    }
}

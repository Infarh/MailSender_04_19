using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace MailSender.ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Type program_type = typeof(Program);
            var program = new Program();
            program_type = program.GetType();

            //program_type.GetProperties();
            //program_type.GetConstructor();
            //program_type.GetEvents();
            //program_type.GetFields();
            //program_type.GetMethods();

            //Assembly
            //AssemblyName
            //Module

            //Type
            //Delegate
            //Enum

            //ConstructorInfo
            //PropertyInfo
            //MethodInfo
            //ParameterInfo
            //FieldInfo
            //EventInfo

            //MemberInfo

            const string LibName = "TestLib.dll";

            var lib_path = Path.GetFullPath(LibName);

            var test_lib_assembly = Assembly.LoadFrom(LibName);

            var public_types = test_lib_assembly.ExportedTypes;
            var defined_types = test_lib_assembly.DefinedTypes;

            var printer_type = test_lib_assembly.GetType("TestLib.DataPrinter");

            foreach (var method_info in printer_type.GetMethods())
            {
                Console.WriteLine($"method {method_info.Name} : {method_info.ReturnType}");
                foreach (var parameter_info in method_info.GetParameters())
                    Console.WriteLine($"\tparameter {parameter_info.Name} : {parameter_info.ParameterType}");
            }

            var printer_ctor = printer_type.GetConstructor(new[] { typeof(string) });

            var printer1 = printer_ctor.Invoke(new object[] { "TestData" });
            var printer2 = Activator.CreateInstance(printer_type, "TestData2");

            var print_method_info = printer_type.GetMethod("Print");

            print_method_info.Invoke(printer1, new object[] { "123" });
            print_method_info.Invoke(printer2, new object[] { "890" });

            var prefix_field_info = printer_type.GetField("_Prefix", BindingFlags.Instance | BindingFlags.NonPublic);

            prefix_field_info.SetValue(printer1, ">>>");

            var private_prefix_value = (string)prefix_field_info.GetValue(printer1);

            var print = (Action<string>)print_method_info.CreateDelegate(typeof(Action<string>), printer1);

            print("Test - Hello World!");

            print = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), printer1, print_method_info);

            print("Test - Hello World! - 2");

            var private_printer_type = test_lib_assembly.GetType("TestLib.PrivateDataPrinter");

            var private_printer = private_printer_type.GetConstructor(new Type[0])?.Invoke(null);

            var private_print_info = private_printer_type.GetMethod("PrivatePrint", BindingFlags.Instance | BindingFlags.NonPublic);
            private_print_info?.Invoke(private_printer, null);
            Console.Clear();

            dynamic dynamic_printer = printer1;

            dynamic_printer.Print("123");

            var data = new object[]
            {
                new [] { 13 },
                "Hello World!",
                true,
                3.14159265358979,
                42,
                'c',
            };

            Process(data);

            GC.Collect();

            //var binary_formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //var memory_stream = new MemoryStream();
            //binary_formatter.Serialize(memory_stream, printer1);
            //memory_stream.Seek(0, SeekOrigin.Begin);
            //printer2 = binary_formatter.Deserialize(memory_stream);

            //var xml_serializer = new System.Xml.Serialization.XmlSerializer(printer1.GetType());

            //var json_serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(printer1.GetType());

            int? ref_int = 42;
            Nullable<int> ref_int2 = 42;

            Console.ReadLine();
            Console.Clear();
        }

        private static void Process(object[] data)
        {
            //foreach (var value in data)
            //    switch (value)
            //    {
            //        case string v: ProcessValue(v); break;
            //        case bool v: ProcessValue(v); break;
            //        case double v: ProcessValue(v); break;
            //        case int v: ProcessValue(v); break;
            //        case char v: ProcessValue(v); break;
            //    }

            foreach (dynamic value in data)
                ProcessValue(value);
        }

        private static void ProcessValue(string value) => Console.WriteLine($"str:{value}");
        private static void ProcessValue(bool value) => Console.WriteLine($"bool:{value}");
        private static void ProcessValue(double value) => Console.WriteLine($"double:{value}");
        private static void ProcessValue(int value) => Console.WriteLine($"int:{value}");
        private static void ProcessValue(char value) => Console.WriteLine($"char:{value}");
        private static void ProcessValue(object value) => Console.WriteLine($"object:{value}");
    }

  

    class Logger : IDisposable
    {
        private readonly Stream _DataStream;

        public Logger(string FileName) =>
            _DataStream = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.Read);

        public Logger(NetworkStream DataStream) => _DataStream = DataStream;

        //~Logger() => Dispose(false);

        public void Log(object data)
        {
            var writer = new StreamWriter(_DataStream, Encoding.UTF32, 2048, leaveOpen: true);
            dynamic dynamic_data = data;
            writer.Write(dynamic_data);
        }

        private bool _Disposed;

        protected virtual void Dispose(bool Disposing)
        {
            if(_Disposed) return;
            if (Disposing)
            {
                _DataStream.Dispose(); // Освобождаются только управляемые ресурсы
            }
            // Здесь освобождаются неуправляемые ресурсы
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

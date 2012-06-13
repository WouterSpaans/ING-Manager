namespace ING_Manager.TransactionParser
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using ING_Manager.Data;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ING Transaction Parser...");

            Csv.Parse(new DirectoryInfo(args.Any() ? args[0] : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            
            Console.WriteLine("Done!");
        }
    }
}

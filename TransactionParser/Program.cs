using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ING_Manager.TransactionParser
{
    using System.IO;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ING Transaction Parser...");
          
            string csvFolder = args.Any() ? args[0] : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Data.Csv.Parse(new DirectoryInfo(csvFolder));
            
            Console.ReadLine();
        }
    }
}

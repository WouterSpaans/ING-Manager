using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ING_Manager.Data
{
    using System.IO;

    public class Csv
    {
        public static void Parse(DirectoryInfo csvDirectory)
        {
            foreach (FileInfo csvFile in csvDirectory.EnumerateFiles("*.csv"))
            {
                Console.WriteLine("  Parsing file: {0}", csvFile.Name);
            }
        }
    }
}

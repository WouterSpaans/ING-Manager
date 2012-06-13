// -----------------------------------------------------------------------
// <copyright file="Xml.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ING_Manager.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Xml
    {
        private static string dataXml = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Data.xml";

        public static DataSet Read()
        {
            var ds = new DataSet();

            if (File.Exists(dataXml))
            {
                ds.ReadXml(dataXml);
            }

            return ds;
        }

        internal static void Write(DataSet ds)
        {
            // Read earlier data into set...
            ds.WriteXml(dataXml);
        }

    }
}

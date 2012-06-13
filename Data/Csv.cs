namespace ING_Manager.Data
{
    using System;
    using System.IO;
    using System.Linq;

    using LINQtoCSV;

    public class Csv
    {
        private static readonly CsvFileDescription InputFileDescription = new CsvFileDescription
        {
            // MaximumNbrExceptions = -1,
            SeparatorChar = ',',
            EnforceCsvColumnAttribute = true,
            FirstLineHasColumnNames = false,
        };
        
        public static DataSet Parse(DirectoryInfo csvDirectory)
        {
            var ds = Xml.Read();
            ds.AcceptChanges();

            var csvContext = new CsvContext();
            foreach (FileInfo csvFile in csvDirectory.EnumerateFiles("*.csv"))
            {
                Console.WriteLine("  Parsing file: {0}", csvFile.Name);
                foreach (CsvItemBase csvItemBase in csvContext.Read<CsvItemBase>(csvFile.FullName, InputFileDescription))
                {
                    var account = ds.Account.FirstOrDefault(x => x.ID == csvItemBase.ContraAccount)
                              ?? ds.Account.AddAccountRow(csvItemBase.ContraAccount);
                   
                    var timeStamp = new DateTime(
                        int.Parse(csvItemBase.TimeStamp.Substring(0, 4)),
                        int.Parse(csvItemBase.TimeStamp.Substring(4, 2)),
                        int.Parse(csvItemBase.TimeStamp.Substring(6, 2)));

                    var transaction =
                        ds.Transaction.FirstOrDefault(x => x.AccountID == csvItemBase.ContraAccount && x.TimeStamp == timeStamp && x.Description == csvItemBase.Description)
                        ?? ds.Transaction.AddTransactionRow(
                            timeStamp, 
                            account, 
                            csvItemBase.Description,
                            decimal.Parse(csvItemBase.Ammount),
                            csvItemBase.BijAf == "B",
                            csvItemBase.Currency,
                            csvItemBase.Name,
                            csvItemBase.TransactionType);
                }
            }

            if (ds.HasChanges())
            {
                Xml.Write(ds);
            }

            return ds;
        }

        public class CsvItemBase
        {
            #region Public Properties

            [CsvColumn(FieldIndex = 1, CanBeNull = false)]
            public string Account { get; set; }

            [CsvColumn(FieldIndex = 8, CanBeNull = false, OutputFormat = "C")]
            public string Ammount { get; set; }

            [CsvColumn(FieldIndex = 9, CanBeNull = false)]
            public string BijAf { get; set; }

            [CsvColumn(FieldIndex = 5, CanBeNull = false)]
            public long ContraAccount { get; set; }

            [CsvColumn(FieldIndex = 12, CanBeNull = false)]
            public string Currency { get; set; }

            [CsvColumn(FieldIndex = 2, CanBeNull = false)]
            public string TimeStamp { get; set; }

            [CsvColumn(FieldIndex = 11)]
            public string Description { get; set; }

            [CsvColumn(FieldIndex = 6)]
            public string Name { get; set; }

            [CsvColumn(FieldIndex = 3, CanBeNull = false)]
            public string TransactionType { get; set; }

            [CsvColumn(FieldIndex = 4, CanBeNull = false)]
            public int Unknown1 { get; set; }

            [CsvColumn(FieldIndex = 7)]
            public int? Unknown2 { get; set; }

            [CsvColumn(FieldIndex = 10)]
            public char Unknown3 { get; set; }

            #endregion
        }
    }
}

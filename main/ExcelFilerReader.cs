using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace main
{
    class ExcelFileReader
    {
        public ExcelFileReader()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var firstFile = Directory.GetFiles(currentDirectory + "/data/").FirstOrDefault();

            Console.WriteLine(firstFile);

            var collection = ReadExcelFileContent(firstFile);
         
            Console.WriteLine("Number of items : " + collection.Count());
            var headerLine = ConsoleTableCreator.CreateHeaderLine();
            var separatorLine = ConsoleTableCreator.CreateSeparator(headerLine.Count());
            var codes = collection.Select(b => b.TradeIdentifier).Where(a => a != string.Empty).Distinct();
            collection = collection.OrderBy(a => a.Name).ToList();
/*             foreach(var itemCode in codes)
            {
                Console.WriteLine(itemCode);
            } */

            //Console.WriteLine("\nSelect stock identifier for more information:");
            //var readCode = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine(headerLine);
            Console.WriteLine(separatorLine);

            var groupedList = collection.GroupBy(a => a.Name, a => a);
            foreach (var item in groupedList)
            {
                foreach (var line in item)
                {
                    if (line.TypeOfAction.Contains("Myynti") || line.TypeOfAction.Contains("Osto"))
                    {
                        /* TODO string truncation to fit column width */
                        var contentLine = ConsoleTableCreator.CreateContentLine(line.Name.ToString(),
                                                                                line.TypeOfAction.ToString(),
                                                                                line.Quantity.ToString(),
                                                                                line.Price.ToString());
                        ConsoleTableCreator.PrintLine(contentLine);
                    }
                }
                Console.WriteLine(separatorLine);
            }
        }

        private void CreateContent(TradeInformation info)
        {
            var contentLine = ConsoleTableCreator.CreateContentLine(info.Name.ToString(),
                                                                    info.TypeOfAction.ToString(),
                                                                    info.Quantity.ToString(),
                                                                    info.Price.ToString());
            ConsoleTableCreator.PrintLine(contentLine);
        }

        public IEnumerable<TradeInformation> ReadExcelFileContent(string fileName)
        {
            var collection = new List<TradeInformation>();
            
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    do
                    {
                        while (reader.Read())
                        {
                            var info = new TradeInformation(recordNumber: reader.GetString(0),
                                                            typeOfAction: reader.GetString(1),
                                                            name: reader.GetString(2),
                                                            isinCode: reader.GetString(3),
                                                            tradeIdentifier: reader.GetString(4),
                                                            stockExchange: reader.GetString(5),
                                                            dateOfAction: reader.GetString(6),
                                                            paymentDate: reader.GetString(7),
                                                            quantity: reader.GetValue(8),
                                                            price: reader.GetValue(9),
                                                            currency: reader.GetDouble(9),
                                                            currencyRate: reader.GetString(10),
                                                            marketValue: reader.GetValue(12),
                                                            commision: reader.GetValue(13),
                                                            totalTransactionCost: reader.GetValue(14));
                            collection.Add(info);
                        }
                    } while (reader.NextResult());
                }
            }
            
            return collection;
        }
    }
}
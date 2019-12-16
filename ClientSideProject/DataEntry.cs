using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientSideGrid
{
    class DateEntry
    {
        public readonly string assetName;
        public readonly string date;
        public readonly string bidPrice;
        public readonly string askPrice;

        public DateEntry(string assetName, string date, string bidPrice, string askPrice)
        {
            this.askPrice = askPrice;
            this.date = date;
            this.bidPrice = bidPrice;
            this.assetName = assetName;
        }

        public static DateEntry ParseLine(string theLine)
        {
            var tokens = theLine.Replace("[", "").Replace("]", "").Split(' ');
            if (tokens[0].Equals("Best"))
                tokens = tokens.Skip(1).ToArray();
            return new DateEntry(tokens[0], tokens[1], tokens[2], tokens[3]);
        }

        public static void PrintGrid(Dictionary<String, DateEntry> itemsByName)
        {
            Console.Clear();
            Console.WriteLine("Asset Name     Time                          Bid            Ask");
            foreach (DateEntry dataEntry in itemsByName.Values)
            {
                Console.Write(PadSpaces(dataEntry.assetName, 15));
                Console.Write(PadSpaces(dataEntry.date, 30));
                Console.Write(PadSpaces(dataEntry.bidPrice, 15));
                Console.Write(PadSpaces(dataEntry.askPrice, 0));
                Console.WriteLine("");
            }
            System.Threading.Thread.Sleep(100);
        }

        private static string PadSpaces(string str, int totalAmount)
        {
            while (str.Length < totalAmount)
                str += " ";
            return str;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ClientSideGrid
{
    class ClientSide
    {

        static void Main(string[] args)
        {
            Dictionary<String, DateEntry> itemsByName = new Dictionary<string, DateEntry>();
            string line = "";
            DateEntry parsedItem;

            using (var client = new TcpClient("79.125.80.209", 9999))   // Server
            using (var stream = client.GetStream())                         // Closes everything safely in the end
                while (true)
                {
                    try
                    {
                        var bytes = new List<byte>();
                        while (true)
                        {
                            int currentByte = stream.ReadByte();
                            stream.WriteByte(1);
                            // Console.WriteLine(currentByte);
                            if (currentByte == -1)
                                return;
                            bytes.Add((byte)currentByte);
                            if (currentByte == 0)
                                break;
                        }
                        line = new string(UTF8Encoding.UTF8.GetChars(bytes.ToArray()));

                        //Console.WriteLine(line);//Checking what we get from server

                        parsedItem = DateEntry.ParseLine(line);
                        if (itemsByName.ContainsKey(parsedItem.assetName))
                            itemsByName.Remove(parsedItem.assetName);
                        itemsByName.Add(parsedItem.assetName, parsedItem);

                        DateEntry.PrintGrid(itemsByName);
                    }
                    catch
                    { }
                }
        }
    }
}

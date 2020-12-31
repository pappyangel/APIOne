using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System;
using cocktails.models;

namespace cocktails.DB
{
    public class FileDB
    {

        public List<Item> InitialDBLoad()
        {
            List<Item> _itemList = new();
            _itemList.Add(new Item() { Id = 1, Name = "Martini", Price = 15.25, Rating = 4.8 });
            _itemList.Add(new Item() { Id = 2, Name = "Manhattan", Price = 15.50, Rating = 4.5 });
            _itemList.Add(new Item() { Id = 3, Name = "Domestic Beer", Price = 8.50, Rating = 3.0 });
            _itemList.Add(new Item() { Id = 4, Name = "Wine", Price = 10.00, Rating = 3.5 });

            return _itemList;

        }

        public List<Item> ReadListFromFile()
        {
            // in method variable
            List<Item> _items = new();

            // read file
            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";
            string jsonString = File.ReadAllText(fileName);

            // deserialize into class array
            //var options = new JsonSerializerOptions { WriteIndented = true, };
            //_items  = JsonSerializer.Deserialize<List<Item>>(jsonString, options);
            _items = JsonSerializer.Deserialize<List<Item>>(jsonString);

            // pass list back to caller

            //_items.Sort(); 
            return _items;


        }

        public void WriteListtoFile(List<Item> _items)
        {

            //below two lines are for debugging needs only
            var options = new JsonSerializerOptions { WriteIndented = true, };
            string jsonStringPretty = JsonSerializer.Serialize(_items, options);

            string jsonString = JsonSerializer.Serialize(_items);

            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";
            string prettyFileName = dirName + "/PrettyAPIData.txt";

            // Create Directory creates dir if it does not exists, otherwise it does nothing
            Directory.CreateDirectory(dirName);

            //if (!File.Exists(fileName)) File.Create()


            using FileStream APIDataFileStream = File.Open(fileName, FileMode.OpenOrCreate);
            APIDataFileStream.SetLength(0);

            //File.WriteAllText(fileName, jsonString);

            byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
            APIDataFileStream.Write(info, 0, info.Length);

            using FileStream APIDataFileStreamPretty = File.Open(prettyFileName, FileMode.Append);
            info = new UTF8Encoding(true).GetBytes(jsonStringPretty);
            APIDataFileStreamPretty.Write(info, 0, info.Length);

            APIDataFileStream.Close();

        }

    } // end of class fileDB

} // end of namespace
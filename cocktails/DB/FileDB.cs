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
            //Note: this initial list is already sorted by Id
            _itemList.Add(new Item() { Id = 1, Name = "Martini", Price = 15.25, Rating = 4.8 });
            _itemList.Add(new Item() { Id = 2, Name = "Manhattan", Price = 15.50, Rating = 4.5 });
            _itemList.Add(new Item() { Id = 3, Name = "Domestic Beer", Price = 8.50, Rating = 3.0 });
            _itemList.Add(new Item() { Id = 4, Name = "Wine", Price = 10.00, Rating = 3.5 });

            return _itemList;

        }
        public void InsertItemintoList(Item _item)
        {
            // read list from file
            List<Item> _ItemListFromDisk = ReadListFromFile();

            // get max id from list
            Item _LastItem = new();
            int _nextItemID = 1;

            if (_ItemListFromDisk.Count > 0)
            {
                _LastItem = _ItemListFromDisk[_ItemListFromDisk.Count - 1];
                _nextItemID = _LastItem.Id + 1;
            }
            _item.Id = _nextItemID;

            // add record to list with values from put
            _ItemListFromDisk.Add(_item);

            // write list to file
            WriteListtoFile(_ItemListFromDisk);

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

            _items.Sort();
            return _items;


        }

        public  void DeleteItemfromListById(int _Id)
        {

            // read list from file
            List<Item> _ItemListFromDisk = ReadListFromFile();

           //delete the item if it exists from post
            if (_ItemListFromDisk.Exists(x => x.Id == _Id))
            {
                // remove item from list
                _ItemListFromDisk.RemoveAll(p => p.Id == _Id);

            }

            //write list to file
            WriteListtoFile(_ItemListFromDisk);

        }
        public  void UpdateItemInListById(Item _item)
        {

            // read list from file
            List<Item> _ItemListFromDisk = ReadListFromFile();

            if (_ItemListFromDisk.Exists(x => x.Id == _item.Id))
            {
                Item _ItemtoUpdate = _ItemListFromDisk.Find(p => p.Id ==_item.Id);

                _ItemtoUpdate.Name = _item.Name;
                _ItemtoUpdate.Price = _item.Price;
                _ItemtoUpdate.Rating = _item.Rating;

                // remove item from list
                _ItemListFromDisk.RemoveAll(p => p.Id == _item.Id);

                // add record to list with values from put
                _ItemListFromDisk.Add(_ItemtoUpdate);

            }

            //write list to file
            WriteListtoFile(_ItemListFromDisk);


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

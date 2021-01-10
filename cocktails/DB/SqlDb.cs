using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using cocktails.models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cocktails.DB
{
    public class SqlDb
    {
        private readonly ILogger<SqlDb> _logger;
        private readonly IConfiguration _configuration;

        //public SqlDb(ILogger<SqlDb> logger, IConfiguration configuration)
        public SqlDb(IConfiguration configuration)
        {
            {
                //  _logger = logger;
                _configuration = configuration;
            }
        }


        public SqlConnection GetSQLCn()
        {
            var builder = new SqlConnectionStringBuilder(
                _configuration["ConnectionStrings:defaultConnection"]);
            // _configuration["ConnectionStrings:pluralConnection"]);

            //builder.Password = _configuration.GetValue<string>("plural-sqlPWD");
            var keyVaultSecretLookup = _configuration["AzureKeyVaultSecret:defaultSecret"];
            builder.Password = _configuration.GetValue<string>(keyVaultSecretLookup);


            SqlConnection pluralCn = new SqlConnection(builder.ConnectionString);
            //pluralCn.Open();
            return pluralCn;

        }

        public List<Item> GetAllItems()
        {
            // define variables
            string tblName = "items";
            string qry = $"Select * from {tblName} order by Id";
            SqlCommand command;
            SqlDataReader dataReader;

            // set up objects
            List<Item> items = new();
            SqlConnection SQLCn = GetSQLCn();
            SQLCn.Open();

            command = new SqlCommand(qry, SQLCn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {                 
                items.Add(new Item() {  Id = dataReader.GetInt32(0),
                                        Name = dataReader.GetString(1),
                                        Price = dataReader.GetDecimal(2),
                                        Rating = dataReader.GetDecimal(3) });
            }

            // Tim beleives these are superfluous
            dataReader.Close();
            command.Dispose();
            SQLCn.Close();

            return items;


            // to do  action results

        }

        public List<Item> GetAllItemsJSON()
        {
            // local var items
            string tblName = "items";
            List<Item> _items = new();


            SqlConnection _pluralCn = GetSQLCn();
            _pluralCn.Open();

            string qry = $"Select * from {tblName} order by Id for json auto";
            var sqlCommand = new SqlCommand(qry, _pluralCn);

            //List<Item> sqlItems = sqlCommand.ExecuteReader
            object jsonObject = sqlCommand.ExecuteScalar();

            _items = JsonSerializer.Deserialize<List<Item>>(jsonObject.ToString());
            return _items;


            // to do  action results




        }

    }  // end of class
} // end of ns

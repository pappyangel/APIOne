using System.Collections.Generic;
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
         public SqlDb( IConfiguration configuration)
        {
            {
              //  _logger = logger;
                _configuration = configuration;
            }
        }


        public SqlConnection  GetSqlCn()
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
        // local var items
        string tblName = "items";
        List<Item> _items = new();


        SqlConnection _pluralCn =  GetSqlCn();
        _pluralCn.Open();

        string qry = $"Select * from {tblName} order by Id for json auto";
        var sqlCommand = new SqlCommand(qry, _pluralCn);

        //List<Item> sqlItems = sqlCommand.ExecuteReader
        object jsonObject =  sqlCommand.ExecuteScalar();

        _items = JsonSerializer.Deserialize<List<Item>>(jsonObject.ToString());
        return _items;


        // to do  action results




    }

    }  // end of class
} // end of ns

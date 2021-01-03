using System.Collections.Generic;
using System.Data.SqlClient;
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
                _configuration["ConnectionStrings:pluralConnection"]);
            builder.Password = _configuration.GetValue<string>("plural-sqlPWD");

            SqlConnection pluralCn = new SqlConnection(builder.ConnectionString);
            //pluralCn.Open();
            return pluralCn;

        }

    public void GetAllItems()
    {
        // local var items
        string tblName = "items";

        SqlConnection _pluralCn =  GetSqlCn();
        _pluralCn.Open();

        string qry = $"Select * from {tblName} order by Id for json auto";
        var sqlCommand = new SqlCommand(qry, _pluralCn);

        //List<Item> sqlItems = sqlCommand.ExecuteReader
        object jsonObject =  sqlCommand.ExecuteScalar();

        // sel sql order by id
        // action results
        // return list of items



    }

    }  // end of class
} // end of ns

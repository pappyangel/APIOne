using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;
using cocktails.models;
using Microsoft.Extensions.Configuration;

namespace cocktails.DB
{
    public class SqlDb
    {
        // private readonly ILogger<SqlDb> _logger;
        private readonly IConfiguration _configuration;

        private List<Item> sqlItems = new();        
        private string tblName = "Items";
        private string viewName = "ItemsVw";

        //private string selectClause = "Select id, name, price, rating, coalesce(imagepath,'', imagepath) ";
        private string selectClause = "Select id, name, price, rating, imagepath ";

        //public SqlDb(ILogger<SqlDb> logger, IConfiguration configuration)
        public SqlDb(IConfiguration configuration)
        {
            // _logger = logger
            _configuration = configuration;

        }

        public SqlConnection GetSQLCn()
        {
            var builder = new SqlConnectionStringBuilder(
                _configuration["ConnectionStrings:defaultConnection"]);
            

            
            var keyVaultSecretLookup = _configuration["AzureKeyVaultSecret:defaultSecret"];
            builder.Password = _configuration.GetValue<string>(keyVaultSecretLookup);


            SqlConnection sqlDBCn = new SqlConnection(builder.ConnectionString);
            //pluralCn.Open();
            return sqlDBCn;

        }

        public void ExecuteQuery(string qry)
        {
            SqlCommand command;
            SqlDataReader dataReader;            

            SqlConnection SQLCn = GetSQLCn();
            SQLCn.Open();
            command = new SqlCommand(qry, SQLCn);
            dataReader =  command.ExecuteReader();

            while (dataReader.Read())
            {                
                sqlItems.Add(new Item()
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    Price = dataReader.GetDecimal(2),
                    Rating = dataReader.GetDecimal(3),     
                    ImagePath = dataReader.GetString(4)                    
                });
            }
            
            // Tim beleives these are superfluous
            dataReader.Close();
            command.Dispose();
            SQLCn.Close();
        }
 public async Task<int> ExecuteQueryAsync(string qry)
        {
            SqlCommand command;
            SqlDataReader dataReader;            

            SqlConnection SQLCn = GetSQLCn();
            await SQLCn.OpenAsync();
            command = new SqlCommand(qry, SQLCn);
            dataReader =  await command.ExecuteReaderAsync();

            while (dataReader.Read())
            {                
                sqlItems.Add(new Item()
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    Price = dataReader.GetDecimal(2),
                    Rating = dataReader.GetDecimal(3),     
                    ImagePath = dataReader.GetString(4)                    
                });
            }

            // Tim beleives these are superfluous
            dataReader.Close();
            command.Dispose();
            SQLCn.Close();

            return 1;

        }
        public List<Item> GetItemsById(int id)
        {            
            string qryId = selectClause + $"from {viewName} where Id = {id} order by Id";            

            ExecuteQuery(qryId);

            return sqlItems;

        } // end get by price


        public List<Item> GetItemsByPrice(decimal price)
        {         
            string qryPrice = selectClause + $"from {viewName} where price <= {price} order by Id";            

            ExecuteQuery(qryPrice);

            return sqlItems;

        } // end get by price

        public List<Item> GetItemsByRating(decimal rating)
        {            
            string qryRating = selectClause + $"from {viewName} where rating >= {rating} order by Id";

            ExecuteQuery(qryRating);

            return sqlItems;

        } // end get by rating

        public async Task<List<Item>> GetAllItems()
        {
            // define variables          
            string qryAllItems = selectClause + $"from {viewName} order by Id";

            await ExecuteQueryAsync(qryAllItems);

            return sqlItems;

        }

        private int CRUD(string sqlStatetment)
        {
            SqlCommand command;
            int rowsAffected;

            SqlConnection SQLCn = GetSQLCn();
            SQLCn.Open();

            command = new SqlCommand(sqlStatetment, SQLCn);
            command.CommandType = CommandType.Text;
            rowsAffected = command.ExecuteNonQuery();

            command.Dispose();
            SQLCn.Close();

            return rowsAffected;

        }
        public int DeleteItembyId(int id)
        {
            int crudResult;         
            string sql = $"Delete from {tblName} where Id = {id}";

            crudResult = CRUD(sql);

            return crudResult;
        }
        
        public int UpdateItembyId(Item item)
        {
            int crudResult;            
            string sql = $"Update t Set t.name = '{item.Name}', t.price = {item.Price}, t.rating = {item.Rating}, t.ImagePath = '{item.ImagePath}'"
             + $" From {tblName} t where t.id = {item.Id}";

            crudResult = CRUD(sql);

            return crudResult;
        }
        public int InsertItem(Item item)
        {
            int crudResult;            
            string sql = $"Insert into {tblName} (Name, Price ,Rating) values ('{item.Name}', {item.Price}, {item.Rating})";

            crudResult = CRUD(sql);

            return crudResult;
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

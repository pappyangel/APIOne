using System.Collections.Generic;
using System.Linq;
using cocktails.models;
using Microsoft.AspNetCore.Mvc;
using cocktails.DB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace cocktails.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("Cocktails")]

    public class CocktailController : ControllerBase
    {
        private readonly ILogger<CocktailController> _logger;
        private readonly IConfiguration _configuration;
        public CocktailController(ILogger<CocktailController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public List<Item> GetAllCocktails()
        {

            FileDB fileDB = new();
            List<Item> _itemList = fileDB.ReadListFromFile();

            _logger.LogInformation("You asked for a list of all items");

            SqlDb _sqlDb = new(_configuration);
            _itemList = _sqlDb.GetAllItems();


            return _itemList;

        }

        // cocktail/resetdb
        [HttpGet("resetdb")]
        public List<Item> ResetCocktailsDB()
        {
            FileDB fileDB = new();

            List<Item> _itemList = fileDB.InitialDBLoad();
            fileDB.WriteListtoFile(_itemList);

            _logger.LogInformation("A DB Reset request was made.");

            return _itemList;
        }

        // cocktail/id/$int
        [HttpGet("id/{id:int}")]
        public List<Item> GetCocktailsById(int Id)
        {
            // FileDB fileDB = new();
            // List<Item> _itemList = fileDB.ReadListFromFile();
            // return _itemList.Where(_itemList => _itemList.Id == _Id);

            SqlDb sqlDb = new(_configuration);
            List<Item> itemList = sqlDb.GetItemsById(Id);

            _logger.LogInformation("Received request to return item: {@int}", Id);

            return itemList;

        } // end get by ID

        // cocktail/rating/$double
        [HttpGet("rating/{rating:decimal}")]
        public List<Item> GetCocktailsByRating(decimal rating)
        {
            // To Do: move logic into new class in FileDB, retunr List<Item>
            //FileDB fileDB = new();
            //List<Item> _itemList = fileDB.ReadListFromFile();
            //return _itemList.Where(_itemList => _itemList.Rating >= _Rating);

            SqlDb sqlDb = new(_configuration);
            List<Item> itemList = sqlDb.GetItemsByRating(rating);

            _logger.LogInformation("Received request to return item by this rating: {@decimal}", rating);

            return itemList;

        }
        // cocktail/rating/$double
        [HttpGet("price/{price:decimal}")]
        public List<Item> GetCocktailsByPrice(decimal price)
        {
            // To Do: move logic into new class in FileDB, retunr List<Item>
            //FileDB fileDB = new();
            //List<Item> _itemList = fileDB.ReadListFromFile();
            //return _itemList.Where(_itemList => _itemList.Rating >= _Rating);

            SqlDb sqlDb = new(_configuration);
            List<Item> itemList = sqlDb.GetItemsByPrice(price);

            _logger.LogInformation("Received request to return item by this rating: {@decimal}", price);

            return itemList;

        }


        // cocktail/Post  -- insert new Item
        [HttpPost]
        public string AddCocktail(Item _item)
        {
            // FileDB fileDB = new();
            // fileDB.InsertItemintoList(_item);

            int rowsAffected;

            SqlDb sqlDb = new(_configuration);
            rowsAffected = sqlDb.InsertItem(_item);

            _logger.LogInformation("Received request to add this item: {@Item}", _item);

            return $"Row(s) inserted were: {rowsAffected}";


        }

        // cocktail/Put  -- update  Item by id
        [HttpPut]
        public string UpdateCocktail(Item item)
        {
            int rowsAffected;
            // FileDB fileDB = new();
            // fileDB.UpdateItemInListById(_item);

            SqlDb sqlDb = new(_configuration);
            rowsAffected = sqlDb.UpdateItembyId(item);

            _logger.LogInformation("Received request to update this item: {@Item}", item);

            return $"Row(s) updated were: {rowsAffected}";

        }

        // cocktail/Post  -- Delete Item
        [HttpDelete("id/{id:int}")]
        //[HttpDelete]
        public string DeleteCocktail(int id)
        {
            int rowsAffected;

            // FileDB fileDB = new();
            // fileDB.DeleteItemfromListById(_Id);            

            SqlDb sqlDb = new(_configuration);
            rowsAffected = sqlDb.DeleteItembyId(id);

            _logger.LogInformation("Received request to delete by this item id: {@int}", id);

            return $"Row(s) deleted were: {rowsAffected}";

        }
    }  // end of class controller

} // end of namespace

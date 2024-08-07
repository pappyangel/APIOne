using System.Collections.Generic;
using System.Linq;
using cocktails.models;
using Microsoft.AspNetCore.Mvc;
using cocktails.DB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<Item>>> GetAllCocktails()
        {
            List<Item> itemList;

            // FileDB fileDB = new();
            // List<Item> itemList = fileDB.GetAllItems();

            SqlDb sqlDb = new(_configuration);
            itemList = await sqlDb.GetAllItems();
            _logger.LogInformation("You asked for a list of all items");

            if (itemList.Count == 0)
            {
                //return BadRequest($"Item not found: {Id}");
                // reply with http code 204
                return NoContent();

            }
            // otherwise send back list -- best practice

            return itemList;

        }

        // cocktail/resetdb
        [HttpGet("resetdb")]
        public void ResetCocktailsFileDB()
        {
            // this method was orignally for
            // internal maintenace when we
            // were creating FileDB

            // List<Item> itemList;

            // // FileDB fileDB = new();
            // SqlDb sqlDb = new(_configuration);

            // itemList = sqlDb.GetAllItems();

            // // fileDB.InitialDBLoad(itemList);

            _logger.LogInformation("A FileDB Reset request was made.");

        }

        // cocktail/id/$int
        [HttpGet("id/{id:int}")]
        public async Task<ActionResult<List<Item>>> GetCocktailsById(int Id)
        {
            // FileDB fileDB = new();
            // List<Item> fileItemList = fileDB.GetItemsbyId(Id);

            SqlDb sqlDb = new(_configuration);
            List<Item> itemList = await sqlDb.GetItemsById(Id);

            if (itemList.Count == 0)
                return NoContent();

            _logger.LogInformation("Received request to return item: {@int}", Id);

            return itemList;

        } // end get by ID


        [HttpGet("rating/{rating:decimal}")]
        public async Task<ActionResult<List<Item>>> GetCocktailsByRating(decimal rating)
        {
            // FileDB fileDB = new();
            // List<Item> fileItemList = fileDB.GetItemsbyRating(rating);

            SqlDb sqlDb = new(_configuration);
            List<Item> itemList = await sqlDb.GetItemsByRating(rating);

            if (itemList.Count == 0)
                return NoContent();

            _logger.LogInformation("Received request to return item by this rating: {@decimal}", rating);

            return itemList;

        }

        [HttpGet("price/{price:decimal}")]
        public async Task<ActionResult<List<Item>>> GetCocktailsByPrice(decimal price)
        {
            // FileDB fileDB = new();
            // List<Item> fileItemList = fileDB.GetItemsbyPrice(price);

            SqlDb sqlDb = new(_configuration);
            List<Item> itemList = await sqlDb.GetItemsByPrice(price);

            if (itemList.Count == 0)
                return NoContent();

            _logger.LogInformation("Received request to return item by this price or lower: {@decimal}", price);

            return itemList;

        }


        // cocktail/Post  -- insert new Item
        [HttpPost]
        public async Task<ActionResult<string>> AddCocktail(Item _item)
        {
            int rowsAffected;

            // FileDB fileDB = new();
            // fileDB.InsertItemintoList(_item);            

            SqlDb sqlDb = new(_configuration);
            rowsAffected = await sqlDb.InsertItem(_item);

            _logger.LogInformation("Received request to add this item: {@Item}", _item);

            return $"Row(s) inserted were: {rowsAffected}";

        }

        // cocktail/Put  -- update  Item by id
        [HttpPut]
        public async Task<ActionResult<string>> UpdateCocktail(Item item)
        {
            int rowsAffected;
            // FileDB fileDB = new();
            // fileDB.UpdateItemInListById(item);

            SqlDb sqlDb = new(_configuration);
            rowsAffected = await sqlDb.UpdateItembyId(item);

            if (rowsAffected == 0)
                return NotFound();


            _logger.LogInformation("Received request to update this item: {@Item}", item);

            return $"Row(s) updated were: {rowsAffected}";

        }

        // cocktail/Post  -- Delete Item
        [HttpDelete("id/{id:int}")]
        //[HttpDelete]
        public async Task<ActionResult<string>> DeleteCocktail(int id)
        //public async Task<ActionResult<string>> DeleteCocktail(Item item)

        {
            int rowsAffected;

            // FileDB fileDB = new();
            // fileDB.DeleteItemfromListById(id);            

            SqlDb sqlDb = new(_configuration);
            rowsAffected = await sqlDb.DeleteItembyId(id);

            if (rowsAffected == 0)
                return NotFound();
                

            _logger.LogInformation("Received request to delete by this item id: {@int}", id);

            return $"Row(s) deleted were: {rowsAffected}";

        }
    }  // end of class controller

} // end of namespace

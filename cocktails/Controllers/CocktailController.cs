using System.Collections.Generic;
using System.Linq;
using cocktails.models;
using Microsoft.AspNetCore.Mvc;
using cocktails.DB;


namespace cocktails.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("Cocktails")]

    public class CocktailController : ControllerBase
    {
        // private readonly Cocktail cocktail;
        List<Cocktail> CocktailList = new()
        {
            new Cocktail { ID = 1, Name = "Manhattan", Price = 14.25, Rating = 4.8 },
            new Cocktail { ID = 2, Name = "Cosmo", Price = 12.75, Rating = 4.0 },
            new Cocktail { ID = 3, Name = "White Russian", Price = 17.50, Rating = 4.2 },
            new Cocktail { ID = 4, Name = "Old Fashion", Price = 16.50, Rating = 4.5 }
        };


        [HttpGet]
        public IEnumerable<Item> GetAllCocktails()
        {
            FileDB fileDB = new();
            List<Item> _itemList = fileDB.ReadListFromFile();

            return _itemList;

        }

        // cocktail/resetdb
        [HttpGet("resetdb")]
        public IEnumerable<Item> ResetCocktailsDB()
        {
            FileDB fileDB = new();

            List<Item> _itemList = fileDB.InitialDBLoad();
            fileDB.WriteListtoFile(_itemList);

            return _itemList;
        }

        // cocktail/id/$int
        [HttpGet("id/{_ID:int}")]
        public IEnumerable<Item> GetCocktailsByID(int _Id)
        {
            FileDB fileDB = new();
            List<Item> _itemList = fileDB.ReadListFromFile();

            return _itemList.Where(_itemList => _itemList.Id == _Id);
        }

        // cocktail/rating/$double
        [HttpGet("rating/{_Rating:double}")]
        public IEnumerable<Item> GetCocktailsByRating(double _Rating)
        {
            FileDB fileDB = new();
            List<Item> _itemList = fileDB.ReadListFromFile();

            return _itemList.Where(_itemList => _itemList.Rating >= _Rating);

        }

        // cocktail/Post  -- insert new Item
        [HttpPost]
        public void AddCocktail(Item _item)
        {

            FileDB fileDB = new();
            fileDB.InsertItemintoList(_item);


        }
        // cocktail/Put  -- update  Item by id
        [HttpPut]
        public void UpdateCocktail(Item _item)
        {

            FileDB fileDB = new();
            fileDB.UpdateItemInListById(_item);


        }

        // cocktail/Post  -- Delete Item
        [HttpDelete("id/{_ID:int}")]
        //[HttpDelete]
        public void DeleteCocktail(int _Id)
        {

            FileDB fileDB = new();
            fileDB.DeleteItemfromListById(_Id);
        }
    }  // end of class controller

} // end of namespace

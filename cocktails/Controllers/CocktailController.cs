using System.Collections.Generic;
using System.Linq;
using cocktails.models;
using Microsoft.AspNetCore.Mvc;

namespace cocktails.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<Cocktail> GetCocktails()
        {
            return CocktailList;
        }

        [HttpGet("{_ID}")]
        public IEnumerable<Cocktail> GetCocktailsByID(int _ID)
        {
            return CocktailList.Where(Cocktail => Cocktail.ID == _ID);
        }
        [Route("Cocktail/Rating")]
        [HttpGet("{_Rating}")]
        public IEnumerable<Cocktail> GetCocktailsByRating(double _Rating)
        {
            return CocktailList.Where(Cocktail => Cocktail.Rating >= _Rating);
        }



    }  // end of class controller

} // end of namespace
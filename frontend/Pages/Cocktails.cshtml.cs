using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using frontend.models;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;

namespace frontend.Pages
{
    [BindProperties]
    public class CocktailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ICocktailRepository _cocktailRepository;

        public CocktailsModel(ILogger<IndexModel> logger,ICocktailRepository cocktailRepository)
        {
            _logger = logger;
            _cocktailRepository = cocktailRepository;
        }
        public List<Item> cocktailList = new();
        public string dog = "Cosmo";
        
        public void OnGet()
        {         
            // HttpClient APIclient = new HttpClient();

            // cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");       

            //_cocktailRepository.
            cocktailList = (List<Item>)_cocktailRepository;


            // create an instance of CocktailRepositoy Foo
            // don't want  call Foo.GetItems                            

        }

        public void OnPost()
        {
            int dog = 1;
            dog++;
        }

    }
}

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

        public CocktailsModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<Item> cocktailList = new();
        public string dog = "Cosmo";
        
        public async Task OnGet()
        {         
            HttpClient APIclient = new HttpClient();

            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");                                    

        }

        public void OnPost()
        {
            int dog = 1;
            dog++;
        }

    }
}

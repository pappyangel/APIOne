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

        public async void OnGet()
        {
            
            List<Item> junk = new();
            HttpClient APIclient = new HttpClient();

            APIclient.DefaultRequestHeaders.Accept.Clear();
            APIclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string url = "https://localhost:5001/cocktails";
            url = "http://localhost:5000/cocktails";                        

            HttpResponseMessage response = await APIclient.GetAsync(url);

            var APIData = await response.Content.ReadAsStringAsync();            

            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

            //junk = JsonSerializer.Deserialize(APIData,List<Item>,);
            // cocktailList = JsonSerializer.Deserialize<List<Item>>(APIData);
            

        }
    }
}

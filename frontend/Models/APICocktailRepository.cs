using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace frontend.models
{

    public class APICocktailRepository : ICocktailRepository
    {
        private List<Item> cocktailList { get; set; }
        private HttpClient APIclient = new HttpClient();

        // public APICocktailRepository()
        // {
        //     GetItemsAsync(); 
        // }

        public async Task<List<Item>> GetItemsAsync()
        {
            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

            return cocktailList;

        }

        public Item GetItem(int Id)
        {
            throw new System.NotImplementedException();
        }
    }

}
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace frontend.models
{

    public class APICocktailRepository : ICocktailRepository
    {
        public List<Item> cocktailList { get; private set; }

        public APICocktailRepository()
        {
            GetItemsAsync(); 
        }

        public async void GetItemsAsync()
        {
            HttpClient APIclient = new HttpClient();

            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

        }

        public Item GetItem(int Id)
        {
            throw new System.NotImplementedException();
        }
    }

}
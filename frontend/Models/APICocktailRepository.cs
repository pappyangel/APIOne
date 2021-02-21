using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace frontend.models
{

    public class APICocktailRepository : ICocktailRepository
    {
        private List<Item> cocktailList { get; set; }
        private HttpClient APIclient = new HttpClient();

        public APICocktailRepository()
        {
            int dog = 0;
            dog++;
            //GetItemsAsync(); 
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

            return cocktailList;

        }

        public Item GetItem(int Id)
        {
            Item item = cocktailList.Find(e => e.Id == Id);
            return item;
            //return cocktailList.Find(e => e.Id == Id);
        }

        public async Task<List<Item>> UpdateItemAsync(Item updatedItem)
        {            
            var jsonItem = JsonSerializer.Serialize(updatedItem);
            var httpContent = new StringContent(jsonItem, Encoding.UTF8, "application/json");
            var url = "http://localhost:5000/cocktails";
            var response = await APIclient.PutAsync(url, httpContent);            
            
            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

            return cocktailList;
        }        

        public async Task<List<Item>> AddItemAsync(Item cocktailToAdd)
        {
            var jsonItem = JsonSerializer.Serialize(cocktailToAdd);
            var httpContent = new StringContent(jsonItem, Encoding.UTF8, "application/json");
            var url = "http://localhost:5000/cocktails";
            var response = await APIclient.PostAsync(url, httpContent);
            
            
            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

            return cocktailList;
        }

        public async Task<List<Item>> DeleteItemAsync(int cocktailIdToDelete)
        {            
            var url = "http://localhost:5000/cocktails";
            var deleteUrl = url + "/id/" + cocktailIdToDelete;
            var response = await APIclient.DeleteAsync(deleteUrl);
            
            
            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>("http://localhost:5000/cocktails");

            return cocktailList;

         
        }
    }

}
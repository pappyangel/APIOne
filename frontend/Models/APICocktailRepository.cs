using System;
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

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/cocktails");
            var response1 = await APIclient.SendAsync(request);           


            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
            {
                cocktailList = await response1.Content.ReadFromJsonAsync<List<Item>>();
            }
            else
            if (response1.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                //use case where API call was good, but there was no data
                //do something here to have screen display no results, please add some!

            }            

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
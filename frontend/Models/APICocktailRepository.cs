using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace frontend.models
{

    public class APICocktailRepository : ICocktailRepository
    {
        private readonly IConfiguration _configuration;
        private List<Item> cocktailList { get; set; }
        private HttpClient APIclient = new HttpClient();

        private string apiUrl;

        public APICocktailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration["APIProductionUrl"];
        }
        
        public async Task<List<Item>> GetItemsAsync()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
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

            var response = await APIclient.PutAsync(apiUrl, httpContent);

            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>(apiUrl);

            return cocktailList;
        }

        public async Task<List<Item>> AddItemAsync(Item cocktailToAdd)
        {
            var jsonItem = JsonSerializer.Serialize(cocktailToAdd);
            var httpContent = new StringContent(jsonItem, Encoding.UTF8, "application/json");

            var response = await APIclient.PostAsync(apiUrl, httpContent);


            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>(apiUrl);

            return cocktailList;
        }

        //public async Task<List<Item>> DeleteItemAsync(int cocktailIdToDelete)
        public async Task<List<Item>> DeleteItemAsync(Item cocktailIdToDelete)
        {

            var deleteUrl = apiUrl + "/id/" + cocktailIdToDelete.Id;
            var response = await APIclient.DeleteAsync(deleteUrl);

            // var jsonItem = JsonSerializer.Serialize(cocktailIdToDelete);
            // var httpContent = new StringContent(jsonItem, Encoding.UTF8, "application/json");            
            // var response = await APIclient.DeleteAsync(apiUrl, httpContent)


            cocktailList = await APIclient.GetFromJsonAsync<List<Item>>(apiUrl);

            return cocktailList;


        }
    }

}
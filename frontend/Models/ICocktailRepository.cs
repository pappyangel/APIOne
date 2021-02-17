using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace frontend.models
{

public interface ICocktailRepository
{    
    //void GetItemsAsync();
    Task<List<Item>> GetItemsAsync();

    Item GetItem(int Id);

    Task<List<Item>> UpdateItemAsync(Item updatedItem);
    
    //Task<List<Item>> UpdateItemAsync(Item cocktailToEdit);
    }

}
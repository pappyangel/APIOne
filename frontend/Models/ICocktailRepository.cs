using System.Collections.Generic;
using System.Threading.Tasks;

namespace frontend.models
{

public interface ICocktailRepository
{    
    void GetItemsAsync();
    // Task<List<Item>> GetItemsAsync();

    Item GetItem(int Id);

}

}
using System.Collections.Generic;

namespace frontend.models
{

public interface ICocktailRepository
{
    List<Item> GetItems();

    Item GetItems(int Id);

}

}
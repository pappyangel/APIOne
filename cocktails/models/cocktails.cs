using System;

namespace cocktails.models
{
    
     public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
    }

    public record Cocktail
    {
        public int ID { get; init; }

        public string Name { get; init; }

        public double Price { get; init; }

        public double Rating { get; init; }

    }
}

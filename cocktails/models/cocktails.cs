using System;

namespace cocktails.models
{
    public record Cocktail
    {
        public int ID { get; init; }

        public string Name { get; init; }

        public double Price { get; init; }

        public double Rating { get; init; }

    }
}

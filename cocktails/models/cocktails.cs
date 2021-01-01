using System;

namespace cocktails.models
{

    public class Item: IComparable<Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        public int CompareTo(Item compareItem)
            {
                // A null value means that this object is greater.
                if (compareItem == null)
                    return 1;

                else
                    return this.Id.CompareTo(compareItem.Id);
            }

        public override string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Price: " + Price + ", Rating: " + Rating;
        }


    }

    public record Cocktail
    {
        public int ID { get; init; }

        public string Name { get; init; }

        public double Price { get; init; }

        public double Rating { get; init; }

    }
}

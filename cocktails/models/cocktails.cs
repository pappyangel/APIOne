using System;

namespace cocktails.models
{
    public class Item : IComparable<Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
    

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

}

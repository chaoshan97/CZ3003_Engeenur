public class Item
{
    public string name { get; set; }
    public types type { get; set; }
    public int quantity 
    { 
        get
        {
            return quantity;
        } 

        set 
        {
            if (value <= 0)
            {
                quantity = value;
            }
        } 
    }

    public enum types
    {
        potion,
        weapon
    }
}
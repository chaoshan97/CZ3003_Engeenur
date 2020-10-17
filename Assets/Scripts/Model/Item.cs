public class Item
{
    public string Name { get; set; }
    public types Type { get; set; }
    private int quantity;
    public int Quantity 
    { 
        get
        {
            return quantity;
        } 

        set 
        {
            if (value > 0)
                quantity = value;
            else
                quantity = 0;
        } 
    }

    public enum types
    {
        potion,
        weapon
    }
}
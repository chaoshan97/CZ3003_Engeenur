using System;

[Serializable]
public class Item
{
    public static string WEAPON = "weapon";
    public static string HEALINGPOTION = "healingPotion";
    public static string FREEZINGPOTION = "freezingPotion";

    public string Name { get; set; }
    private string type;
    public string Type 
    { 
        get
        {
            return this.type;
        } 
        set
        {
            if (value.Equals(WEAPON) || value.Equals(HEALINGPOTION) || value.Equals(FREEZINGPOTION))
                this.type = value;
        }
    }
    private int quantity;
    public int Quantity 
    { 
        get
        {
            return this.quantity;
        } 

        set 
        {
            // minimum is 1 item else shouldn't even add the item into inventory
            if (value > 0)
                this.quantity = value;
            else
                this.quantity = 1;
        } 
    }

    private int valueOfItem = 0;
    public int ValueOfItem
    {
        get
        {
            return this.valueOfItem;
        }
        set
        {
            if (value >= 0)
                this.valueOfItem = value;
        }
    }
}
using System;

[Serializable]
public class Item
{
    public static string WEAPON = "Damage";
    public static string HEALINGPOTION = "Health";
    public static string FREEZINGPOTION = "Time";

    // public string studentUsername { get; set; }
    // public string Name { get; set; }
    // private string type;
    // public string Type 
    // { 
    //     get
    //     {
    //         return this.type;
    //     } 
    //     set
    //     {
    //         if (value.Equals(WEAPON) || value.Equals(HEALINGPOTION) || value.Equals(FREEZINGPOTION))
    //             this.type = value;
    //     }
    // }
    // private uint quantity;
    // public uint Quantity 
    // { 
    //     get
    //     {
    //         return this.quantity;
    //     } 

    //     set 
    //     {
    //         // minimum is 1 item else shouldn't even add the item into inventory
    //         if (value > 0)
    //             this.quantity = value;
    //         else
    //             this.quantity = 1;
    //     } 
    // }

    public string name;
    public string studentUsername;
    public uint quantity;
    public string property;

    public Item() {}

    public Item(string name, string property, uint quantity, string studentUsername)
    {
        this.name = name;
        this.property = property;
        this.quantity = quantity;
        this.studentUsername = studentUsername;
    }
}
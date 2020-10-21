using System.Collections.Generic;
using FullSerializer;
using Proyecto26;

public class DatabaseShopHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void DelItemCallback();
    public delegate void PostItemCallback();
    public delegate void PostUserCallback();
    public delegate void GetItemCallback(ShopItem shopitem);
    public delegate void GetItemsCallback(Dictionary<string, ShopItem> shopitems);
    public delegate void GetUsersCallback(Dictionary<string, UserData> userDatas);

    //Create an item in Shop
    public static void PostShopItem(ShopItem shopitem, string itemName, PostItemCallback callback)
    {
        RestClient.Put<ShopItem>($"{databaseURL}shop/{itemName}.json", shopitem).Then(response => { callback(); });
    }

    //Retrieve all shop items
    public static void GetShopItems(GetItemsCallback callback)
    {
        RestClient.Get($"{databaseURL}shop.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, ShopItem>), ref deserialized);

            var shopItems = deserialized as Dictionary<string, ShopItem>;
            callback(shopItems);
        });
    }

    //Retrieve a Shop Item
    public static void GetShopItem(string itemName, GetItemCallback callback)
    {
        RestClient.Get<ShopItem>($"{databaseURL}shop/{itemName}.json").Then(shopItem => { callback(shopItem); });
    }

    //Delete a shop item
    public static void DeleteShopItem(string itemName, DelItemCallback callback)
    {
        RestClient.Delete($"{databaseURL}shop/" + itemName + ".json").Then(response => { callback(); });
    }

    //Retrieve all user details
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}students.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserData>), ref deserialized);

            var userDatas = deserialized as Dictionary<string, UserData>;
            callback(userDatas);
        });
    }

    //Update User
     public static void PutUser(string key, UserData userData, PostUserCallback callback)
    {
        RestClient.Put($"{databaseURL}students/"+key+".json", userData).Then(response => { callback(); });
    }
}

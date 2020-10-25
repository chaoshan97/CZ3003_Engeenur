using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;

public static class InventoryDBHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void GetInventoryCallback(Dictionary<string, Item> itemDict);

    // get all dictionary of items the user has in his/her inventory
    public static void GetInventory(string studentUsername, GetInventoryCallback callback) 
    {
        Dictionary<string, Item> itemsDict;
        uint i = 0;

        // ref: https://stackoverflow.com/questions/42315302/specify-condition-and-limit-for-firebase-realtime-database-request 
        // example: https://engeenur-17baa.firebaseio.com/inventory.json?orderBy=%22studentUsername%22&equalTo=%22jack%22
        RestClient.Get($"{databaseURL}inventory.json?orderBy=%22studentUsername%22&equalTo=%22" + studentUsername + "%22").Then(res => 
        {
            // parsing JSON into Item object
            var responseJson = res.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Item>), ref deserialized);

            itemsDict = deserialized as Dictionary<string, Item>;

            // to get the the type of each items from shop based on the item name
            foreach (string key in itemsDict.Keys)
            {
                Item item = itemsDict[key];
                RestClient.Get($"{databaseURL}shop/" + item.name + "/property.json").Then(res1 =>
                {
                    // without using serializer, res1.Text will include '\"' on both ends of the string so need to Trim them away
                    item.property = res1.Text.Trim('\"');

                    // check if retrieved type for last item before returning back to InventoryViewModel
                    if (++i == itemsDict.Count)
                        callback(itemsDict);
                });
            }
        });
    }
}
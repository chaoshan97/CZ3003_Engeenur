using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryController : MonoBehaviour
{
    private InventoryItems ii;
    private string serverURL = "127.0.0.1"; // 172.21.148.166
    //private UserData userData;    // commented out 1st

    // to bind item list to the UI
    private void bindList()
    {
        // fake data set
        List<Item> tempList = new List<Item>();
        Item iTemp = new Item();
        iTemp.Name = "a";
        iTemp.Quantity = 1;
        iTemp.Type = Item.types.weapon;
        Item iTemp1 = new Item();
        iTemp1.Name = "b";
        iTemp1.Quantity = 3;
        iTemp1.Type = Item.types.potion;
        tempList.Add(iTemp);
        tempList.Add(iTemp1);
        ii = new InventoryItems();
        ii.items = tempList;
    }

    // get inventory data of user from server
    public IEnumerator getInventoryDetails(Action<bool, InventoryItems> callback)
    {
        // send web to our server to get inventory data
        UnityWebRequest uwr = UnityWebRequest.Get(this.serverURL);
        yield return uwr.SendWebRequest();

        // check if have error getting data from server
        if(uwr.isNetworkError || uwr.isHttpError) 
        {
            Debug.Log(uwr.error);
            this.bindList();    // debug
            callback(true, this.ii);    // debug
        }
        else 
        {
            // get inventory items in JSON and convert to object
            this.ii = JsonUtility.FromJson<InventoryItems>(uwr.downloadHandler.text);
            this.bindList();    // debug
            callback(true, this.ii);
        }
    }
}

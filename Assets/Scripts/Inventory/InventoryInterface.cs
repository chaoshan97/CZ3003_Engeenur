using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryInterface : MonoBehaviour
{
    private EquippedItems ei;
    private InventoryItems ii;
    private string serverURL = "http://127.0.0.1:3000"; // 172.21.148.166
    //private UserData userData;    // commented out 1st

    // fake data set
    private void fakeDataGen()
    {
        ei = new EquippedItems();

        ii = new InventoryItems();
        List<Item> tempList = new List<Item>();
        for (int i=0; i<1; i++)
        {
            // Item iTemp = new Item();
            // iTemp.Name = "a";
            // iTemp.Quantity = 1;
            // iTemp.Type = Item.WEAPON;
            // Item iTemp1 = new Item();
            // iTemp1.Name = "b";
            // iTemp1.Quantity = 3;
            // iTemp1.Type = Item.WEAPON;
            // tempList.Add(iTemp);
            // tempList.Add(iTemp1);
        }
        
        ii.items = tempList;
    }


    // get inventory data of user from server
    public IEnumerator getEquippedItemsDetails(Action<bool, EquippedItems> callback)
    {
        // send web to our server to get inventory data
        UnityWebRequest uwr = UnityWebRequest.Get(this.serverURL);
        yield return uwr.SendWebRequest();

        // check if have error getting data from server
        if(uwr.isNetworkError || uwr.isHttpError) 
        {
            Debug.Log(uwr.error);
            callback(false, null);
        }
        else 
        {
            // get equipped items in JSON and convert to object
            // this.ei = JsonUtility.FromJson<EquippedItems>(uwr.downloadHandler.text);
            callback(true, this.ei);
        }
    }

    // get inventory data of user from server
    public IEnumerator getInventoryDetails(Action<bool, InventoryItems> callback)
    {
        // send web to our server to get inventory data
        UnityWebRequest uwr = UnityWebRequest.Get(this.serverURL + "/a");
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();

        // check if have error getting data from server
        if(uwr.isNetworkError || uwr.isHttpError) 
        {
            Debug.Log(uwr.error);
            // this.fakeDataGen();    // debug
            callback(false, null);
        }
        else 
        {
            Debug.Log(uwr.downloadHandler.text);    // debug
            // get inventory items in JSON and convert to object
            this.ii = JsonUtility.FromJson<InventoryItems>(uwr.downloadHandler.text);
            this.fakeDataGen();    // debug
            callback(true, this.ii);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    private InventoryItems ii;
    //private UserData userData;    // commented out 1st
    private Canvas itemsCanvas; 

    void Start()
    {
        setUserData();  // temporary
    }

    // to bind item list to the UI
    private void bindList()
    {
        // fake data set
        List<Item> tempList = new List<Item>();
        Item iTemp = new Item();
        iTemp.name = "a";
        iTemp.quantity = 1;
        iTemp.type = Item.types.weapon;
        Item iTemp1 = new Item();
        iTemp1.name = "b";
        iTemp1.quantity = 3;
        iTemp1.type = Item.types.potion;
        tempList.Add(iTemp);
        tempList.Add(iTemp1);
        ii = new InventoryItems();
        ii.items = tempList;

        ListView itemList = new ListView();
        itemList.binding = ii.items as IBinding;
    }

    // get inventory data of user from server
    private void getInventoryDetails()
    {
        // send web to our server to get inventory data
        UnityWebRequest uwr = UnityWebRequest.Get("172.21.148.166");
        uwr.SendWebRequest();

        // check if have error getting data from server
        if(uwr.isNetworkError || uwr.isHttpError) 
        {
            Debug.Log(uwr.error);
        }
        else {
            // get inventory items in JSON and convert to object
            this.ii = JsonUtility.FromJson<InventoryItems>(uwr.downloadHandler.text);
        }
    }

    public void setUserData()
    {
        // NEED TO SET USERDATA HERE. WAITING FOR SHOY 1ST TO CREATE IT

        // get inventory data of user from server
        this.getInventoryDetails();
        this.bindList();
    }
}

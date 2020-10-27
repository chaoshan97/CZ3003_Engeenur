using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class ShopViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox;
    public Text coinTxt;
    //public static string userName = "mary"; //retrieve current login user
    public Dictionary<string, Item> itemdict = new Dictionary<string, Item>();
    public string itemtobepurchased;
    public int quantity;
    //UserData currentUser = new UserData();
    //UserData currentUser;
    
    public MainMenuControllerScript mainMenuController;
    private UserData userData;

    



    // Start is called before the first frame update
    async void Start()
    {
        //string itemName = "Freezing Potion";
        //ShopItem shopitem = new ShopItem(15, "Time", 10, "Freeze time");
        //DatabaseShopHandler.PostShopItem(shopitem, itemName, () => { });
        
        await Read();
        await checkCoin();
        Init();
    }

    //Creating shopitem buttons
    public async Task Read()
    {
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
        DatabaseShopHandler.GetShopItems(shopItems =>
        {
            foreach (var shopItem in shopItems)
            {
                Debug.Log($"{shopItem.Key} {shopItem.Value.cost} {shopItem.Value.property} {shopItem.Value.no} {shopItem.Value.description}");


                GameObject tmp_btn = Instantiate(item, itemParent.transform);
                tmp_btn.name = shopItem.Key;
                Debug.Log("item name: " + tmp_btn.name);
                tmp_btn.transform.GetChild(1).GetComponent<Text>().text = (shopItem.Key).ToString();
                tmp_btn.transform.GetChild(2).GetComponent<Text>().text = (shopItem.Value.cost).ToString();
                tmp_btn.transform.GetChild(3).GetComponent<Text>().text = (shopItem.Value.description).ToString();
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(200).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Read elapsed milliseconds: {0}" + sec);
    }

    //Check coin
    public string userKey;
    public async Task checkCoin()
    {
        UserData player = mainMenuController.getUserData();


       
        //Debug.Log("coin"+ player.getCoin());
        //Debug.Log("coinnnnnnn" + player.getCoin().ToString());
        coinTxt.text = player.getCoin().ToString(); 
        Debug.Log(player.getCoin().ToString());
        // Debug.Log(userData.coin.ToString());
        /*
        Debug.Log("INNNNNNNNNNNNNNNNNNNNNNNNN");
        DatabaseShopHandler.GetUsers(userDatas =>
        {
            Debug.Log("goooooooooooooooooooooooooooo");
            foreach (var userData in userDatas)
            {
                Debug.Log($"{userData.Key} {userData.Value.coin} {userData.Value.userName}");
                if (userData.Value.userName == userName)
                {
                    userKey = userData.Key;
                    Debug.Log("User Key: " + userKey);
                    //currentUser.coin = userData.Value.coin;
                    Debug.Log(coinTxt);
                    currentUser = new UserData();
                    currentUser.email = userData.Value.email;
                    currentUser.userName = userData.Value.userName;
                    currentUser.experience = userData.Value.experience;
                    currentUser.maxExperience = userData.Value.hp;
                    currentUser.hp = userData.Value.hp;
                    currentUser.coin = userData.Value.coin;
                    coinTxt.text = (userData.Value.coin).ToString();
                    currentUser.level = userData.Value.level;
                    currentUser.verified = userData.Value.verified;
                    currentUser.localId = userData.Value.localId;
                }
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(200).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Get coin elapsed milliseconds: {0}" + sec);
        */
    }

    //When a shop item is clicked
    private int buyCost;
    public void clickShopItem(GameObject item)
    {
        formCreate.transform.GetChild(1).GetComponent<Text>().text = item.name;
        formCreate.transform.GetChild(3).GetComponent<Text>().text = "1";
        DatabaseShopHandler.GetShopItem(item.name, shopItem =>
        {
            formCreate.transform.GetChild(6).GetComponent<Text>().text = shopItem.cost.ToString();
            buyCost = shopItem.cost;
            itemtobepurchased = item.name;
        });
    }

    public int qty = 1;
    // When clicked qty up
    private int totalcost;
    public void UpQty()
    {
        qty++;
        formCreate.transform.GetChild(3).GetComponent<Text>().text = qty.ToString();        
        totalcost = qty * buyCost;
        formCreate.transform.GetChild(6).GetComponent<Text>().text = totalcost.ToString(); // total cost
        //formCreate.transform.GetChild(6).GetComponent<Text>().text = buyCost.ToString();
       
    }

    // When clicked qty down
    public void DownQty()
    {
        if (qty == 1)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Quantity cannot be less than 1.";
        }
        else
        {
            qty--;
            formCreate.transform.GetChild(3).GetComponent<Text>().text = qty.ToString();
        }
    }

    //Buy
    private int totalCost;
    public void Buy()
    {

        // simin added
        UserData player = mainMenuController.getUserData();
        Debug.Log("coin" + player.getCoin());
        Debug.Log("coinnnnnnn" + player.getCoin().ToString());
        coinTxt.text = player.getCoin().ToString();

        //

        bool notExist = true;
        string coin = coinTxt.text;
        buyCost = buyCost * qty;

  


        int urCoin = int.Parse(coin, System.Globalization.NumberStyles.Integer);
        if (urCoin >= buyCost)
        {
            //ADD IN INVENTORY (update inventory db)

            foreach (var key in this.itemdict.Keys)
            {
                var item = this.itemdict[key];
                if (this.itemtobepurchased.Equals(item.name))
                {
                    item.quantity += (uint)this.qty;
                    notExist = false;
                    break;
                }
            }

            if (notExist)
            {
                Item newItem = new Item();
                newItem.name = this.itemtobepurchased;
                newItem.quantity = (uint)this.qty;
                newItem.studentUsername = player.userName;   
                this.itemdict.Add(player.userName + this.itemtobepurchased, newItem);
            }

            InventoryDBHandler.PutInventory(this.itemdict);

            Debug.Log("CAN BUY");
            urCoin = urCoin - buyCost;
            player.coin = urCoin;
            DatabaseShopHandler.PutUser(userKey, player, () => { });//Update coin in User DB
            coinTxt.text = urCoin.ToString(); //update coin text in UI
        }
        else
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Inefficient coins.";
        }
        buyCost = 0;
        qty = 1;
    }

    private void Init()
    {
        UserData player = mainMenuController.getUserData();
        // retrieve inventory details 
        DatabaseShopHandler.GetInventory(player.userName,inventoryItems =>
        {
            itemdict = inventoryItems;
            Debug.Log("ItemDict = " + itemdict.Count);
           /* foreach (var inventoryItem in inventoryItems)
            {
                Debug.Log($"{inventoryItem.Key} {inventoryItem.Value.name} {inventoryItem.Value.quantity}");

            }
            */
        });

    }
}

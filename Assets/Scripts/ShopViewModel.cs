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
    public string userName="chaoshan"; //retrieve current login user
    
    UserData currentUser;

    // Start is called before the first frame update
    async void Start()
    {
        //string itemName = "Freezing Potion";
        //ShopItem shopitem = new ShopItem(15, "Time", 10, "Freeze time");
        //DatabaseShopHandler.PostShopItem(shopitem, itemName, () => { });
        await Read();
        await checkCoin();
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
        DatabaseShopHandler.GetUsers(userDatas =>
        {
            foreach (var userData in userDatas)
            {
                Debug.Log($"{userData.Key} {userData.Value.coin}");
                if(userData.Value.userName == userName){
                    Debug.Log("INNNN");
                    userKey = userData.Key;
                    Debug.Log("User Key: "+userKey);
                    coinTxt.text = userData.Value.coin.ToString();
                    currentUser = new UserData();
                    currentUser.userName = userData.Value.userName;
                    currentUser.experience = userData.Value.experience;
                    currentUser.maxExperience = userData.Value.hp;
                    currentUser.hp = userData.Value.hp;
                    currentUser.coin = userData.Value.coin;
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
            buyCost=shopItem.cost;
        });
    }

    public int qty = 1;
    // When clicked qty up
    public void UpQty()
    {
        qty++;
        formCreate.transform.GetChild(3).GetComponent<Text>().text = qty.ToString();
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
    public void Buy(){
        buyCost = buyCost*qty;
        int urCoin = int.Parse(coinTxt.text.ToString(), System.Globalization.NumberStyles.Integer);
        if(urCoin>=buyCost){
            //ADD IN INVENTORY (update inventory db)

            Debug.Log("CAN BUY");
            urCoin = urCoin-buyCost;
            currentUser.coin = urCoin;
            DatabaseShopHandler.PutUser(userKey, currentUser, () => { });//Update coin in User DB
            coinTxt.text= urCoin.ToString(); //update coin text in UI
        }else{
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Inefficient coins.";
        }
        buyCost=0;
        qty=1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

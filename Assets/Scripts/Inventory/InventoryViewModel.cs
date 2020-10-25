using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using System.ComponentModel;

[Binding]
public class InventoryViewModel : MonoBehaviour, INotifyPropertyChanged
{
    public InventoryInterface inventoryInt;
    public MainMenuControllerScript mainMenuControllerScript;
    private UserData userData;
    private EquippedItems equippedItems = new EquippedItems();
    private Dictionary<string, Item> inBagList;
    private List<Button> instantiatedUI = new List<Button>();
    public event PropertyChangedEventHandler PropertyChanged;
    public GameObject goldAmount;
    public GameObject equippedWeapon;

    public Button inbagRowTemplate;

    // triggers when Inventory UI's canvas is set active
    void OnEnable() 
    {
        Init();
    }

    private void Init() 
    {
        // StartCoroutine(inventoryInt.getInventoryDetails((success, allResults) => {
        //     if (success) 
        //     {
        //         this.inBagList = allResults;
        //         this.populateInBagItems();
        //     }
        // }));

        // StartCoroutine(inventoryInt.getEquippedItemsDetails((success, allResults) => {
        //     if (success) 
        //     {
        //         this.equippedItems = allResults;
        //     }
        // }));
        Debug.Log("Start of Init()"); // debug
        // get userdata from mainmenu
        // this.userData = this.mainMenuControllerScript.getUserData();

        // get items list
        InventoryDBHandler.GetInventory("tom", itemsDict => {
            this.inBagList = itemsDict;
            // display list of items in inventory on the UI
            this.populateInBagItems();
        });

        // set the gold amount to the UI
        // goldAmount.GetComponent<Text>().text = this.userData.getCoin().ToString();
    }

    // display rows of items on the inventory
    private void populateInBagItems() 
    {
        // Clear Results from previously selected level
        foreach (Button item in instantiatedUI) 
        {
            // can only destroy if is a gameObject so get button's gameObject
            DestroyImmediate(item.gameObject);
        }
        instantiatedUI.Clear();

        // Instantiate new results
        foreach (KeyValuePair<string, Item> item in this.inBagList) 
        {
            Button inbagRow = Instantiate<Button>(this.inbagRowTemplate, transform);
            inbagRow.name = item.Value.name;  // set name of the button
            inbagRow.transform.GetChild(0).GetComponent<Text>().text = item.Value.name;   // set the item name to the row
            inbagRow.transform.GetChild(0).GetComponent<Text>().fontStyle = 0; // set to unbold
            inbagRow.transform.GetChild(1).GetComponent<Text>().text = item.Value.quantity.ToString();    // set the quantity of the item to the row
            inbagRow.transform.GetChild(1).GetComponent<Text>().fontStyle = 0; // set to unbold
            // add button listener to know which item is being clicked
            inbagRow.onClick.AddListener(delegate
                {
                    onInBagItemClick(inbagRow);
                });
            instantiatedUI.Add(inbagRow);
        }
    }

    // event handlers

    public void onInBagItemClick(Button itemClicked)
    {
        bool toAppendItem = true;
        Item previousEquippedItem = this.equippedItems.weapon;
        Item item;

        // search for the item that is being clicked
        foreach (string key in this.inBagList.Keys)
        {
            item = this.inBagList[key];

            if (item.name.Equals(itemClicked.name))
            {
                // check if clicked item to be equipped is a weapon as can only equip weaponm not potions
                if (item.property.Equals(Item.WEAPON))
                {
                    Debug.Log("Equipping item...");
                    // update the newly equipped weapon to the UI
                    this.equippedWeapon.GetComponent<Text>().text = item.name;
                    // set Model of EquippedItems to the newly equipped item
                    equippedItems.weapon = item;
                    
                    // remove item from the inventory since quantity of it becomes 0 after adding it to the item list
                    if (item.quantity - 1 == 0)
                        this.inBagList.Remove(key);
                    // reduce quantity of that equipped item from the inventory
                    else
                        item.quantity -= 1;

                    // found the item so don't need to continue to search
                    break;
                }
                else
                    // can exit loop because only can equip weapons, not any of the potions.
                    return;
            }
        }

        // just incase there isn't anything
        if (previousEquippedItem != null)
        {
            // search item in inventory that contains the same item as the previously equipped item so can increase its quantity in the inventory
            foreach (string key in this.inBagList.Keys)
            {
                item = this.inBagList[key];

                // check if found the inventory item to increase the quantity
                if (item.name.Equals(previousEquippedItem.name))
                {
                    // increment item's quantity by 1 since adding previous equipped item back into the inventory
                    ++item.quantity;
                    // since increased the quantity, no need to append the item to the inventory list
                    toAppendItem = false;

                    // can return since no more task
                    break;
                }
            }

            // check if need to append item into the inventory list
            if (toAppendItem)
                // append previous equipped item since inventory doesn't have that item
                this.inBagList.Add("jack" + previousEquippedItem.name, previousEquippedItem);
        }

        // update the UI in the list of items in the inventory
        this.populateInBagItems();
    }

    

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

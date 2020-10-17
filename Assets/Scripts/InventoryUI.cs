using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryController ic;
    private InventoryItems inBagList;
    private List<GameObject> instantiatedUI = new List<GameObject>();

    public GameObject equippedWeapon;
    public GameObject inbagRowTemplate;
    public GameObject goldAmount;

    void OnEnable() 
    {
        Init();
    }

    private void Init() 
    {
        StartCoroutine(ic.getInventoryDetails((success, allResults) => {
            if (success) {
                this.inBagList = allResults;
                this.populateInBagItems();
            }
        }));
    }

    private void populateInBagItems() 
    {
        // Clear Results from previously selected level
        foreach (GameObject item in instantiatedUI) 
        {
            Destroy(item);
        }
        instantiatedUI.Clear();

        // Instantiate new results
        foreach (Item item in inBagList.items) 
        {
            GameObject resultRow = Instantiate<GameObject>(this.inbagRowTemplate, transform);
            resultRow.transform.GetChild(0).GetComponent<Text>().text = item.Name;
            resultRow.transform.GetChild(1).GetComponent<Text>().text = item.Quantity.ToString();
            instantiatedUI.Add(resultRow);
        }
    }

    // event handlers

    public void onInBagItemClick()
    {
        
    }
}

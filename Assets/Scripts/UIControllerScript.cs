using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerScript : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject createAccountCanvas;
    public GameObject townCanvas;
    public GameObject levelSelectCanvas;
    public GameObject shopCanvas;
    public GameObject battleCanvas;
    public GameObject shopScrollView;
    public GameObject inventoryScrollView;
    public GameObject questionUI;
    public GameObject battleMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loginButton()
    {
        loginCanvas.SetActive(false);
        townCanvas.SetActive(true);
 
    }

    public void gotoCreateAccountButton()
    {
        loginCanvas.SetActive(false);
        createAccountCanvas.SetActive(true);

    }

    public void createAccount()
    {
        createAccountCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }

    public void OpenLevelSelectButton()
    {
        townCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }

    public void CloseLevelSelectButton()
    {
        levelSelectCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void OpenShopButton()
    {
        townCanvas.SetActive(false);
        shopCanvas.SetActive(true);
    }

    public void CloseShopButton()
    {
        inventoryScrollView.SetActive(false);
        shopScrollView.SetActive(false);
        shopCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }

    public void OpenShopScrollView()
    {
        inventoryScrollView.SetActive(false);
        shopScrollView.SetActive(true);
    }

    public void OpenInventoryScrollView()
    {
        inventoryScrollView.SetActive(true);
        shopScrollView.SetActive(false);
    }

    public void OpenQuestionUI()
    {
        questionUI.SetActive(true);
        battleMenu.SetActive(false);
    }

    public void OpenBattleCanvas()
    {
        battleCanvas.SetActive(true);
        levelSelectCanvas.SetActive(false);
    }
    public void CloseBattleCanvas()
    {
        battleCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }
}

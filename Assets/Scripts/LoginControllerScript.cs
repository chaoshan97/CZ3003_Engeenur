using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

public class LoginControllerScript : MonoBehaviour
{

    [SerializeField]
    private InputField emailInput = null;

    [SerializeField]
    private InputField passwordInput = null;

    [SerializeField]
    private GameObject WrongPopUp = null;

    public UIControllerScript UIController;

    public LoginDbHandler loginDbHandler;

    public MainMenuControllerScript mainMenuController;


    private bool verified = false; //Set as 'false'. Now 'true' for testing purpose
    private string response;
       
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void login()
    {
        string email = emailInput.text.Trim().ToLower();
        string password = passwordInput.text;
        StartCoroutine(PostRequest(email, password));

    }

    public void gotoCreateAccount()
    {
        UIController.gotoCreateAccountButton();
    }

    IEnumerator ShowWrongMessage()
    {
        WrongPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPopUp.SetActive(false);

    }

    IEnumerator PostRequest(string username, string password)
    {
        loginDbHandler.FetchUserSignInInfo(username,password);
        yield return new WaitForSeconds(2);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

public class CreateAccountControllerScript : MonoBehaviour
{

    [SerializeField]
    private InputField Username = null;

    [SerializeField]
    private InputField Name = null;

    [SerializeField]
    private InputField Password = null;

    [SerializeField]
    private InputField PasswordConfirm = null;

    [SerializeField]
    private GameObject WrongEmailPopUp = null;

    [SerializeField]
    private GameObject WrongPasswordPopUp = null;

    public MainMenuControllerScript MainMenuController;

    public UIControllerScript UIController;

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

    public void createAccount()
    {
        string username = Username.text.Trim().ToLower();
        string name = Name.text.Trim().ToLower();
        string password = Password.text;
        string confirmPassword = PasswordConfirm.text;
        //Regex for checking email format
        bool isEmail = Regex.IsMatch(username, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        if (!isEmail) //Checking if email format is correct
        {
            Debug.Log("Is not email!");
            StartCoroutine(ShowEmailWrongMessage());
        }
        else
        {
            if (password != confirmPassword) //Checking for password and confirm password is the same
            {
                StartCoroutine(ShowPasswordWrongMessage());
            }
            else
            {   //send username,name and password to server for authentication
                string jsonData = "{\"username\":\"" + username + "\",\"name\":\"" + name + "\",\"password\":\"" + password + "\"}";

                StartCoroutine(PostRequest("http://localhost:3000/this/1", jsonData)); //Change to server url
            }
        }

        

    }

    public bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    IEnumerator ShowEmailWrongMessage()
    {
        WrongEmailPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongEmailPopUp.SetActive(false);

    }
    IEnumerator ShowPasswordWrongMessage()
    {
        WrongPasswordPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPasswordPopUp.SetActive(false);

    }

    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "GET"); //Should be POST
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response = uwr.downloadHandler.text;

            UserData player = JsonUtility.FromJson<UserData>(response);
           
            verified = player.getVerified();
            if (verified == true)
            {
                
                UIController.createAccount();
                MainMenuController.setUserData(player);
                MainMenuController.loadMainMenu();

            }


        }

    }
}

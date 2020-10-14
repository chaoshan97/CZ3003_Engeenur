using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using BCrypt.Net;
using UnityEngine.Networking;
using System.Text;


public class LoginControllerScript : MonoBehaviour
{

    [SerializeField]
    private InputField Username = null;

    [SerializeField]
    private InputField Password = null;

    [SerializeField]
    private GameObject WrongPopUp = null;

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

    public void login()
    {
        string username = Username.text.Trim().ToLower();
        string password = Password.text;
        
        /*
         * const int workFactor = 12;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor);

        Debug.LogFormat("Hash length is {0} chars", hashedPassword.Length);
        Debug.LogFormat("Hashed password: {0} ", hashedPassword);
        Debug.LogFormat("Correct password {0}", BCrypt.Net.BCrypt.Verify("PASSWORD", hashedPassword));
        Debug.LogFormat("Incorrect password {0}", BCrypt.Net.BCrypt.Verify("PASSWORd", hashedPassword));
        */

        string jsonData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

        //StartCoroutine(PostRequest("http://localhost:3000/comments", jsonData)); //Change to server url
        UIController.loginButton();
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

    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
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
            Debug.Log(player.getUsername());


            //verified = player.getVerified();
            //Debug.Log(player.getUsername());
            if (verified == true)
            {
                
                UIController.loginButton();


            }
            else
            {
                StartCoroutine(ShowWrongMessage());
            }


        }

    }
}

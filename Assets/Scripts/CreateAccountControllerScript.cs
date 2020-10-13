using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;


public class CreateAccountControllerScript : MonoBehaviour
{

    [SerializeField]
    private InputField Username = null;

    [SerializeField]
    private InputField Password = null;

    [SerializeField]
    private InputField PasswordConfirm = null;

    [SerializeField]
    private GameObject WrongPasswordPopUp = null;

    public UIControllerScript UIController;

    private bool verified = true; //Set as 'false'. Now 'true' for testing purpose
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
        string password = Password.text;
        string confirmPassword = PasswordConfirm.text;

        if(password != confirmPassword)
        {
            StartCoroutine(ShowPasswordWrongMessage());
        }
        else
        {
            string jsonData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

            StartCoroutine(PostRequest("http://localhost:3000/comments", jsonData)); //Change to server url
        }

    }

    IEnumerator ShowPasswordWrongMessage()
    {
        WrongPasswordPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPasswordPopUp.SetActive(false);

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
            //verified = player.getVerified();
            //Debug.Log(player.getUsername());

            if (verified == true)
            {
                
                UIController.createAccount();


            }


        }

    }
}

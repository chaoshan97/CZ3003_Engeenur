using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using BCrypt.Net;
using UnityEngine.Networking;
using System.Text;


public class TeacherLoginControllerScript : MonoBehaviour
{

    [SerializeField]
    private InputField Username = null;

    [SerializeField]
    private InputField Password = null;

    [SerializeField]
    private GameObject WrongPopUp = null;

    public UIControllerScript UIController;

    public MainMenuControllerScript MainMenuController;


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

        string jsonData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

        StartCoroutine(PostRequest("http://localhost:3000/teacher/1", jsonData)); //Change to server url

    }

    IEnumerator ShowWrongMessage()
    {
        WrongPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPopUp.SetActive(false);

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

            TeacherData teacher = JsonUtility.FromJson<TeacherData>(response);
 
            verified = teacher.getVerified();


            if (verified == true)
            {

                UIController.teacherLoginButton();
                MainMenuController.setTeacherData(teacher);
                MainMenuController.loadTeacherMainMenu();
            }
            else
            {
                StartCoroutine(ShowWrongMessage());
            }


        }

    }
}

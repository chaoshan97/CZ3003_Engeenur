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
    private InputField emailInput = null;

    [SerializeField]
    private InputField passwordInput = null;

    [SerializeField]
    private GameObject wrongPopUp = null;

    public UIControllerScript UIController;

    public MainMenuControllerScript mainMenuController;

    private bool verified = false; //Set as 'false'. Now 'true' for testing purpose
    private string response;
    public TeacherLoginHandler teacherLoginHandler; 

    // Start is called before the first frame update
    void Start()
    {
    }

    //Update is called once per frame
    void Update()
    {

    }

    public void login()
    {
        string email = emailInput.text.Trim().ToLower();
        string password = passwordInput.text;
    
        StartCoroutine(PostRequest(email, password));

    }

    IEnumerator ShowWrongMessage()
    {
        wrongPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        wrongPopUp.SetActive(false);

    }

    IEnumerator PostRequest(string email, string password)
    {
        teacherLoginHandler.FetchTeacherSignInInfo(email,password);
        yield return new WaitForSeconds(2);

        TeacherData teacher = new TeacherData();

        verified = teacher.getVerified();

        if (verified == true)
        {

            UIController.teacherLoginButton();
            mainMenuController.setTeacherData(teacher);
            mainMenuController.loadTeacherMainMenu();
        }
        else
        {
            StartCoroutine(ShowWrongMessage());
        }


    }


}

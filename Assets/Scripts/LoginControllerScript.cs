using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class LoginControllerScript : MonoBehaviour
{

    [SerializeField]
    private InputField emailInput = null;

    [SerializeField]
    private InputField passwordInput = null;

    [SerializeField]
    private GameObject WrongPopUp = null;

    [SerializeField]
    private GameObject EmailPassPopUp = null;

    [SerializeField]
    private GameObject EmailPopUp = null;

    [SerializeField]
    private GameObject PassPopUp = null;

    public UIControllerScript UIController;

    public LoginDbHandler loginDbHandler;

    public MainMenuControllerScript mainMenuController;
    private UserData userData;


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

    public async void login()
    {
        string email = emailInput.text.Trim().ToLower();
        string password = passwordInput.text;
        if ((email == "" || email == null) && (password == "" || password == null))
        {
            StartCoroutine(EmailPasswordNotFill());
        }
        else
        {
            if (email == "" || email == null)
            {
                StartCoroutine(EmailNotFill());
            }
            else
            {
                if (password == "" || password == null)
                {
                    StartCoroutine(PassNotFill());
                }
                else
                {
                    string loginChk = await PostRequest(email, password);
                    if (loginChk != null)
                    {
                        bool studentChk = await ChkStudent(loginChk);
                        if (studentChk == true)
                        {
                            mainMenuController.setUserData(userData);
                            //mainMenuController.loadMainMenu();
                            UIController.loginButton();
                        }
                        else
                        {
                            StartCoroutine(ShowWrongMessage());
                        }
                    }
                    else
                    {
                        StartCoroutine(ShowWrongMessage());
                    }

                }
            }
        }
    }

    /*public void gotoCreateAccount()
    {
        UIController.gotoCreateAccountButton();
    }*/

    IEnumerator EmailPasswordNotFill()
    {
        EmailPassPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        EmailPassPopUp.SetActive(false);

    }
    IEnumerator EmailNotFill()
    {
        EmailPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        EmailPopUp.SetActive(false);

    }
    IEnumerator PassNotFill()
    {
        PassPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        PassPopUp.SetActive(false);

    }

    IEnumerator ShowWrongMessage()
    {
        WrongPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPopUp.SetActive(false);

    }

    public async Task<string> PostRequest(string username, string password)
    {
        string result = await loginDbHandler.FetchUserSignInInfo(username, password);
        return result;
        Debug.Log("Check Result: " + result);
    }

    public async Task<bool> ChkStudent(string localID)
    {
        bool chk = false;
        LoginDbHandler.GetStudent(localID, student =>
        {
            chk = true;
            userData = new UserData();
            userData.coin = student.coin;
            userData.email = student.email;
            userData.experience = student.experience;
            userData.hp = student.hp;
            userData.level = student.level;
            userData.localId = student.localId;
            userData.maxExperience = student.maxExperience;
            userData.userName = student.userName;
            userData.verified = student.verified;
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
        {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Time elapsed milliseconds: {0}" + sec + ", Chk is " + chk);
        return chk;
    }
}

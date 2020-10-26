using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
//using BCrypt.Net;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FullSerializer;
using LitJson;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class LoginDbHandler : MonoBehaviour
{
    private string AuthKey = "AIzaSyAM0B744pa-v9FjU69DmlfQGMqiZAHJpUo";
    public static fsSerializer serializer = new fsSerializer();
    public string userName;
    private string idToken;
    public static string localId;
    private string getLocalId;
    public string verified;
    UserData userData = new UserData();
    private string databaseURL = "https://engeenur-17baa.firebaseio.com/students/";
    private LoginControllerScript loginControllerScript;
    private string localID;

    public async Task<string> FetchUserSignInInfo(string email, string password)
    {
        localID = null;
        await SignInUser(email, password);
        return localID;
    }
    public void FetchUserSignUpInfo(string email, string username, string password)
    {
        Debug.Log("ENTER FETCH1");
        SignUpStudent(email, username, password);
    }
    // public void verify(){
    //     loginControllerScript.ShowSuccessfulLogin();
    // }
    private void PostToDatabase(bool verified = false, string username = null, string email = null)
    {
        UserData userData = new UserData();
        Debug.Log("1");
        if (verified)
        {
            userData.email = email;
            userData.localId = localId;
            userData.userName = username;
            userData.level = 1;
            userData.experience = 0;
            userData.maxExperience = 100;
            userData.hp = 100;
            userData.coin = 0;
            userData.verified = true;
        }
        Debug.Log("2");
        //verify();

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, userData);

    }

    private void RetrieveFromDatabase()
    {
        RestClient.Get(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserData>), ref deserialized);
            var userData = deserialized as Dictionary<string, UserData>;
            Debug.Log(userData);
            //loginControllerScript.
        });
    }

    private void SignUpStudent(string email, string username, string password)
    {
        Debug.Log("signup");
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        Debug.Log("userData");
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                userName = username;
                email = email;
                Debug.Log("bp");
                PostToDatabase(true, userName, email);

            }).Catch(error =>
            {
                //verified = false;
                Debug.Log(error);

            });
    }

    private async Task SignInUser(string email, string password)
    {
        string studentData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, studentData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                GetUsername();
                localID = response.localId; //get the localId
            }).Catch(error =>
            {
                Debug.Log(error);
            });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Time elapsed milliseconds: {0}" + sec);
    }
    private void GetUsername()
    {
        RestClient.Get<UserData>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            userName = response.userName;
            GetLocalId();
            Debug.Log("here2");
        });
    }
    private void GetLocalId()
    {
        RestClient.Get(databaseURL + "/" + ".json?auth=" + idToken).Then(response =>
         {
             var username = userName;

             fsData userData = fsJsonParser.Parse(response.Text);
             Dictionary<string, UserData> users = null;
             serializer.TryDeserialize(userData, ref users);

             foreach (var user in users.Values)
             {
                 if (user.userName == username)
                 {
                     getLocalId = user.localId;
                     RetrieveFromDatabase();
                     break;
                 }
             }
         }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    public delegate void GetUsersCallback(Dictionary<string, UserData> userDatas);

    //Retrieve all user details
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get("https://engeenur-17baa.firebaseio.com/students.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserData>), ref deserialized);

            var userDatas = deserialized as Dictionary<string, UserData>;
            callback(userDatas);
        });
    }

    //Retreive Student
    public delegate void GetUserDataCallback(UserData userData);
    public static void GetStudent(string localId, GetUserDataCallback callback)
    {
        RestClient.Get<UserData>("https://engeenur-17baa.firebaseio.com/students/" + localId + ".json").Then(userData => { callback(userData); });
    }
}


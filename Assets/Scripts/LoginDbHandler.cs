using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
//using BCrypt.Net;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FullSerializer;
using LitJson;

public class LoginDbHandler: MonoBehaviour
{
    private string AuthKey = "AIzaSyAM0B744pa-v9FjU69DmlfQGMqiZAHJpUo";
    public static fsSerializer serializer = new fsSerializer();
    public string userName;
  //  public string password;
   // public string email;
    private string idToken;
    public static string localId;
    private string getLocalId;

    Student student = new Student();

    private string databaseURL = "https://engeenur-17baa.firebaseio.com/students/";
    private LoginControllerScript loginControllerScript;

   
    
   // public void FetchUserSignInInfo(string jsonData)
     public void FetchUserSignInInfo(string email, string password)
    {
       
        // JsonData json = JsonMapper.ToObject(jsonData);
        // string password = json["password"].ToString();
        // string email = json["email"].ToString();
        //password = JsonUtility.FromJson<Student>(obj.password);
        // const int workFactor = 12;
        // string pwdToHash = password + "^Y8~JK";
        // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwdToHash, workFactor);
        SignInUser(email, password);
    }
    public void FetchUserSignUpInfo(string email, string password)
    {
        // LoginControllerScript data = GetComponent<LoginControllerScript>();
        // email= data.email;
        // password=data.password;

        // JsonData json = JsonMapper.ToObject(jsonData);
        // string password = json["password"].ToString();
        // string email = json["email"].ToString();
        
        // const int workFactor = 12;
        // string pwdToHash = password + "^Y8~JK";
        // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwdToHash, workFactor);
        
        string username = "sg";
        SignUpStudent(email,username,password);
    }
    // public void verify(){
    //     loginControllerScript.ShowSuccessfulLogin();
    // }
    private void PostToDatabase(bool verified = false, string username= null)
    {
        Student student = new Student();
        Debug.Log("1");
        if (verified)
        {
            student.level = 1;
            student.experience = 100;
            student.hp = 1000;
            student.coin = 0;
            student.verified=true;
            student.localId=localId;
            student.userName=username;
            
        }
        Debug.Log("2");
        //verify();

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, student);
       
    }

    private void RetrieveFromDatabase()
    {
        
        RestClient.Get<Student>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
        {
            student = response;
            //loginControllerScript.
        });
    }

    private void SignUpStudent(string email, string username, string password)
    {
        string studentData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, studentData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                userName = username;
                PostToDatabase(true,userName);

            }).Catch(error =>
            {
                //verified = false;
                Debug.Log(error);

            });
    }

    private void SignInUser(string email, string password)
    {
        string studentData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, studentData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                GetUsername();
                Debug.Log("here1");
            }).Catch(error =>
            {
                
                Debug.Log(error);
            });
    }
    private void GetUsername()
    {
        RestClient.Get<Student>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            userName = response.userName;
            //Debug.Log("here2");
        });
    }

    // private void GetLocalId()
    // {
    //     RestClient.Get(databaseURL + "/" +".json?auth=" + idToken).Then(response =>
    //     {
    //         var username = response.Text;
    //         fsData studentData = fsJsonParser.Parse(response.Text);
    //         Dictionary<string, Student> students = null;
    //         serializer.TryDeserialize(studentData, ref students);

    //         foreach (var student in students.Values)
    //         {

    //             if (student.userName == username)
    //             {
    //                 getLocalId = student.localId;
    //                 RetrieveFromDatabase();
    //                 break;
    //             }
    //         }
    //     }).Catch(error =>
    //     {
    //         Debug.Log(error);
    //     });
    // }
}


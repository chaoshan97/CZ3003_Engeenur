using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
//using BCrypt.Net;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FullSerializer;
using LitJson;

public class TeacherLoginHandler : MonoBehaviour
{
    private string AuthKey = "AIzaSyAM0B744pa-v9FjU69DmlfQGMqiZAHJpUo";
    public static fsSerializer serializer = new fsSerializer();
    public string userName;
    private string idToken;
    public static string localId;
    private string getLocalId;

    Teacher teacher = new Teacher();
    private string databaseURL = "https://engeenur-17baa.firebaseio.com/students/";
    //private LoginControllerScript loginControllerScript;
    
    // Start is called before the first frame update
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
    private void PostToDatabase(bool verified = false, string username= null)
    {
        Teacher teacher = new Teacher();
        Debug.Log("1");
        if (verified)
        {
            teacher.verified=true;
            teacher.localId=localId;
            teacher.userName=username;
            
        }
        Debug.Log("2");
        //verify();

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, teacher);
       
    }

    private void RetrieveFromDatabase()
    {
        
        RestClient.Get<Teacher>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
        {
            teacher = response;
            //loginControllerScript.
        });
    }

    private void SignInUser(string email, string password)
    {
        string teacherData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, teacherData).Then(
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
        RestClient.Get<Teacher>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            userName = response.userName;
            //Debug.Log("here2");
        });
    }

    private void GetLocalId()
    {
        RestClient.Get(databaseURL + "/" +".json?auth=" + idToken).Then(response =>
        {
            var username = response.Text;
            fsData teacherData = fsJsonParser.Parse(response.Text);
            Dictionary<string, Teacher> teachers = null;
            serializer.TryDeserialize(teacherData, ref teachers);

            foreach (var teacher in teachers.Values)
            {
                if (teacher.userName == username)
                {
                    getLocalId = teacher.localId;
                    RetrieveFromDatabase();
                    break;
                }
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

}

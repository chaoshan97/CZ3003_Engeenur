using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
//using BCrypt.Net;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FullSerializer;
using LitJson;

public class TeacherLoginHandler: MonoBehaviour
{
    private string AuthKey = "AIzaSyAM0B744pa-v9FjU69DmlfQGMqiZAHJpUo";
    public static fsSerializer serializer = new fsSerializer();
    public string userName;
    private string idToken;
    public static string localId;
    private string getLocalId;
    public string verified;
    TeacherData teacherData = new TeacherData();
    private string databaseURL = "https://engeenur-17baa.firebaseio.com/students/";
    private LoginControllerScript loginControllerScript;
    
    public void FetchTeacherSignInInfo(string email, string password)
    {
        SignInTeacher(email, password);
    }

    private void RetrieveFromDB()
    {
        RestClient.Get(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, TeacherData>), ref deserialized);
            var teacherData  = deserialized as Dictionary<string, TeacherData>;
            Debug.Log(teacherData);
            //loginControllerScript.
        });
    }

    private void SignInTeacher(string email, string password)
    {
        string teacherData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, teacherData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                GetTeachername();
                Debug.Log("here1");
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }
    private void GetTeachername()
    {
        RestClient.Get<UserData>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            userName = response.userName;
            GetTeacherLocalId();
            Debug.Log("here2");
        });
    }
    private void GetTeacherLocalId()
    {
        RestClient.Get(databaseURL + "/" +".json?auth=" + idToken).Then(response =>
        {
            var username = userName;
            
            fsData teacherData = fsJsonParser.Parse(response.Text);
            Dictionary<string, TeacherData> teachers = null;
            serializer.TryDeserialize(teacherData, ref teachers);

            foreach (var teacher in teachers.Values)
            {
                if (teacher.userName == username)
                {
                    getLocalId = teacher.localId;
                    RetrieveFromDB();
                    break;
                }
            }
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }
}


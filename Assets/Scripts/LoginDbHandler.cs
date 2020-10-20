/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FirebaseAuth.DefaultInstance;
using BCrypt.Net;
using Firebase;
using System.Diagnostics;

public static class LoginDbHandler
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser Student;

    public string userName;
    public string password;
    public string email;
    public int level;
    public int experience;
    public int hp;
    public int coin;
    public int verified;
    Student student = new Student();
    private string databaseURL = "https://engeenur-17baa.firebaseio.com/";
    public LoginControllerScript loginControllerScript;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    public void FetchUserSignInInfo(string jsonData)
    {
        //string json = GameObject.Find("jsonData").GetComponent<loginControllerScript>().jsonData;
        string json = jsonData;
        dynamic obj = JsonConvert.DeserializeObject(json);
        password = JsonUtility.FromJson<Student>(obj.password);
        const int workFactor = 12;
        string pwdToHash = password + "^Y8~JK";
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwdToHash, workFactor);
        string email = JsonUtility.FromJson<Student>(obj.email);
        StartCoroutine(Login(email, hashedPassword));
    }
    public void FetchUserSignUpInfo()
    {
        string json = GameObject.Find("jsonData").GetComponent<loginControllerScript>().jsonData;
        dynamic obj = JsonConvert.DeserializeObject(json);
        password = JsonUtility.FromJson<Student>(obj.password);
        const int workFactor = 12;
        string pwdToHash = password + "^Y8~JK";
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pwdToHash, workFactor);
        string email = JsonUtility.FromJson<Student>(obj.email);
        string username = JsonUtility.FromJson<Student>(obj.username);
        StartCoroutine(Register(email,hashedPassword,username));
    }
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            //string message = "Login Failed!";
            //switch (errorCode)
            //{
            //    case AuthError.MissingEmail:
            //        message = "Missing Email";
            //        break;
            //    case AuthError.MissingPassword:
            //        message = "Missing Password";
            //        break;
            //    case AuthError.WrongPassword:
            //        message = "Wrong Password";
            //        break;
            //    case AuthError.InvalidEmail:
            //        message = "Invalid Email";
            //        break;
            //    case AuthError.UserNotFound:
            //        message = "Account does not exist";
            //        break;
            //}
            
            verified = 0;
            console.write(verified);
        }
        else
        {
            //User is now logged in
            //Now get the result
            fsData Student = fsJsonParser.Parse(LoginTask.Result);
            Dictionary<string, Student> student = null;
            serializer.TryDeserialize(Student, ref student);

            foreach (var studentData in student.Values)
            {
               
                userName = studentData.userName;
                level = studentData.level;
                experience = studentData.experience;
                hp = studentData.hp;
                coin = studentData.coin;
                verified =1;
                break;
                
            }
            console.write("successfully Login");
        }
    }
    public void FailedLogin(int verified)
    {
        loginControllerScript.getFaildLogin();
    }

    private IEnumerable Register(string _email, string _password, string _username)
    {
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
        if (RegisterTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            int verified = 0;
            FailedLogin(verified);


    }
        else
        {
            //User has now been created
            //Now get the result
            Student = RegisterTask.Result;

            if (Student != null)
            {
                //Create a user profile and set the username
                Student student = new student {userName=_username};

                //Call the Firebase auth update user profile function passing the profile with the username
                var ProfileTask = Student.UpdateUserProfileAsync(profile);
                //Wait until the task completes
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                userName = _username;
                email = _email;
                password = _password;

                RestClient.Put($"{databaseURL}student/" + userName + ".json", student).Then(response =>
                {
                    email = response.email;
                    password = response.password;
                    level = response.level;
                    experience = response.experience;
                    hp = response.hp;
                    coin = response.coin;
                    verified = 1;

                });
            }
        }

    }
   
}*/

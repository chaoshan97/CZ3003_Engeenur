using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData 
{
    public string username;
    public string password;
    public string id;

    public string getUsername()
    {
        return username;
    }

    public void init(string _username, string _password, string _id)
    {
        username = _username;
        password = _password;
        id = _id;
    }


}

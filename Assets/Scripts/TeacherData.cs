using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherData
{
    
    public string id;
    public string userName; //Email
    public string name;
    public bool verified;

    public string getUserName()
    {
        return userName;
    }


    public string getName()
    {
        return name;
    }

    public bool getVerified()
    {
        return verified;
    }

}

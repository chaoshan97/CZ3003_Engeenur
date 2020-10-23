using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherData
{
    public string localId;
    public string userName;
    public bool verified;

    public TeacherData()
    {
        this.userName = userName;
        this.verified = verified;
        this.localId = localId;
    }

    public string getName()
    {
        return userName;
    }

    public bool getVerified()
    {
        return verified;
    }

}

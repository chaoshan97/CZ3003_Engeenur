using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Friend
{
    public string userName;
    public List<string> students;

    public Friend(string userName, List<string> studsList)
    {
        this.userName = userName;
        students = new List<string>(studsList);
    }
}

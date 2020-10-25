using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Course
{
    public string userName;
    public List<string>students;

    public Course(string userName, List<string>studsList)
    {
        this.userName = userName;
        students = new List<string>(studsList);
    }
}
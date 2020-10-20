using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CourseLvlQn 
{
    public string courseName;
    public string userName;
    public int level;
    public List<string>qns;
    public List<string>ans;
    public List<string>students;

    public CourseLvlQn(string courseName, string userName, int level, List<string>qnsList, List<string>ansList, List<string>studsList) {
        this.courseName = courseName;
        this.userName = userName;
        this.level = level;
        qns = new List<string>(qnsList);
        ans = new List<string>(ansList);
        students = new List<string>(studsList);
    }
}

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
    public List<int>ans;
    public List<string>students;

    public CourseLvlQn(string courseName, string userName, int level, List<string>qnsList, List<int>ansList, List<string>studsList) {
        this.courseName = courseName;
        this.userName = userName;
        this.level = level;
        qns = new List<string>(qnsList);
        ans = new List<int>(ansList);
        students = new List<string>(studsList);
    }
}

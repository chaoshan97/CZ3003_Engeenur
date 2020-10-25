using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CourseLvlQn 
{
    public string courseName;
    public int level;
    public List<string>qns;
    public List<int>ans;

    public CourseLvlQn(string courseName, int level, List<string>qnsList, List<int>ansList){
        this.courseName = courseName;
        this.level = level;
        qns = new List<string>(qnsList);
        ans = new List<int>(ansList);
    }
}

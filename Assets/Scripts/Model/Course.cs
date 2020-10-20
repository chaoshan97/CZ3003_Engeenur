/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Course 
{
    public string userName{get; set;}

    public Course(string userName) {
        this.userName = userName;
    }
}**/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Course
{
    public string userName;

    public Course(string userName) {
        this.userName = userName;
    }
}

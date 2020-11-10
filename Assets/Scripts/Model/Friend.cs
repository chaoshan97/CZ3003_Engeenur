using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <br>Author: Heng Fuwei, Esmond</br> 
/// 
/// </summary>
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

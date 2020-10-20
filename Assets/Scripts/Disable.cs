using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Disable : MonoBehaviour
{


    //Get levels from db to disable stage
    public void getStg(int level)
    {
        for(int i=10; i > level; i--)
        {
            setDisable(i);
        }
    }

    //Set button to disable mode
    public void setDisable(int lvl)
    {
        Button myBtn = GameObject.Find("Stg"+lvl).GetComponent<Button>();
        myBtn.interactable = false;
       // Debug.Log("Disabled");
    }

   /* public void setEnable()
    {
        level += 1;
        Button myBtn = GameObject.Find("Stg"+level).GetComponent<Button>();
        myBtn.interactable = true;
        //Debug.Log("Enabled");
        
    }*/

    //Use the obtained data from database to get Stage disable
    public IEnumerator Stage(string uid)
    {
        GameObject getThis = GameObject.Find("GetThis"); //set GetDatabaseModel script to a gameobject and fetch this
        GetDatabaseModel playerScript = getThis.GetComponent<GetDatabaseModel>(); //create new object
        CoroutineWithData cd = new CoroutineWithData(this, playerScript.GetStageInfo(uid)); //Create new object to get function GetStageInfo
        yield return cd.coroutine; //wait for coroutine object to be stored
        Debug.Log("result is " + cd.result);  // retrieve stage information on stage completion levels
        getStg(Convert.ToInt32(cd.result)); // use variable in function of this class
        //To call IEnumerator function, use StartCoroutine(this function);
    }

    // Start is called before the first frame update
    void Awake()
    {
        
        StartCoroutine(Stage("ID1")) ; //get user id. This is to disable stage if player have not cleared

    }


  

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Proyecto26; //RestClient API
using FullSerializer; // External Library, drag the source folder in


public class Disable : MonoBehaviour
{
    Dictionary<string, ScoreData> stgComplete = new Dictionary<string, ScoreData>();
    public GetDatabaseQuestions ds;

    public static fsSerializer serializer = new fsSerializer();
    string database = "https://engeenur-17baa.firebaseio.com/";
    //Get levels from db to disable stage
    public void getStg(int level)
    {
        for (int i=10; i > level; i--)
        {
            setDisable(i);
        }
    }


    //Set button to disable mode
    public void setDisable(int lvl)
    {
        Button myBtn = GameObject.Find("Stg"+lvl).GetComponent<Button>();
        myBtn.interactable = false;
    }

    //Use this to retrieve questions and answers
    public void receive() {
        ds = FindObjectOfType(typeof(GetDatabaseQuestions)) as GetDatabaseQuestions;
        Debug.Log("Print this " + ds.question[0]);
        //Debug.Log("Monster data " + ds.mons["S1"].health + ds.mons["S1"].attack);
    }



    //Replace tom with user ID
    public IEnumerator GetStageCompleted()
    {
        RestClient.Get(database + "score.json").Then(response =>
        {
            fsData questionData = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(questionData, ref stgComplete);
            //Debug.Log("Count this" + stgComplete["tom"].levelScore.Count);
            getStg(stgComplete["tom"].levelScore.Count-1);
        });

        yield return null;
    }



    void Awake()
    {

        StartCoroutine(GetStageCompleted());

    }


  

   
}

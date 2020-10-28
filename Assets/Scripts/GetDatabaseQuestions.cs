using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26; //RestClient API
using FullSerializer; // External Library, drag the source folder in
using System;
using System.Globalization;
using System.Threading.Tasks;
//using System.Diagnostics;

public class GetDatabaseQuestions : MonoBehaviour
{

    public Disable ds;
    public Dictionary<string, StageQuestion> ques = new Dictionary<string, StageQuestion>();
    public Dictionary<string, MonsterData> mons = new Dictionary<string, MonsterData>();
    public Dictionary<string, CourseData> course = new Dictionary<string, CourseData>();
    public string ts;
    int stgAvail;

    public static fsSerializer serializer = new fsSerializer();

    string database = "https://engeenur-17baa.firebaseio.com/";

    //count number of questions in a stage. Input stage: e.g. N1 or N2...
    public int CountKey(int n)
    {
        int key = 0;
        int i = 1;
    
        while (ques.ContainsKey("N" + n + "Q" + i))
        {
            i++;
            key++;
        }
        return key;
    }



    //Get all Questions from database
    public IEnumerator GetStageQuestions()
    {
        //database + @"?orderBy=""$key""&startAt=""" + key + "Q" + k + @"""&endAt=""" + key + "Q" + k + @""""
        //database + @"?orderBy=""$key""&startAt=""" + key + @"""&endAt=""" + key + @"\uf8ff"""
        //RestClient.Get(database + @"?orderBy=""$key""&startAt="""+ key +"Q"+ k +@"""&endAt="""+key+"Q"+ k +@"""").Then(response =>
        RestClient.Get(database + "QuestionData.json").Then(response =>
        {
           

                fsData questionData = fsJsonParser.Parse(response.Text);
                serializer.TryDeserialize(questionData, ref ques);
           
                //callback(ques);
          

        });

        yield return null;
    }

    //For monster data, only uncomment when database is done
   /* public IEnumerator GetMonsterData()
    {
        RestClient.Get(database + "monsters.json").Then(response =>
        {


            fsData monsterData = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(monsterData, ref mons);

        });
        yield return null;
    }*/

    public void GetStageAvailable(string uid)
    {
       

        //database + @"?orderBy=""$key""&startAt=""" + key + "Q" + k + @"""&endAt=""" + key + "Q" + k + @""""
        //database + @"?orderBy=""$key""&startAt=""" + key + @"""&endAt=""" + key + @"\uf8ff"""
        //RestClient.Get(database + @"?orderBy=""$key""&startAt="""+ key +"Q"+ k +@"""&endAt="""+key+"Q"+ k +@"""").Then(response =>
        RestClient.Get(database + "UserData/" + uid + "/StageAvailable.json").Then(response =>
        {

            fsData stgData = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(stgData, ref stgAvail);


            //Debug.Log("Get " + stgAvail);

            //initialise to get function from another class
            ds = FindObjectOfType(typeof(Disable)) as Disable;
            ds.getStg(stgAvail);
          
            //add function to use retrieved data from database. Cannot return variable out


        });


    }

    

    void Start()
    {
        //To disable stage; Replace ID with the student ID
        GetStageAvailable("ID1");
    }

    void Awake()
    {
        
        StartCoroutine(GetStageQuestions());
        //StartCoroutine(GetMonsterData());
    }

}

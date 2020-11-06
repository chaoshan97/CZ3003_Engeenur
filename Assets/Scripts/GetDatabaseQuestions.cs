using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26; //RestClient API
using FullSerializer; // External Library, drag the source folder in
using System;
using System.Globalization;
using System.Threading.Tasks;


public class GetDatabaseQuestions : MonoBehaviour
{

    public Disable ds;
    public Dictionary<string, StageQuestion> ques = new Dictionary<string, StageQuestion>();
    public Dictionary<string, MonsterData> mons = new Dictionary<string, MonsterData>();
    public Dictionary<string, CourseData> course = new Dictionary<string, CourseData>();
    public string ts;
    int stgAvail;

    //for battle system retrieval
    public List<String> question;
    public List<int> answer;
    public int levelNo;

    public Button get1;
    public Button get2;
    public Button get3;
    public Button get4;
    public Button get5;
    public Button get6;
    public Button get7;
    public Button get8;
    public Button get9;
    public Button get10;

    public static fsSerializer serializer = new fsSerializer();

    string database = "https://engeenur-17baa.firebaseio.com/";

    //count number of questions in a stage. Input stage: e.g. 1 for stage 1
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

    

    //OnClickListener Events, update question and answer list according to stage selected
    public void GetStageSelected(string course, int stage)
    {
        levelNo = stage;
        switch (stage)
        {
            case 1:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 2:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 3:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;
            case 4:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 5:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 6:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 7:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 8:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 9:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;

            case 10:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                break;
        }
    }

        void Start()
    {
 
        get1.onClick.AddListener(delegate { GetStageSelected("N1", 1); });
        get2.onClick.AddListener(delegate { GetStageSelected("N2", 2); });
        get3.onClick.AddListener(delegate { GetStageSelected("N3", 3); });
        get4.onClick.AddListener(delegate { GetStageSelected("N4", 4); });
        get5.onClick.AddListener(delegate { GetStageSelected("N5", 5); });
        get6.onClick.AddListener(delegate { GetStageSelected("N6", 6); });
        get7.onClick.AddListener(delegate { GetStageSelected("N7", 7); });
        get8.onClick.AddListener(delegate { GetStageSelected("N8", 8); });
        get9.onClick.AddListener(delegate { GetStageSelected("N9", 9); });
        get10.onClick.AddListener(delegate { GetStageSelected("N10", 10); });
    }

    void Awake()
    {
        
        StartCoroutine(GetStageQuestions());
        //StartCoroutine(GetMonsterData());
    }

}

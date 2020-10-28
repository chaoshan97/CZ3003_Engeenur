using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using Proyecto26; //RestClient API
using FullSerializer;


public class CourseSelectionController : MonoBehaviour
{
    public Dictionary<string, CourseData> course = new Dictionary<string, CourseData>();
    public Dictionary<string, SpecialLevelQuestion> splevel = new Dictionary<string, SpecialLevelQuestion>();
    public List<String> myKeys;
    public List<String> key;
    public GetDatabaseQuestions db;
    string database = "https://engeenur-17baa.firebaseio.com/";

    public static fsSerializer serializer = new fsSerializer();

    public Dropdown dropdown;
    public DynamicButtons dbtn;
    public GameObject CZ2001Canvas;
    public GameObject DefaultCanvas;

    

    public void Drop_IndexChanged(int index)
    {

        dbtn = FindObjectOfType(typeof(DynamicButtons)) as DynamicButtons;

        switch (index)
        {
            case 0:
                CZ2001Canvas.SetActive(false);
                DefaultCanvas.SetActive(true);
                dbtn.SetBtnInvisible(StgCount(myKeys[0]));
                break;
            case 1:
                CZ2001Canvas.SetActive(true);
                DefaultCanvas.SetActive(false);
                //dbtn.SetBtnInvisible(4);
                dbtn.SetBtnInvisible(StgCount(myKeys[1]));
                break;
            case 2:
                CZ2001Canvas.SetActive(true);
                DefaultCanvas.SetActive(false);
                break;
            default:
                CZ2001Canvas.SetActive(false);
                DefaultCanvas.SetActive(true);
                dbtn.SetBtnInvisible(StgCount(myKeys[0]));
                break;

        }
            
    }

    
    void Awake()
    {
        StartCoroutine(GetStudentCourse());
        StartCoroutine(GetSpecial());
    }
    public void SpecialStg()
    {
        dbtn = FindObjectOfType(typeof(DynamicButtons)) as DynamicButtons;
        PopulateList();
        CZ2001Canvas.SetActive(false);
        DefaultCanvas.SetActive(true);
        dbtn.SetBtnInvisible(StgCount(myKeys[0]));
        Debug.Log("Retrun " + splevel[key[0]].qns[0]); //use this to retrieve special level questions
    }



    void PopulateList()
    {

        dropdown.options.Clear();
        List<string> items = new List<string>();
        items.Add("Item 1");

        //check for student registered courses
        for (int i = 0; i < myKeys.Count; i++)
        {
            for(int k=0; k< course[myKeys[i]].students.Count; k++)
            {
                if (course[myKeys[i]].students[k] == "Bob") //change Bob to name from database
                {
                    items.Add(myKeys[i]);
                    break;
                }
                    
            }
            
        }
        items.Add("try");
        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

     
    }

    
    public IEnumerator GetStudentCourse()
    {


        RestClient.Get(database + "course.json").Then(response =>
        {


            fsData studentCourse = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(studentCourse, ref course);
            myKeys = new List<String>(course.Keys);


        });

        yield return null;
    }

    public IEnumerator GetSpecial()
    {


        RestClient.Get(database + "courseLevelQns.json").Then(response =>
        {


            fsData studentCourse = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(studentCourse, ref splevel);
            key = new List<String>(splevel.Keys);


        });

        yield return null;
    }

    public int StgCount(string k)
    {
        int level = 0;
        foreach (var i in key)
        {
            if (splevel[i].courseName == k)
                level++;
        }
        return level;
    }

}

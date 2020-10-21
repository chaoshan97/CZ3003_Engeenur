using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;//external library, get from nuget
using System.Runtime.InteropServices;

public class GetDatabaseModel : MonoBehaviour
{
    string test;
    string database = "https://my-project-1475569765373.firebaseio.com/QuestionData/";


   
    

    public IEnumerator GetQuestion(string stage, int qst)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(database + stage +"Q"+ qst +".json"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            StageQuestion info = JsonUtility.FromJson<StageQuestion>(weatherJSON);
            test = info.Questions;
            Debug.Log("Return " + test);
            yield return info.Questions;
           
        }
    }

    public IEnumerator GetAnswer(string stage, int qst)
    {
        using (UnityWebRequest req = UnityWebRequest.Get("https://my-project-1475569765373.firebaseio.com/QuestionData/" + stage + "Q" + qst + ".json"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            StageQuestion info = JsonUtility.FromJson<StageQuestion>(weatherJSON);
            //Debug.Log("Request " + info.Questions);
            yield return info.Answer.ToString();

        }
    }

    public IEnumerator GetStageInfo(string uid)
    {
        using (UnityWebRequest req = UnityWebRequest.Get("https://my-project-1475569765373.firebaseio.com/UserData/" + uid + ".json"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            StageData info = JsonUtility.FromJson<StageData>(weatherJSON);
            yield return info.StageAvailable.ToString();

        }
    }


   /* public IEnumerator GetMonsterHp(string mid)
    {
        using (UnityWebRequest req = UnityWebRequest.Get("https://my-project-1475569765373.firebaseio.com/UserData/" + mid + ".json"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            StageData info = JsonUtility.FromJson<StageData>(weatherJSON);
            yield return info.StageAvailable.ToString();

        }
    }*/

    //Call question and answer
    public void ClickMe()
        {
        //StartCoroutine(Question("N1" , 1));
        //StartCoroutine(Answer("N1", 1));
        //StartCoroutine(Stage("ID1"));
        StartCoroutine(GetQuestion("N1",1));
        Debug.Log("Get " + test);
        }




  





    //For BP to get Question in his script
    /*public IEnumerator Question(string stage, int qst)
    {
        GameObject getThis = GameObject.Find("GetThis"); //set GetDatabaseModel script to a gameobject and fetch this
        GetDatabaseModel playerScript = getThis.GetComponent<GetDatabaseModel>(); //create new object
        CoroutineWithData cd = new CoroutineWithData(this, playerScript.GetQuestion(stage, qst)); //Create new object to get function GetStageInfo
        yield return cd.coroutine; //wait for coroutine object to be stored
        Debug.Log("result is " + cd.result);  // retrieve stage information on stage completion levels
        //getStg(Convert.ToInt32(cd.result)); // use variable in function of this class
        //To call IEnumerator function, use StartCoroutine(this function);
    }*/

    /*public IEnumerator Answer(string stage, int qst)
    {
        GameObject getThis = GameObject.Find("GetThis"); //set GetDatabaseModel script to a gameobject and fetch this
        GetDatabaseModel playerScript = getThis.GetComponent<GetDatabaseModel>(); //create new object
        CoroutineWithData cd = new CoroutineWithData(this, playerScript.GetAnswer(stage, qst)); //Create new object to get function GetStageInfo
        yield return cd.coroutine; //wait for coroutine object to be stored
        Debug.Log("result is " + cd.result);  // retrieve stage information on stage completion levels
        //getStg(Convert.ToInt32(cd.result)); // use variable in function of this class
        //To call IEnumerator function, use StartCoroutine(this function);
    }*/

}

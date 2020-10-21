using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LeadershipInterface : MonoBehaviour
{
    public List<TheScore> scoreList;
    private string serverURL = "127.0.0.1"; // 172.21.148.166
    //private UserData userData;    // commented out 1st

    // fake data
    private void fakeDataGen()
    {
        // fake data set
        scoreList = new List<TheScore>();
        for (int i=0; i < 10; i++)
        {
            TheScore s = new TheScore();
            s.LevelNo = i;
            s.studentName = "a";
            s.Score = 10 - i;
            scoreList.Add(s);
        }
        
    }

    // get inventory data of user from server
    public IEnumerator getLeadershipBoardDetails(Action<bool, List<TheScore>> callback)
    {
        // send web to our server to get inventory data
        UnityWebRequest uwr = UnityWebRequest.Get(this.serverURL);
        yield return uwr.SendWebRequest();

        // check if have error getting data from server
        if(uwr.isNetworkError || uwr.isHttpError) 
        {
            Debug.Log(uwr.error);
            this.fakeDataGen();    // debug
            this.scoreList = this.scoreList.OrderBy(t => t.Score).ToList(); // debug
            callback(true, this.scoreList);    // debug
            // callback(false, new List<TheScore>());
        }
        else 
        {
            // get inventory items in JSON and convert to object
            this.scoreList = JsonUtility.FromJson<List<TheScore>>(uwr.downloadHandler.text);
            this.fakeDataGen();    // debug
            // sort by score
            this.scoreList = this.scoreList.OrderBy(t => t.Score).ToList();
            callback(true, this.scoreList);
        }
    }
}

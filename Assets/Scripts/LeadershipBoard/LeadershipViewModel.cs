using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LeadershipViewModel : MonoBehaviour
{
    public LeadershipInterface leadershipInt;
    private List<TheScore> scoreList;
    private List<GameObject> instantiatedUI = new List<GameObject>();
    public GameObject scoreRowTemplate;

    void OnEnable() 
    {
        Init();
    }

    private void Init() 
    {
        StartCoroutine(leadershipInt.getLeadershipBoardDetails((success, allResults) => {
            if (success) {
                this.scoreList = allResults;
                this.populateLeadershipboardRows();
            }
        }));
    }

    private void populateLeadershipboardRows() 
    {
        // Clear Results from previously selected level
        foreach (GameObject item in instantiatedUI) 
        {
            Destroy(item);
        }
        instantiatedUI.Clear();
        // this.scoreRowTemplate.GetComponent<Button>

        // Instantiate new results
        foreach (TheScore score in scoreList) 
        {
            GameObject resultRow = Instantiate<GameObject>(this.scoreRowTemplate, transform);
            resultRow.transform.GetChild(0).GetComponent<Text>().text = score.studentName;
            resultRow.transform.GetChild(0).GetComponent<Text>().fontStyle = 0; // set to not bold
            resultRow.transform.GetChild(1).GetComponent<Text>().text = score.LevelNo.ToString();
            resultRow.transform.GetChild(1).GetComponent<Text>().fontStyle = 0; // set to not bold
            resultRow.transform.GetChild(2).GetComponent<Text>().text = score.Score.ToString();
            resultRow.transform.GetChild(2).GetComponent<Text>().fontStyle = 0; // set to not bold
            instantiatedUI.Add(resultRow);
        }
    }
}

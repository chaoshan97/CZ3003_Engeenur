using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewResultUI : MonoBehaviour
{
    public ViewResultController ctrl;

    public GameObject resultTemplate;
    public Dropdown dropdown;
    private List<GameObject> instantiatedUI = new List<GameObject>();

    private Dictionary<string, Dictionary<string, int>> allResults;

    void OnEnable() {
        Init();
    }

    private void Init() {
        ctrl.GetResults(allResults => {
            this.allResults = allResults;
            PopulateCourseInDropdown();
        });
    }

    private void PopulateCourseInDropdown() {
        dropdown.ClearOptions();

        foreach (string course in allResults.Keys) {
            dropdown.options.Add(new Dropdown.OptionData(course));
        }
        dropdown.RefreshShownValue();

        // Trigger OnValueChange Event to PopulateLevelResults(0)
        OnLevelSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate {
            OnLevelSelected(dropdown);
        });
    }

    private void OnLevelSelected(Dropdown dropdown) {
        int index = dropdown.value;
        string course = dropdown.options[index].text;
        PopulateScores(course);
    }

    private void PopulateScores(string course) {
        foreach (GameObject item in instantiatedUI) {
            Destroy(item);
        }
        instantiatedUI.Clear();

        Dictionary<string, int> scores = allResults[course];

        // Instantiate new results
        foreach (string user in scores.Keys) {
            GameObject resultRow = Instantiate<GameObject>(resultTemplate, transform);
            resultRow.transform.GetChild(0).GetComponent<Text>().text = user;
            resultRow.transform.GetChild(1).GetComponent<Text>().text = scores[user].ToString();
            resultRow.SetActive(true);
            instantiatedUI.Add(resultRow);
        }
    }
}

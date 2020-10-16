using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewResultUI : MonoBehaviour
{
    public ViewResultController ctrl;
    private List<SpecialLevelResult> allResults;

    public GameObject resultTemplate;
    public Dropdown dropdown;
    private List<GameObject> instantiatedUI = new List<GameObject>();

    void OnEnable() {
        Init();
    }

    private void Init() {
        StartCoroutine(ctrl.GetResults((success, allResults) => {
            if (success) {
                this.allResults = allResults;
                PopulateLevelsInDropdown();
            }
        }));
    }

    private void PopulateLevelsInDropdown() {
        dropdown.ClearOptions();
        
        for (int i=0; i<allResults.Count; i++) {
            dropdown.options.Add(new Dropdown.OptionData(allResults[i].level));
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
        PopulateLevelResults(index);
    }

    private void PopulateLevelResults(int index) {
        // Clear Results from previously selected level
        foreach (GameObject item in instantiatedUI) {
            Destroy(item);
        }
        instantiatedUI.Clear();

        // Instantiate new results
        foreach (SpecialLevelResult.UserResult userResult in allResults[index].userResults) {
            GameObject resultRow = Instantiate<GameObject>(resultTemplate, transform);
            resultRow.transform.GetChild(0).GetComponent<Text>().text = userResult.username;
            resultRow.transform.GetChild(1).GetComponent<Text>().text = userResult.score;
            instantiatedUI.Add(resultRow);
        }
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewResultController : MonoBehaviour {

    public UIControllerScript uiController;

    public void GetResults(Action<Dictionary<string, Dictionary<string, int>>> callback) {
        SpecialScoreDBHandler.GetResults(allResults => {
            callback(allResults);
        });
    }

    public void CloseViewResult() {
        //uiController.CloseViewResult();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ViewResultController : MonoBehaviour {
    private string ResultsURL = "http://127.0.0.1:3000/data";

    // Wrapper class for Json List used in JsonUtility
    private class UserResultList {
        public List<SpecialLevelResult.UserResult> userResults;
    }

    public IEnumerator GetResults(Action<bool, List<SpecialLevelResult>> callback) {
        UnityWebRequest req = UnityWebRequest.Get(ResultsURL);
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log(string.Format("GetResults - failed. {0}\n{1}", req.error, req.downloadHandler.text));
            callback(false, null);
        } else {
            // Parse JSON response using SimpleJSON to get Level Names
            var node = JSONNode.Parse(req.downloadHandler.text);
            if (node.Tag == JSONNodeType.Object) {
                List<SpecialLevelResult> allResults = new List<SpecialLevelResult>();

                foreach (KeyValuePair<string, JSONNode> kvp in (JSONNode)node) {
                    string level = kvp.Key;
                    string resultsJson = AddJsonListKey(kvp.Value.ToString(), "userResults");
                    // Create List of UserResults
                    List<SpecialLevelResult.UserResult> userResults = JsonUtility.FromJson<UserResultList>(resultsJson).userResults;
                    // Create SpecialLevelResults obj and add to list
                    allResults.Add(new SpecialLevelResult(level, userResults));
                }
                // return allResults
                callback(true, allResults);
            } else {
                Debug.Log("node.Tag == JSONNodeType.Object - Failed");
                callback(false, null);
            }
        }
    }

    // Helper method to add key to Json List for JsonUtility
    private string AddJsonListKey(string json, string key) {
        return "{\"" + key + "\":" + json + "}";
    }

}

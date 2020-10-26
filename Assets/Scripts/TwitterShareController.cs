using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Twity.DataModels.Core;

public class TwitterShareController : MonoBehaviour
{
    public GameObject popup;
    public InputField pinField;
    public Button submitBtn;
    public Text info;

    public static string CONSUMER_KEY = "LxL3mKtWPRCkELljFghQL7ae2";
    public static string CONSUMER_SECRET = "w2qVN3IznTkcOpthcnOadXH6dsBDu5945XUHNDiFFYqreXiUQj";

    private int level = 0;
    private int score = 0;

    public static string screenshotBase64;

    public void setResults(int level, int score) {
        this.level = level;
        this.score = score;
    }

    #region Twity API
    public void ShareToTwitter() {
        StartCoroutine(TakeScreenshot());
        Twity.Oauth.consumerKey = CONSUMER_KEY;
        Twity.Oauth.consumerSecret = CONSUMER_SECRET;

        StartCoroutine(Twity.Client.GenerateRequestToken(RequestTokenCallback));
    }
    void RequestTokenCallback(bool success) {
        if (!success) {
            return;
        }
        Application.OpenURL(Twity.Oauth.authorizeURL);
        popup.SetActive(true);
    }
    void GenerateAccessToken(string pin) {
        // pin is numbers displayed on web browser when user complete authorization.
        StartCoroutine(Twity.Client.GenerateAccessToken(pin, AccessTokenCallback));
    }
    void AccessTokenCallback(bool success) {
        if (!success) return;
        // When success, authorization is completed. You can make request to other endpoint.
        // User's screen_name is in '`Twity.Client.screenName`.
        info.text = "Authentication Successful!";
        Debug.Log("Twitter Username: " + Twity.Client.screenName);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["media_data"] = screenshotBase64;
        StartCoroutine(Twity.Client.Post("media/upload", parameters, MediaUploadCallback));
    }
    void MediaUploadCallback(bool success, string response) {
        if (success) {
            UploadMedia media = JsonUtility.FromJson<UploadMedia>(response);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["media_ids"] = media.media_id.ToString();
            parameters["status"] = $"I cleared level {this.level} with a score of {this.score}!";
            StartCoroutine(Twity.Client.Post("statuses/update", parameters, StatusesUpdateCallback));
        } else {
            Debug.Log(response);
        }
    }
    void StatusesUpdateCallback(bool success, string response) {
        if (success) {
            info.text = "Battle Results Posted!";
            Debug.Log("Tweet Posted");
            Tweet tweet = JsonUtility.FromJson<Tweet>(response);
            popup.SetActive(false);
        } else {
            Debug.Log(response);
        }
    }
    #endregion


    // Popup Submit Button OnClick Listener
    public void submitBtnListener() {
        string pin = pinField.text;
        Debug.Log("User entered PIN: " + pin);
        GenerateAccessToken(pin);
    }

    // Screenshot function
    IEnumerator TakeScreenshot() {
        yield return new WaitForEndOfFrame();

        Texture2D screenImg = new Texture2D(Screen.width, Screen.height);
        screenImg.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImg.Apply();
        screenshotBase64 = System.Convert.ToBase64String(screenImg.EncodeToPNG());
    }
}

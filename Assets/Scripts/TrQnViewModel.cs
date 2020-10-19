using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Text.RegularExpressions;
using UnityWeld.Binding;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class TrQnViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, formUpdate, messageBox, delMsgBox, loader;
    public string key;
    public string courseName;
    public string userName;
    public int level;
    public InputField qnInput;
    public InputField ansInput;
    public InputField qnUpdateInput;
    public InputField ansUpdateInput;
    public bool chk = false;
    CourseLvlQn courseLvlQnCreate;
    public SpecialLevelController specialLevelController;
    public EnrollViewModel enrollViewModel;
    public UIControllerScript UIController;

    void Start()
    {
    }

    public async Task Read()
    {
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
        DatabaseQAHandler.GetCourseLvlQn(key, courseLvlQn =>
        {
            courseLvlQnCreate = courseLvlQn;
            int number = 1;
            for (int i = 0; i < (courseLvlQn.qns).Count; i++) // Loop through List
            {
                GameObject tmp_item = Instantiate(item, itemParent.transform);
                tmp_item.name = i.ToString();
                Debug.Log("here item name: " + tmp_item.name);
                tmp_item.transform.GetChild(0).GetComponent<Text>().text = number.ToString();
                tmp_item.transform.GetChild(1).GetComponent<Text>().text = courseLvlQn.qns[i];
                tmp_item.transform.GetChild(2).GetComponent<Text>().text = courseLvlQn.ans[i];
                number++;
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Read elapsed milliseconds: {0}" + sec);
    }

    //create qns check
    public async void CreateQns()
    {
        string qn = qnInput.text;
        string ans = ansInput.text;
        bool handler = Check(qn, ans);
        if (handler == true)
        {
            await InvokeQnCheckExist(qn, ans);
        }
        chk = false;
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
        formCreate.transform.GetChild(2).GetComponent<InputField>().text = "";
    }

    //Basic Form Check
    public bool Check(string qn, string ans)
    {
        string strQ = Regex.Replace(qn, @"\s", "");
        string strA = Regex.Replace(ans, @"\s", "");
        if (qn == null || strQ == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a question.";
            return false;
        }
        else if (ans == null || strA == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a question.";
            return false;
        }
        return true;
    }

    // Qn Creation Check
    public async Task InvokeQnCheckExist(string qn, string ans)
    {
        Task<bool> task = CheckQnExist(qn);
        bool qnExist = await task;
        Debug.Log("Qn Exist: " + qnExist);
        if (qnExist == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = qn + " already exist.";
        }
        else
        {
            loader.SetActive(true);
            await PostingQn(qn, ans);
            await Read();
            loader.SetActive(false);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = qn + " created successfully.";

        }
    }

    //Check for existing qn
    public async Task<bool> CheckQnExist(string qn)
    {
        DatabaseQAHandler.GetCourseLvlQn(key, courseLvlQn =>
        {
            //courseLvlQnCreate = courseLvlQn;
            for (int i = 0; i < (courseLvlQn.qns).Count; i++) // Loop through List
            {
                if (qn == courseLvlQn.qns[i])
                {
                    chk = true;
                }
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(500).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("check qn exist elapsed milliseconds: {0}" + sec + ", Chk is " + chk);
        return chk;
    }

    //Creating qn in DB
    public async Task PostingQn(string qn, string ans)
    {
        (courseLvlQnCreate.qns).Add(qn);
        (courseLvlQnCreate.ans).Add(ans);
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Qn Elapsed milliseconds: {0}" + sec);
    }

    //Check the name of the course to be deleted
    public int delName;
    public void CheckDelName(GameObject item)
    {
        delName = int.Parse(item.name, System.Globalization.NumberStyles.Integer);
        Debug.Log("CHECK DELETE name: " + delName);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to delete this question?";

    }

    //Deleting qn in DB
    public async void DeleteQn()
    {
        loader.SetActive(true);
        (courseLvlQnCreate.qns).RemoveAt(delName);
        (courseLvlQnCreate.ans).RemoveAt(delName);
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Qn Elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Deleted successfully.";
        //key = null;
    }

    //Check the name of the course to be edited
    public int editName;
    public void OpenUpdateForm(GameObject item)
    {
        editName = int.Parse(item.name, System.Globalization.NumberStyles.Integer);
        Debug.Log("CHECK EDIT name: " + editName);
        formUpdate.transform.GetChild(1).GetComponent<InputField>().text = courseLvlQnCreate.qns[editName];
        formUpdate.transform.GetChild(2).GetComponent<InputField>().text = courseLvlQnCreate.ans[editName];
    }
    // Edit qn basic check
    public async void EditQn()
    {
        string qn = qnUpdateInput.text;
        string ans = ansUpdateInput.text;
        bool handler = Check(qn, ans);
        if (handler == true)
        {
            if (qn == courseLvlQnCreate.qns[editName] && ans == courseLvlQnCreate.ans[editName])
            {
                messageBox.SetActive(true);
                messageBox.transform.GetChild(1).GetComponent<Text>().text = "No changes in question and answer.";
            }
            else
            {
                await UpdateQn(qn, ans);
            }
        }
        formUpdate.transform.GetChild(1).GetComponent<InputField>().text = "";
        formUpdate.transform.GetChild(2).GetComponent<InputField>().text = "";
    }

    //Update qn in DB
    public async Task UpdateQn(string qn, string ans)
    {
        loader.SetActive(true);
        courseLvlQnCreate.qns[editName] = qn;
        courseLvlQnCreate.ans[editName] = ans;
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Updating Qn Elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Updated successfully.";
    }

    //Wake up 
    public async void WakeUp()
    {
        Debug.Log("Course Name In tr qn: " + courseName);
        Debug.Log("Username In tr qn: " + userName);
        Debug.Log("Key In tr qn: " + key);
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    //When click Close
    public void CloseTrQn()
    {
        specialLevelController.WakeUp();
        UIController.CloseTrQnCanvas();
    }

    //When click Next
    public void OpenEnrollment()
    {
        enrollViewModel.key = key;
        enrollViewModel.courseName = courseName;
        enrollViewModel.userName = userName;
        enrollViewModel.WakeUp();
        Debug.Log("CLICK next ITEM NAME: " + key);
        UIController.OpenEnrollCanvas();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

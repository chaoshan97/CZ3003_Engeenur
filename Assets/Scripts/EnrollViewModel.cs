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

public class EnrollViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader;
    public string key;
    public string courseName;
    public string userName;
    public int level;
    public InputField studInput;
    public bool chk = false;
    CourseLvlQn courseLvlQnCreate;
    public TrQnViewModel trQnViewModel;
    public UIControllerScript UIController;
    // Start is called before the first frame update
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
            for (int i = 0; i < (courseLvlQn.students).Count; i++) // Loop through List
            {
                GameObject tmp_item = Instantiate(item, itemParent.transform);
                tmp_item.name = i.ToString();
                Debug.Log("here item name: " + tmp_item.name);
                tmp_item.transform.GetChild(0).GetComponent<Text>().text = number.ToString();
                tmp_item.transform.GetChild(1).GetComponent<Text>().text = courseLvlQn.students[i];
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

    //create stud check
    public async void CreateStudEnroll()
    {
        string studName = studInput.text;
        bool handler = Check(studName);
        if (handler == true)
        {
            await InvokeStudCheckExist(studName);
        }
        chk = false;
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
    }

    //Basic Form Check
    public bool Check(string studName)
    {
        string str = Regex.Replace(studName, @"\s", "");
        if (studName == null || str == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter student username.";
            return false;
        }
        return true;
    }

    // Stud Creation Check
    public async Task InvokeStudCheckExist(string studName)
    {
        Task<bool> task = CheckStudExist(studName);
        bool studExist = await task;
        Debug.Log("Stud Exist: " + studExist);
        if (studExist == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = studName + " already exist.";
        }
        else
        {
            loader.SetActive(true);
            await PostingStud(studName);
            await Read();
            loader.SetActive(false);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = studName + " enrolled successfully.";

        }
    }

    //Check for existing stud
    public async Task<bool> CheckStudExist(string studName)
    {
        DatabaseQAHandler.GetCourseLvlQn(key, courseLvlQn =>
        {
            for (int i = 0; i < (courseLvlQn.students).Count; i++) // Loop through List
            {
                if (studName == courseLvlQn.students[i])
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

    //Creating stud in DB
    public async Task PostingStud(string studName)
    {
        (courseLvlQnCreate.students).Add(studName);
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
    public async void DeleteStud()
    {
        loader.SetActive(true);
        (courseLvlQnCreate.students).RemoveAt(delName);
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Delete Stud Elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Deleted successfully.";
    }

    //Wake up 
    public async void WakeUp()
    {
        Debug.Log("Course Name In enrollment: " + courseName);
        Debug.Log("Username In enrollment: " + userName);
        Debug.Log("Key In enrollment: " + key);
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    //When click Close
    public void CloseEnroll()
    {
        trQnViewModel.WakeUp();
        UIController.CloseEnrollCanvas();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

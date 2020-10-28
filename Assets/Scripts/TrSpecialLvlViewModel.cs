using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class TrSpecialLvlViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader;
    public string courseName;
    public string userName;
    private bool chk = false;
    public EnrollViewModel enrollViewModel;
    public UIControllerScript UIController;
    public TrQnViewModel trQnViewModel;
    public bool created = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    //Creating buttons for levels
    public async Task Read()
    {
        Debug.Log("READ ");
        int count = itemParent.transform.childCount;
        if (count != null)
        {
            for (int i = 0; i < count; i++)
            {
                Debug.Log("itemparent");
                Destroy(itemParent.transform.GetChild(i).gameObject);
            }

        }
        //Retrieve Courses Created by the Teacher
        DatabaseQAHandler.GetCourseLvlQns(courseLvlQns =>
        {
            foreach (var courseLvlQn in courseLvlQns)
            {
                Debug.Log($"{courseLvlQn.Key} {courseLvlQn.Value.courseName} {courseLvlQn.Value.level}");
                if (courseLvlQn.Value.courseName == courseName)//&& courseLvlQn.Value.userName == userName)
                {

                    GameObject tmp_btn = Instantiate(item, itemParent.transform);
                    tmp_btn.name = courseLvlQn.Key;
                    Debug.Log("item name: " + tmp_btn.name);
                    Debug.Log("lvl name: " + (courseLvlQn.Value.level).ToString());
                    tmp_btn.transform.GetChild(1).GetComponent<Text>().text = (courseLvlQn.Value.level).ToString();

                }
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

    public int lvlNoIF = 1;

    public void OpenFormCreate()
    {
        formCreate.transform.GetChild(2).GetComponent<Text>().text = "1";
    }

    public void UpLvlNoInput()
    {
        lvlNoIF++;
        formCreate.transform.GetChild(2).GetComponent<Text>().text = lvlNoIF.ToString();
    }

    public void DownLvlNoInput()
    {
        if (lvlNoIF == 1)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Level cannot be less than 1.";
        }
        else
        {
            lvlNoIF--;
            formCreate.transform.GetChild(2).GetComponent<Text>().text = lvlNoIF.ToString();
        }
    }

    public async void CreateLvl()
    {
        await InvokeLvlCheckExist(lvlNoIF);
        lvlNoIF = 1;
        chk = false;
        formCreate.transform.GetChild(2).GetComponent<Text>().text = lvlNoIF.ToString();
    }

    public List<string> qns = new List<string>();
    public List<int> ans = new List<int>();
    public async Task InvokeLvlCheckExist(int lvlNo)
    {
        Task<bool> task = CheckLvlExist(lvlNo);
        bool lvlExist = await task;
        Debug.Log("Level Exist: " + lvlExist);
        if (lvlExist == true)
        {
            created = false;
            Debug.Log("LvlExist true, created: "+created);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Level " + lvlNo + " already exist.";
        }
        else
        {
            var courseLvlQn = new CourseLvlQn(courseName, lvlNo, qns, ans);
            loader.SetActive(true);
            created = true;
            await PostingLvl(courseLvlQn);
            await Read();
            loader.SetActive(false);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Level " + lvlNo + " created successfully.";
        }
    }

    public async Task<bool> CheckLvlExist(int lvlNo)
    {
        DatabaseQAHandler.GetCourseLvlQns(courseLvlQns =>
        {
            foreach (var courseLvlQn in courseLvlQns)
            {
                Debug.Log($"{courseLvlQn.Key} {courseLvlQn.Value.courseName} {courseLvlQn.Value.level}");
                if (/*courseLvlQn.Value.userName == userName &&*/ courseLvlQn.Value.courseName == courseName)
                {
                    if (courseLvlQn.Value.level == lvlNo)
                    {
                        chk = true;
                    }
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
        Debug.Log("check level exist elapsed milliseconds: {0}" + sec);
        return chk;
    }

    //Create level in DB
    public async Task PostingLvl(CourseLvlQn courseLvlQn)
    {
        DatabaseQAHandler.PostCourseLvlQn(courseLvlQn, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Level Elapsed milliseconds: {0}" + sec);
    }

    //Check the name of the course to be deleted
    public string key;
    public void CheckItemDelName(GameObject item)
    {
        key = item.name;
        Debug.Log("CHECK DELETE name: " + key);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to delete this level?";

    }

    //Delete course in database
    public async void DeleteCourseLvl()
    {
        loader.SetActive(true);
        Debug.Log("DELETE name: " + key);
        DatabaseQAHandler.DeleteCourseLvlQn(key, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Delete elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Deleted successfully.";
        key = null;
    }


    //Click a course button
    public void ClickLevel(GameObject item)
    {
        trQnViewModel.key = item.name;
        trQnViewModel.courseName = courseName;
        trQnViewModel.userName = userName;
        trQnViewModel.WakeUp();
        Debug.Log("CLICK COURSE ITEM NAME: " + item.name);
        UIController.OpenTrQnCanvas();
    }

    //Wakeup call 
    public async void WakeUp()
    {
        Debug.Log("Course Name In Special Levels: " + courseName);
        Debug.Log("Username In Special Levels: " + userName);
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    //When click close
    public void closeSpecialLvl()
    {
        enrollViewModel.WakeUp();
        UIController.SpecialLevelToEnrollmentCanvas();
    }
}

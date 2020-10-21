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

public class SpecialLevelController : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader;
    public string courseName;
    public string userName;
    public bool chk = false;
    public CourseController courseController;
    public UIControllerScript UIController;
    public TrQnViewModel trQnViewModel;

    // Start is called before the first frame update
    void Start()
    {
       /* Debug.Log("Course Name In Special Levels: " + courseName);
        Debug.Log("Username In Special Levels: " + userName);
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);*/
    }

    public async Task Read()
    {
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
        DatabaseQAHandler.GetCourseLvlQns(courseLvlQns =>
        {
            foreach (var courseLvlQn in courseLvlQns)
            {
                Debug.Log($"{courseLvlQn.Key} {courseLvlQn.Value.courseName} {courseLvlQn.Value.userName} {courseLvlQn.Value.level}");
                if (courseLvlQn.Value.courseName == courseName && courseLvlQn.Value.userName == userName)
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
    public List<string> students = new List<string>();
    public async Task InvokeLvlCheckExist(int lvlNo)
    {
        Task<bool> task = CheckLvlExist(lvlNo);
        bool lvlExist = await task;
        Debug.Log("Level Exist: " + lvlExist);
        if (lvlExist == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Level " + lvlNo + " already exist.";
        }
        else
        {
            var courseLvlQn = new CourseLvlQn(courseName, userName, lvlNo, qns, ans, students);
            loader.SetActive(true);
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
                Debug.Log($"{courseLvlQn.Key} {courseLvlQn.Value.courseName} {courseLvlQn.Value.userName} {courseLvlQn.Value.level}");
                if (courseLvlQn.Value.userName == userName && courseLvlQn.Value.courseName == courseName)
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
    public void ClickCourse(GameObject item){
        trQnViewModel.key = item.name;
        trQnViewModel.courseName = courseName;
        trQnViewModel.userName = userName;
        trQnViewModel.WakeUp();
        Debug.Log("CLICK COURSE ITEM NAME: "+item.name);
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

    public void closeSpecialLvl()
    {
        courseController.WakeUp();
        UIController.CloseSpecialLevelCanvas();
    }

    // Update is called once per frame
    void Update()
    {
    }
}

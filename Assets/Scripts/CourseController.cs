using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CourseController : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader, specialLevelCanvas;
    public InputField courseInput;
    public static string userName = "Jame"; //retrieve user
    public bool chk = false;
    public SpecialLevelController specialLevelController;
    public UIControllerScript UIController;

    // Start is called before the first frame update
    async void Start()
    {
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    // Create course buttons
    public async Task Read()
    {
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
        DatabaseQAHandler.GetCourses(courses =>
        {
            foreach (var course in courses)
            {
                Debug.Log($"{course.Key} {course.Value.userName}");
                if (course.Value.userName == userName)
                {
                    GameObject tmp_btn = Instantiate(item, itemParent.transform);
                    tmp_btn.name = course.Key;
                    Debug.Log("item name: " + tmp_btn.name);
                    tmp_btn.transform.GetChild(1).GetComponent<Text>().text = course.Key;
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

    //When Cancel is clicked 
    public void Cancel()
    {
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
    }

    //Course creation check
    public async void CreateCourse()
    {
        string courseName = courseInput.text;
        bool handler = Check();
        if (handler == true)
        {
            await InvokeCourseCheckExist(courseName);
        }
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
        chk = false;
    }


    //Basic checking of form elements
    public bool Check()
    {
        string str = Regex.Replace(courseInput.text, @"\s", "");
        if (courseInput.text == null || str == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a course name.";
            return false;
        }
        return true;
    }

    // Invoke checkCourseExist
    public async Task InvokeCourseCheckExist(string courseName)
    {
        Task<bool> task = CheckCourseExist(courseName);
        bool courseExist = await task;
        Debug.Log("Course Exist: " + courseExist);
        if (courseExist == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = courseName + " already exist.";
        }
        else
        {
            var course = new Course(userName);
            loader.SetActive(true);
            await PostingCourse(course, courseName);
            await Read();
            loader.SetActive(false);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = courseName + " created successfully.";

        }
    }

    // Creating course in database
    public async Task PostingCourse(Course course, string courseName)
    {
        DatabaseQAHandler.PostCourse(course, courseName, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Course Elapsed milliseconds: {0}" + sec);
    }

    // Check if the course already exist
    public async Task<bool> CheckCourseExist(string courseName)
    {
        DatabaseQAHandler.GetCourse(courseName, course =>
        {
            Debug.Log($"{course.userName} exist.");
            chk = true;
            Debug.Log("chCourseExist1" + chk);
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(500).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("check course exist elapsed milliseconds: {0}" + sec);
        return chk;
    }


    //Check the name of the course to be deleted
    public string courseKey;
    public void CheckItemDelName(GameObject item)
    {
        courseKey = item.name;
        Debug.Log("CHECK DELETE name: " + courseKey);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to delete " + courseKey + "?";

    }

    //Delete course in database
    public async void DeleteCourse()
    {
        loader.SetActive(true);
        Debug.Log("DELETE name: " + courseKey);
        DatabaseQAHandler.DeleteCourse(courseKey, () => { });
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
        messageBox.transform.GetChild(1).GetComponent<Text>().text = courseKey + " deleted successfully.";
        courseKey = null;
    }

    //Click a course button
    public void ClickCourse(GameObject item)
    {
        specialLevelController.courseName = item.name;
        specialLevelController.userName = userName;
        specialLevelController.WakeUp();
        Debug.Log("CLICK COURSE ITEM NAME: " + item.name);
        UIController.OpenSpecialLevelCanvas();
    }

    //wake up 
    public async void WakeUp()
    {
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
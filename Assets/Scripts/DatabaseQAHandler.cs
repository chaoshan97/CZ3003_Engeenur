using System.Collections.Generic;
using FullSerializer;
using Proyecto26;

public static class DatabaseQAHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void DelCourseCallback();
    public delegate void PostCourseCallback();
    public delegate void GetCourseCallback(Course course);
    public delegate void GetCoursesCallback(Dictionary<string, Course> courses);
    public delegate void DelLvlCallback();
    public delegate void PostLvlCallback();
    public delegate void GetLvlCallback(CourseLvlQn courseLvlQn);
    public delegate void GetLvlsCallback(Dictionary<string, CourseLvlQn> courseLvlQns);
    public delegate void GetUsersCallback(Dictionary<string, UserData> userDatas);

    //Create a Course
    public static void PostCourse(Course course, string courseName, PostCourseCallback callback)
    {
        RestClient.Put<Course>($"{databaseURL}course/{courseName}.json", course).Then(response => { callback(); });
    }

    //Retrieve All Courses
    public static void GetCourses(GetCoursesCallback callback)
    {
        RestClient.Get($"{databaseURL}course.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Course>), ref deserialized);

            var courses = deserialized as Dictionary<string, Course>;
            callback(courses);
        });
    }

    //Retrieve a Course
    public static void GetCourse(string courseName, GetCourseCallback callback)
    {
        RestClient.Get<Course>($"{databaseURL}course/{courseName}.json").Then(course => { callback(course); });
    }

    //Delete a Course
    public static void DeleteCourse(string courseKey, DelCourseCallback callback)
    {
        RestClient.Delete($"{databaseURL}course/" + courseKey + ".json").Then(response => { callback(); });
    }

    //Update a Course
    public static void PutCourse(string courseName, Course course, PostCourseCallback callback)
    {
        RestClient.Put($"{databaseURL}course/"+courseName+".json", course).Then(response => { callback(); });
    }

    //Create a level
    public static void PostCourseLvlQn(CourseLvlQn courseLvlQn, PostLvlCallback callback)
    {
        RestClient.Post<CourseLvlQn>($"{databaseURL}courseLevelQns/.json", courseLvlQn).Then(response => { callback(); });
    }

    //Retrieve a course level 
    public static void GetCourseLvlQn(string id, GetLvlCallback callback)
    {
        RestClient.Get<CourseLvlQn>($"{databaseURL}courseLevelQns/{id}.json").Then(courseLvlQn => { callback(courseLvlQn); });
    }

    //Retrieve all course levels
    public static void GetCourseLvlQns(GetLvlsCallback callback)
    {
        RestClient.Get($"{databaseURL}courseLevelQns.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, CourseLvlQn>), ref deserialized);

            var courseLvlQns = deserialized as Dictionary<string, CourseLvlQn>;
            callback(courseLvlQns);
        });
    }

    //Delete a Course Level
    public static void DeleteCourseLvlQn(string key, DelLvlCallback callback)
    {
        RestClient.Delete($"{databaseURL}courseLevelQns/" + key + ".json").Then(response => { callback(); });
    }

    //Update a Course Level Atrributes
    public static void PutCourseLvlQn(string key, CourseLvlQn courseLvlQn, PostLvlCallback callback)
    {
        RestClient.Put($"{databaseURL}courseLevelQns/"+key+".json", courseLvlQn);//.Then(response => { callback(); });
    }

    //Retrieve all user details
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}students.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserData>), ref deserialized);

            var userDatas = deserialized as Dictionary<string, UserData>;
            callback(userDatas);
        });
    }

}
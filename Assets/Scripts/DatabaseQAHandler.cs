using System.Collections.Generic;
using FullSerializer;
using Proyecto26;

public static class DatabaseQAHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void DelQnCallback();
    public delegate void PostQnCallback();
    public delegate void GetQnCallback(Qn qn);
    public delegate void GetQnsCallback(Dictionary<string, Qn> qns);
    public delegate void DelCourseCallback();
    public delegate void PostCourseCallback();
    public delegate void GetCourseCallback(Course course);
    public delegate void GetCoursesCallback(Dictionary<string, Course> courses);
    public delegate void DelLvlCallback();
    public delegate void PostLvlCallback();
    public delegate void GetLvlCallback(CourseLvlQn courseLvlQn);
    public delegate void GetLvlsCallback(Dictionary<string, CourseLvlQn> courseLvlQns);

    public static void PostQn(Qn qn, PostQnCallback callback)
    {
        RestClient.Post<Qn>($"{databaseURL}qns/.json", qn).Then(response => { callback(); });
    }

    public static void GetQn(string qnId, GetQnCallback callback)
    {
        RestClient.Get<Qn>($"{databaseURL}qns/{qnId}.json").Then(qn => { callback(qn); });
    }

    public static void GetQns(GetQnsCallback callback)
    {
        RestClient.Get($"{databaseURL}qns.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Qn>), ref deserialized);

            var qns = deserialized as Dictionary<string, Qn>;
            callback(qns);
        });
    }

    public static void DeleteQn(string qnKey, DelQnCallback callback)
    {
        RestClient.Delete($"{databaseURL}qns/" + qnKey + ".json").Then(response => { callback(); });
    }

    public static void PutQn(string qnKey, Qn qn, PostQnCallback callback)
    {
        RestClient.Put("https://engeenur-17baa.firebaseio.com/qns/" + qnKey + ".json", qn);
        //RestClient.Put($"{databaseURL}qns/"+qnKey+".json", qn);//.Then(response => { callback(); });
    }

    public static void PostCourse(Course course, string courseName, PostCourseCallback callback)
    {
        RestClient.Put<Course>($"{databaseURL}course/{courseName}.json", course).Then(response => { callback(); });
    }
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

    public static void GetCourse(string courseName, GetCourseCallback callback)
    {
        RestClient.Get<Course>($"{databaseURL}course/{courseName}.json").Then(course => { callback(course); });
    }

    public static void DeleteCourse(string courseKey, DelCourseCallback callback)
    {
        RestClient.Delete($"{databaseURL}course/" + courseKey + ".json").Then(response => { callback(); });
    }

    public static void PostCourseLvlQn(CourseLvlQn courseLvlQn, PostLvlCallback callback)
    {
        RestClient.Post<CourseLvlQn>($"{databaseURL}courseLevelQns/.json", courseLvlQn).Then(response => { callback(); });
    }

    public static void GetCourseLvlQn(string id, GetLvlCallback callback)
    {
        RestClient.Get<CourseLvlQn>($"{databaseURL}courseLevelQns/{id}.json").Then(courseLvlQn => { callback(courseLvlQn); });
    }

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

    public static void DeleteCourseLvlQn(string key, DelLvlCallback callback)
    {
        RestClient.Delete($"{databaseURL}courseLevelQns/" + key + ".json").Then(response => { callback(); });
    }
    public static void PutCourseLvlQn(string key, CourseLvlQn courseLvlQn, PostLvlCallback callback)
    {
        RestClient.Put($"{databaseURL}courseLevelQns/"+key+".json", courseLvlQn);//.Then(response => { callback(); });
    }

}
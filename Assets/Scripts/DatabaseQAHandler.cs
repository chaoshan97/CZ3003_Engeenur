//using System.Collections.Generic;
//using FullSerializer;
//using Proyecto26;

//public static class DatabaseQAHandler
//{
//    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

//    private static fsSerializer serializer = new fsSerializer();

//    public delegate void DelQnCallback();
//    public delegate void PostQnCallback();
//    public delegate void GetQnCallback(Qn qn);
//    public delegate void GetQnsCallback(Dictionary<string, Qn> qns);


//    /// <summary>
//    /// Adds a user to the Firebase Database
//    /// </summary>
//    /// <param name="qn"> User object that will be uploaded </param>
//    /// <param name="qnId"> Id of the user that will be uploaded </param>
//    /// <param name="callback"> What to do after the user is uploaded successfully </param>
//    public static void PostQn(Qn qn, PostQnCallback callback)
//    {
//        RestClient.Post<Qn>($"{databaseURL}qns/.json", qn).Then(response => { callback(); });
//    }

//    /// <summary>
//    /// Retrieves a user from the Firebase Database, given their id
//    /// </summary>
//    /// <param name="qnId"> Id of the user that we are looking for </param>
//    /// <param name="callback"> What to do after the user is downloaded successfully </param>
//    public static void GetQn(string qnId, GetQnCallback callback)
//    {
//        RestClient.Get<Qn>($"{databaseURL}qns/{qnId}.json").Then(qn => { callback(qn); });
//    }

//    /// <summary>
//    /// Gets all users from the Firebase Database
//    /// </summary>
//    /// <param name="callback"> What to do after all users are downloaded successfully </param>
//    public static void GetQns(GetQnsCallback callback)
//    {
//        RestClient.Get($"{databaseURL}qns.json").Then(response =>
//        {
//            var responseJson = response.Text;

//            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
//            // to serialize more complex types (a Dictionary, in this case)
//            var data = fsJsonParser.Parse(responseJson);
//            object deserialized = null;
//            serializer.TryDeserialize(data, typeof(Dictionary<string, Qn>), ref deserialized);

//            var qns = deserialized as Dictionary<string, Qn>;
//            callback(qns);
//        });
//    }

//    public static void DeleteQn(string qnKey, DelQnCallback callback)
//    {
//        RestClient.Delete($"{databaseURL}qns/" + qnKey + ".json").Then(response => { callback(); });
//    }

//    public static void PutQn(string qnKey, Qn qn, PostQnCallback callback)
//    {
//        RestClient.Put("https://engeenur-17baa.firebaseio.com/qns/" + qnKey + ".json", qn);
//        //RestClient.Put($"{databaseURL}qns/"+qnKey+".json", qn);//.Then(response => { callback(); });
//    }
//}
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using System.Linq;

public static class LeadershipDBHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void GetLeadershipCallback(List<KeyValuePair<string, TheScore>> itemDict);

    public static void GetLeadershipRanking(GetLeadershipCallback callback) 
    {
        RestClient.Get($"{databaseURL}score.json").Then(res => 
        {
            // parsing JSON into Item object
            var responseJson = res.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, TheScore>), ref deserialized);

            var scoreDict = deserialized as Dictionary<string, TheScore>;
            
            var scoreList = scoreDict.OrderByDescending(t => t.Value.levelNo).ThenByDescending(t => t.Value.score).ToList();

            callback(scoreList);
        });
    }
}
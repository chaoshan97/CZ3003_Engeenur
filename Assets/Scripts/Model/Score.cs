public class TheScore
{
    public string scoreId { get; set; }
    public string studentName { get; set; }
    private int levelNo;
    public int LevelNo 
    { 
        get
        {
            return levelNo;
        } 

        set 
        {
            // minimum level is 1
            if (value > 0)
                levelNo = value;
            else
                levelNo = 1;
        } 
    }

    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            // no such thing as negative score
            if (value >= 0)
                score = value;
            else
                score = 0;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUIScript : MonoBehaviour
{
    public Text score;
    public Text gold;
    public Text experience;

    public void setResults(int _score, int _gold, int _experience)
    {
        score.text += _score.ToString();
        gold.text += _gold.ToString();
        experience.text += _experience.ToString();
    }

    public void reset()
    {
        score.text = "Score: ";
        gold.text = "Gold: ";
        experience.text = "Experience: ";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

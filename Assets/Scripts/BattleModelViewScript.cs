using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityWeld.Binding;

[Binding]
public class BattleModelViewScript : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private PlayerScript playerScript;
    private GameObject player;
    public GameObject playerPrefab;
    public GameObject playerPos;
    public MonsterControllerScript monsterController;
    public double timer;
    public GameObject monsterPrefab;
    public GameObject monsterPos;
    private GameObject monster;
    private MonsterScript monsterScript;
    private float timer2 = 0;
    private int count = 100;
    private Question questionScript = new Question(0, "");
    public Text answerInput;

    private List<Question> listOfQuestions = new List<Question>();

   [Binding]
    public int MonsterHealth
    {
        get
        {
            // check if monsterScript exists
            if (monsterScript)
                return monsterScript.Health;
            return 0;
        }
        set
        {
            if (monsterScript.Health == value)
            {
                return; // No change.
            }

            monsterScript.Health = value;

            //check if property changed and monsterScript exists
            if (PropertyChanged != null && monsterScript)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("MonsterHealth"));
            }
        }
    }

    [Binding]
    public string MonsterName
    {
        get
        {
            // check if monsterScript exists
            if (monsterScript)
                return monsterScript.MonsterName;
            return "";
        }
        set
        {
            if (monsterScript.MonsterName == value)
            {
                return; // No change.
            }

            monsterScript.MonsterName = value;

            //check if property changed and monsterScript exists
            if (PropertyChanged != null && monsterScript)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("MonsterName"));
            }
        }
    }

    [Binding]
    public int PlayerHealth
    {
        get
        {
            if (playerScript)
                return playerScript.Health;
            return 0;
        }
        set
        {
            if (playerScript.Health == value)
            {
                return; // No change.
            }

            playerScript.Health = value;

            //check if property changed
            if (PropertyChanged != null && playerScript)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("PlayerHealth"));
            }
        }
    }

    [Binding]
    public string PlayerName
    {
        get
        {
            if (playerScript)
                return playerScript.UserName;
            return "";
        }
        set
        {
            if (playerScript.UserName == value)
            {
                return; // No change.
            }

            playerScript.UserName = value;

            //check if property changed
            if (PropertyChanged != null && playerScript)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("PlayerName"));
            }
        }
    }

    [Binding]
    public string QuestionString
    {
        get
        {
            if (questionScript != null)
                return questionScript.Questions;
            return "";
        }
        set
        {
            if (questionScript.Questions == value)
            {
                return; // No change.
            }

            questionScript.Questions = value;

            //check if property changed
            if (PropertyChanged != null && questionScript != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("QuestionString"));
            }
        }
    }

    [Binding]
    public int Answer
    {
        get
        {
            if (questionScript != null)
                return questionScript.Answer;
            return 0;
        }
        set
        {
            if (questionScript.Answer == value)
            {
                return; // No change.
            }

            questionScript.Answer = value;

            //check if property changed
            if (PropertyChanged != null && questionScript != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Answer"));
            }
        }
    }

    private void Awake()
    {
        //instantiate monsterGameObject based on prefab
        monster = Instantiate(monsterPrefab);
        player = Instantiate(playerPrefab);

        //Set monster to predefined pos by parenting predefined ui element to monster
        monster.transform.SetParent(monsterPos.transform, false);
        player.transform.SetParent(playerPos.transform, false);
        //Get reference of the gameobject monster's monsterscript
        monsterScript = monster.GetComponent<MonsterScript>();
        playerScript = player.GetComponent<PlayerScript>();
        // initialize the monster values TODO fetch from firebase
        monsterScript.init(1, 50, 10);
        PlayerName = "tanbp";
        PlayerHealth = 100;
        playerScript.init(PlayerName, PlayerHealth, 1, 1, 1, 1);
        

        Question tempQuestion;
        //Fetch questions
        for (int i = 0; i < 3; ++i)
        {
            listOfQuestions.Add(new Question(i+i, i +"+"+ i));
        }
        Debug.Log(listOfQuestions[0].Questions);
        questionScript = listOfQuestions[0];
        QuestionString = listOfQuestions[0].Questions;
        Answer = listOfQuestions[0].Answer;
    }

    private void attackMonster()
    {

    }

    public void checkAnswer()
    {
        if (int.Parse(answerInput.text) == Answer)
        {
            MonsterHealth -= playerScript.Damage;
        }
        else
        {
            PlayerHealth -= monsterScript.Damage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;

        if (timer2 >= 1f)
        {
            // example of directly modifying monster health
            MonsterHealth--;
            timer2 = 0;
        }
    }
}

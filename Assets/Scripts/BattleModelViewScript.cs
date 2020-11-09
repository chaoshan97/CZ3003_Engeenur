using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityWeld.Binding;
using Proyecto26;

[Binding]
public class BattleModelViewScript : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private PlayerScript playerScript;
    private GameObject player;
    public GameObject playerPrefab;
    public GameObject playerPos;
    public MonsterControllerScript monsterController;
    public GameObject monsterPrefab;
    public GameObject monsterPos;
    private GameObject monster;
    private MonsterScript monsterScript;
    public TimerBarScript timerBarScript;

    private Question questionScript = new Question(0, "");
    public Text answerInput;
    public ResultUIScript resultUIScript;
    public GameObject questionUI;

    private List<Question> listOfQuestions = new List<Question>();
    private int score = 0;

    public MainMenuControllerScript mainMenuControllerScript;
    public UserData userData;
    private bool levelLoaded = false;
    public GetDatabaseQuestions normal;
    public CourseSelectionController special;
    private bool isThisNormalLevel = false;

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
        init();
    }

    public void init()
    {
        //instantiate monsterGameObject based on prefab
        questionUI.SetActive(true);
        monster = Instantiate(monsterPrefab);
        player = Instantiate(playerPrefab);

        //Set monster to predefined pos by parenting predefined ui element to monster
        monster.transform.SetParent(monsterPos.transform, false);
        player.transform.SetParent(playerPos.transform, false);
        //Get reference of the gameobject monster's monsterscript
        monsterScript = monster.GetComponent<MonsterScript>();
        playerScript = player.GetComponent<PlayerScript>();
        // initialize the monster values TODO fetch from firebase

        MonsterHealth = 30;
        MonsterName = "Nyx";

        monsterScript.init(1, MonsterHealth, 10, 20, 30);
        PlayerName = "tanbp";
        PlayerHealth = 100;
        playerScript.init(PlayerName, PlayerHealth, 10, 1, 1, 1);
        levelLoaded = true;
    }

    private void attackMonster()
    {

    }

    private bool setNewQuestion()
    {
        listOfQuestions.RemoveAt(0);
        if (listOfQuestions.Count != 0)
        {
            QuestionString = listOfQuestions[0].Questions;
            Answer = listOfQuestions[0].Answer;
            questionScript = listOfQuestions[0];
            return true;
        }
        return false;
    }

    private void toggleResultScreen()
    {
        questionUI.SetActive(false);
        resultUIScript.gameObject.SetActive(true);
        userData.setCoin(userData.getCoin() + monsterScript.Coin);
        //level up
        if (userData.getMaxExperience() <= userData.getExperience() + monsterScript.Experience)
        {
            userData.setLevel(userData.getLevel() + 1);
            userData.setExperience(userData.getMaxExperience() - userData.getExperience() + monsterScript.Experience);
        }
        else
            userData.setExperience(userData.getExperience() + monsterScript.Experience);
        LoginDbHandler.UpdateToDatabase(userData);
        if (PlayerHealth <= 0)
        {
            resultUIScript.setResults(score, 0, 0);
        }
        else
        {
            resultUIScript.setResults(score, monsterScript.Coin, monsterScript.Experience);
        }
    }

    public void checkAnswer()
    {
        if (listOfQuestions.Count == 0)
        {
            Debug.Log("no more questions");
            return;
        }
        if (answerInput.text == "")
        {
            monsterScript.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            PlayerHealth -= monsterScript.Damage;
        }
        else if (int.Parse(answerInput.text) == Answer)
        {
            playerScript.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            if (MonsterHealth - playerScript.Damage < 0)
                MonsterHealth = 0;
            else
                MonsterHealth -= playerScript.Damage;

            ++score;
        }
        else
        {
            monsterScript.gameObject.GetComponent<Animator>().SetTrigger("Attack");
            PlayerHealth -= monsterScript.Damage;
        }

        if (!setNewQuestion() || MonsterHealth <= 0 || PlayerHealth <= 0)
        {
            //goto result page
            if (PlayerHealth == 0)
            {
                //play player death animation
            }
            else
            {
                //play monster death animation

            }
            toggleResultScreen();
            //reset battle
        }
    }

        // Start is called before the first frame update
    void Start()
    {
        userData = mainMenuControllerScript.getUserData();
        PlayerName = userData.userName;
        PlayerHealth = userData.hp;
    }

    public void resetLevel()
    {
        levelLoaded = false;
        Destroy(monster);
        Destroy(player);
        monsterScript = null;
        playerScript = null;
        listOfQuestions.Clear();
        timerBarScript.barDisplay = 1;
        score = 0;
        resultUIScript.reset();
        init();
        isThisNormalLevel = false;
    }

    void LoadLevel()
    {
        if (levelLoaded && listOfQuestions.Count == 0)
        {
            //Fetch questions
            if (normal.question.Count != 0)
            {
                Debug.Log("playing normal level" + normal.levelNo);
                for (int i = 0; i < normal.question.Count; ++i)
                {
                    listOfQuestions.Add(new Question(normal.answer[i], normal.question[i]));
                }
                QuestionString = listOfQuestions[0].Questions;
                questionScript = listOfQuestions[0];
                Answer = listOfQuestions[0].Answer;
                normal.question.Clear();
                normal.answer.Clear();
                isThisNormalLevel = true;
            }
            if (special.question.Count != 0)
            {
                Debug.Log("playing special level" + special.levelNo);
                for (int i = 0; i < special.question.Count; ++i)
                {
                    listOfQuestions.Add(new Question(special.answer[i], special.question[i]));
                }
                QuestionString = listOfQuestions[0].Questions;
                questionScript = listOfQuestions[0];
                Answer = listOfQuestions[0].Answer;
                special.question.Clear();
                special.answer.Clear();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        LoadLevel();
        if (timerBarScript.barDisplay <= 0 ||  Input.GetKeyUp(KeyCode.Return))
        {
            checkAnswer();
            answerInput.text = "";
            timerBarScript.barDisplay = 1;
        }
    }
}

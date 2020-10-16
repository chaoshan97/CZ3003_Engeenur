using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityWeld.Binding;

[Binding]
public class BattleControllerScript : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public PlayerScript player;
    public MonsterControllerScript monsterController;
    public double timer;
    public GameObject monsterPrefab;
    public GameObject monsterPos;
    private GameObject monster;
    public MonsterScript monsterScript;
    private float timer2 = 0;
    private int count = 100;

    [Binding]
    public int MonsterHealth
    {
        get
        {
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

            if (PropertyChanged != null && monsterScript)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("MonsterHealth"));
            }
        }
    }

    private void Awake()
    {
        //monster = new GameObject();
        //monster = monsterController.getMonster(1);
        monster = Instantiate(monsterPrefab);
        monster.transform.SetParent(monsterPos.transform, false);
        monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.init(1, 50, 10);
    }

    public void SetMonsterHealth(int _health)
    {
        monsterScript.Health = _health;
        //monsterHealth = _health.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        //monsterScript = monster.GetComponent<MonsterScript>();
        // if (monster)
        //     monster.transform.parent = monsterPos.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;

        if (timer2 >= 1f)
        {
            MonsterHealth--;
            timer2 = 0;
        }
    }
}

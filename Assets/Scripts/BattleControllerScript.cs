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

    private void Awake()
    {
        //instantiate monsterGameObject based on prefab
        monster = Instantiate(monsterPrefab);

        //Set monster to predefined pos by parenting predefined ui element to monster
        monster.transform.SetParent(monsterPos.transform, false);
        //Get reference of the gameobject monster's monsterscript
        monsterScript = monster.GetComponent<MonsterScript>();
        // initialize the monster values TODO fetch from firebase
        monsterScript.init(1, 50, 10);
    }

    // call when monster take damage
    public void SetMonsterHealth(int _health)
    {
        monsterScript.Health = _health;
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

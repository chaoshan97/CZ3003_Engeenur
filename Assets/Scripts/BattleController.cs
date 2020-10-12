using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public PlayerScript player;
    public MonsterControllerScript monsterController;
    public double timer;
    public GameObject monsterPrefab;
    public GameObject monsterPos;
    private GameObject monster;

    private void Awake()
    {
        //monster = new GameObject();
        //monster = monsterController.getMonster(1);
    }

    // Start is called before the first frame update
    void Start()
    {

        // if (monster)
        //     monster.transform.parent = monsterPos.transform;
        monster = Instantiate(monsterPrefab);
        monster.transform.SetParent(monsterPos.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private int id;
    private int health;
    private int damage;

    public MonsterScript(int _id, int _health, int _damage)
    {
        id = _id;
        health = _health;
        damage = _damage;
    }

    public int Id { get => id; set => id = value; }
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }

    public void init(int _id, int _health, int _damage)
    {
        id = _id;
        health = _health;
        damage = _damage;
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

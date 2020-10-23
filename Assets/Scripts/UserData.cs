using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string userName;
    public int level;
    public int experience;
    public int maxExperience;
    public int hp;
    public int coin;
    public bool verified;
    public string localId;

    public UserData()
    {
        this.userName = userName;//email
        this.level = level;
        this.experience = experience;
        this.maxExperience = maxExperience;
        this.hp = hp;
        this.coin = coin;
        this.verified = verified;
        this.localId = localId;
    }

    //  public string getUserName()
    // {
    //     return email;
    // }

    public string getName()
    {
        return userName;
    }

    public int getLevel()
    {
        return level;
    }

    public int getExperience()
    {
        return experience;
    }

    public int getMaxExperience()
    {
        return maxExperience;
    }

    public int getHp()
    {
        return hp;
    }

    public int getCoin()
    {
        return coin;
    }

    public bool getVerified()
    {
        return verified;
    }
}

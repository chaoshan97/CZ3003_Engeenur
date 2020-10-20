using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData 
{
    /*
    public string username;
    public string password;
    public string id;

    public string getUserName()
    {
        return username;
    }
    */
        public string id;
        public string userName; //Email
        public string name;
        public int level;
        public int experience;
        public int maxExperience;
        public int hp;
        public int coin;
        public bool verified;

        public string getUserName()
        {
            return userName;
        }


        public string getName()
        {
            return name;
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

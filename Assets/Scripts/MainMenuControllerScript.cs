using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainMenuControllerScript : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text Name = null;

    [SerializeField]
    private UnityEngine.UI.Text LevelValue = null;

    [SerializeField]
    private UnityEngine.UI.Text ExpValue = null;

    [SerializeField]
    private UnityEngine.UI.Text HealthValue = null;

    [SerializeField]
    private UnityEngine.UI.Text CoinValue = null;

    UserData data = new UserData();

    [SerializeField]
    private UnityEngine.UI.Text TeacherName = null;

    TeacherData teacherData = new TeacherData();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public UserData getUserData()
    {
        return data;
    }

    public TeacherData getTeacherData()
    {
        return teacherData;
    }

    public void setUserData(UserData user)
    {
        Debug.Log("SET USER DATA: " + user.userName);
       /* UserData userData = new UserData();
        userData.coin = user.coin;
        userData.email = user.email;
        userData.experience = user.experience;
        userData.hp = user.hp;
        userData.level = user.level;
        userData.localId = user.localId;
        userData.maxExperience = user.maxExperience;
        userData.userName = user.userName;
        userData.verified = user.verified;
        data = userData; */
        
        
        data = user; //Object reference not set to instance error 
    }

    public void loadMainMenu()
    {
        Name.text = data.userName;
        LevelValue.text = data.level.ToString();
        ExpValue.text = data.experience.ToString() + "/" + data.maxExperience.ToString();
        HealthValue.text = data.hp.ToString();
        CoinValue.text = data.coin.ToString();
        /*Name.text = data.getName();
        LevelValue.text = data.getLevel().ToString();
        ExpValue.text = data.getExperience().ToString() + "/" + data.getMaxExperience().ToString();
        HealthValue.text = data.getHp().ToString();
        CoinValue.text = data.getCoin().ToString();*/
    }

    public void setTeacherData(TeacherData teacher)
    {
        Debug.Log("SET teacher DATA: " + teacher.userName);
        teacherData = teacher;
    }
    public void loadTeacherMainMenu()
    {
        TeacherName.text = teacherData.getName();
    }
}

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

    public void setUserData(UserData user)
    {
        data = user;
    }

    public void loadMainMenu()
    {
        Name.text = data.getName();
        LevelValue.text = data.getLevel().ToString();
        ExpValue.text = data.getExperience().ToString() + "/" + data.getMaxExperience().ToString();
        HealthValue.text = data.getHp().ToString();
        CoinValue.text = data.getCoin().ToString();
    }

    public void setTeacherData(TeacherData teacher)
    {
        teacherData = teacher;
    }
    public void loadTeacherMainMenu()
    {
        TeacherName.text = teacherData.getName();
    }
}

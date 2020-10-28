using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26; //RestClient API
using FullSerializer;

public class DynamicButtons : MonoBehaviour
{

    //int level=0;
    //public List<String> myKeys;
    //private Button myBtn;

    public void SetBtnInvisible(int lvl)
    {

        
        for (int i=10; i > lvl; i--)
        {
            Button myBtn = GameObject.Find("Stg" + i).GetComponent<Button>();
            myBtn.gameObject.SetActive(false);
        }
        
    }
    // Start is called before the first frame update
    /*public void AvailableStage()
    {
        SetBtnInvisible(level);
    }*/



}

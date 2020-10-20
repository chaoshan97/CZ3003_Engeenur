using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicButtons : MonoBehaviour
{
    int level = 3;
    //private Button myBtn;
   
    public void setBtnInvisible(int lvl)
    {
        
        for (int i=10; i > lvl; i--)
        {
            Button myBtn = GameObject.Find("Stg" + i).GetComponent<Button>();
            myBtn.gameObject.SetActive(false);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        setBtnInvisible(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

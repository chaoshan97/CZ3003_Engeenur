using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBarScript : MonoBehaviour
{
    public float barDisplay = 1; //current progress
    public Vector2 pos = new Vector2(0, 0);
    public Vector2 size = new Vector2(500, 40);
    public Texture2D emptyTex;
    public Texture2D fullTex;
    private float reductionSpeed = 15;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.DrawTexture(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.DrawTexture(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    void setReductionSpeed(float _value)
    {
        reductionSpeed = _value;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //for this example, the bar display is linked to the current time,
        //however you would set this value based on your desired display
        //eg, the loading progress, the player's health, or whatever.
        barDisplay -= (float)(Time.deltaTime/ reductionSpeed);
        //        barDisplay = MyControlScript.staticHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRemain : MonoBehaviour
{
    // Start is called before the first frame update
    public int numOfenemy = 0;
    private string level;
    void Start()
    {
        //numOfenemy = LevelManager.numOfEnemy;
        level = PlayerPrefs.GetString("Level");
    }

    // Update is called once per frame
    void Update()
    {
        numOfenemy = LevelManager.numOfEnemy;
    }

    private void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle(GUI.skin.box);
        myStyle.fontSize = 20;
        myStyle.normal.textColor = Color.black;
        myStyle.hover.textColor = Color.red;
        GUIStyle numStyle = new GUIStyle();
        numStyle.fontSize = 40;
        numStyle.normal.textColor = Color.white;
        GUIStyle levelStyle = new GUIStyle();
        levelStyle.fontSize = 30;
        levelStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 120), "Enemies\n Remaining:", myStyle);
        GUI.Label(new Rect(45, 65, 100, 80), ""+numOfenemy,numStyle);
        //GUI.Label(new Rect(Screen.width-100, 10, 100, 120), level, levelStyle);
    }
}

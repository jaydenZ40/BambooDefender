using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private int level;
    private int randNum;

    private void Start()
    {
        level = Int32.Parse(PlayerPrefs.GetString("Level").Substring(5));
        randNum = UnityEngine.Random.Range(0, 20);
    }
    public void NextGame()
    {
        level++;
        FindObjectOfType<AudioManager>().StopBGM();
        FindObjectOfType<AudioManager>().Play("GamePlayBGM");
        GameManager.instance.SavePlayerProperty();
        SceneManager.LoadScene("Level "+ level);
    }

    public void ReplayGame()
    {
        FindObjectOfType<AudioManager>().StopBGM();
        FindObjectOfType<AudioManager>().Play("GamePlayBGM");
        SceneManager.LoadScene("Level "+level);
    }
    public void ReplayLostGame()
    {
        FindObjectOfType<AudioManager>().StopBGM();
        FindObjectOfType<AudioManager>().Play("GamePlayBGM");
        SceneManager.LoadScene("Level "+level);
    }

    public void GoToSkillTree()
    {
        //FindObjectOfType<AudioManager>().StopBGM();
        //FindObjectOfType<AudioManager>().Play("GamePlayBGM");
        SceneManager.LoadScene("SkillTree");
        //GameManager.instance.LoadPlayerProperty();
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
    private void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle(GUI.skin.box);
        myStyle.alignment = TextAnchor.UpperCenter;
        myStyle.fontSize = 36;
        myStyle.normal.textColor = Color.white;
        string[] hints = new string[6] { "You can pause the game with ESC",
            "Enemies always find the closest available route", "Tornado will push all enemies back shortly",
            "Thunder will hit all enemy in the screen", "Bamboo bait is more enticing for all enemies then the base",
            "Combinations of skills are always stronger than separate use" };
        int i = level - 1 < 3 ? level - 1 : randNum % hints.Length;
        GUI.Label(new Rect(50, 5, 1500, 60), "Did you know: " + hints[i], myStyle);
    }
}

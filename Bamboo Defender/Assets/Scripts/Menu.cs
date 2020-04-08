using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject levelSelect;
    public Text[] texts;
    public GameObject GetNamePanel, Leaderboard;
    public Text Name;
    public TextMeshProUGUI leaderName, leaderLevel, leaderTime;
    public Animator transition;


    public void Start()
    {
        int i = GameManager.instance.level;
        foreach (Text text in texts)
        {
            if (i <= 0)
            {
                text.color = Color.red;
            }
            i--;
        }
    }

    public void ShowLeaderboard()
    {
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string[] name = GameManager.instance.leaderName;
        int[] level = GameManager.instance.leaderLevel;
        float[] time = GameManager.instance.leaderTime;
        for (int i = 0; i < 10; i++)
        {
            str1 += name[i];
            str1 += "\n";
            str2 += level[i];
            str2 += "\n";
            str3 += time[i];
            str3 += "\n";
        }

        leaderName.text = str1;
        leaderLevel.text = str2;
        leaderTime.text = str3;
        Leaderboard.SetActive(true);
    }
    public void BackToMainMenuFromLeaderboard()
    {
        Leaderboard.SetActive(false);
    }

    public void ShowGetNamePanel()
    {
        GetNamePanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        GetNamePanel.SetActive(false);
    }

    public void StartGame()
    {        
        GameManager.instance.playerName = Name.text;
        SceneManager.LoadScene("StoryScene");
    }
    
    public void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
        PlayerController.instance.level = 1;
    }
    public void loadLevel2()
    {
        if (GameManager.instance.level >= 2)
        {
            SceneManager.LoadScene("Level 2");
        }
    }
    public void loadLevel3()
    {
        if (GameManager.instance.level >= 3)
        {
            SceneManager.LoadScene("Level 3");
        }
    }
    public void loadLevel4()
    {
        if (GameManager.instance.level >= 4)
        {
            SceneManager.LoadScene("Level 4");
        }
    }
    public void loadLevel5()
    {
        if (GameManager.instance.level >= 5)
        {
            SceneManager.LoadScene("Level 5");
        }
    }
    public void loadLevel6()
    {
        if (GameManager.instance.level >= 6)
        {
            SceneManager.LoadScene("Level 6");
        }
    }
    public void loadLevel7()
    {
        if (GameManager.instance.level >= 7)
        {
            SceneManager.LoadScene("Level 7");
        }
    }
    public void loadLevel8()
    {
        if (GameManager.instance.level >= 8)
        {
            SceneManager.LoadScene("Level 8");
        }
    }
    public void loadLevel9()
    {
        if (GameManager.instance.level >= 9)
        {
            SceneManager.LoadScene("Level 9");
        }
    }
    public void loadLevel10()
    {
        if (GameManager.instance.level >= 10)
        {
            SceneManager.LoadScene("Level 10");
        }
    }

    public void selectLevel()
    {
        levelSelect.SetActive(true);
    }
    public void back()
    {
        levelSelect.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}

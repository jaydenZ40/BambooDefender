using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenu;
    public GameObject levelSelect;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        FindObjectOfType<AudioManager>().PauseBGM();
    }

    public void Restart()
    {
        isPause = false;
        FindObjectOfType<AudioManager>().PauseBGM();
        FindObjectOfType<AudioManager>().StopBGM();
        SceneManager.LoadScene("Level " + Int32.Parse(PlayerPrefs.GetString("Level").Substring(5)));
        FindObjectOfType<AudioManager>().Play("GamePlayBGM");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().PauseBGM();
        isPause = true;
    }

    public void selectLevel()
    {
        levelSelect.SetActive(true);
    }

    public void back()
    {
        levelSelect.SetActive(false);
    }

    public void menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void quit()
    {
        Application.Quit();
    }
    public void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Animator transition;
    public float fadeTime = 1.0f;
    public static int numOfEnemy = 2;
    public Image healthBar;
    public float baseMaxHealth = 2;
    private float baseHealth;

    private void Start()
    {
        instance = this;
        numOfEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        baseHealth = baseMaxHealth;
    }

    public void KillEnemy(int n)   // this function will be called every time an enemy was killed
    {
        numOfEnemy -= n;
        StartCoroutine(CheckWin());
    }

    IEnumerator CheckWin()
    {
        int sum = 0;
        foreach (GameObject spawnPoint in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            sum += spawnPoint.GetComponent<SpawnPoint>().enemyNum;
        }

        if (numOfEnemy == 0 && sum == 0)
        {
            //Debug.Log("Win");
            GameManager.instance.SavePlayerProperty();
            yield return new WaitForSeconds(1.0f);
            PlayerController.instance.SavePlayer();
            StartCoroutine(LoadFade());
            //SceneManager.LoadScene("playerwin");

            FindObjectOfType<AudioManager>().StopBGM();
            if (GameManager.instance.level != 11)
            {
                FindObjectOfType<AudioManager>().Play("WinSFX");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("CompleteWinSFX");
            }
        }
    }

    IEnumerator LoadFade()
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("playerwin");
    }

    public void AttackBase()
    {
        baseHealth -= 1;
        healthBar.fillAmount = baseHealth / baseMaxHealth;
        if (baseHealth == 0)
        {
            Lose();
        }
    }

    void Lose()
    {
        FindObjectOfType<AudioManager>().StopBGM();
        FindObjectOfType<AudioManager>().Play("LoseSFX");
        //Debug.Log("Lose");
        SceneManager.LoadScene("playlose");
    }

    public void IncEnemyNum(int n)
    {
        numOfEnemy += n;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillBackToGame : MonoBehaviour
{
    public void GoToGame()
    {
        //FindObjectOfType<AudioManager>().StopBGM();
        //FindObjectOfType<AudioManager>().Play("GamePlayBGM");
        SceneManager.LoadScene("playerwin");
        //GameManager.instance.LoadPlayerProperty();
    }
}

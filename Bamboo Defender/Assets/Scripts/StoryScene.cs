using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryScene : MonoBehaviour
{
    public GameObject pic1, pic2, pic3, t1, t2, t3, t4;
    public TextMeshProUGUI T1, T2, T3, T4;
    public TextMeshProUGUI skipButton;

    private float timer = 0;
    private string[] str = new string[4] {"Long time ago, when history itself did not exist, there was a mystic village where many Pandas lived. ",
            "Those Pandas lived peacefully and undiscovered by other animals, and they devoted their whole lives into the bamboo plantation.",
            "However, as not everything can go as they wish for, their secret was known by other animals at last. The precious bamboo caught " +
                "every single one’s attention, and the animals around the village decided to march against and raid the Pandas.",
            "A heroic warrior realizes their dangerous situation, and he decides to protect his ethnic."};
    private bool isStory1Done = false;
    private bool isStory2Done = false;
    private bool isStory3Done = false;
    private bool isStory4Done = false;

    private void Start()
    {
        StartCoroutine(AnimateText(T1, str[0], 1));
    }

    private void Update()
    {
        if (isStory1Done)
        {
            t1.SetActive(false);
            pic1.transform.position -= Vector3.left * 0.5f;
            if (pic1.transform.position.x > 20)
            {
                t2.SetActive(true);
                isStory1Done = false;
                StartCoroutine(AnimateText(T2, str[1], 2));
            }
        }
        else if (isStory2Done)
        {
            t2.SetActive(false);
            pic2.transform.position -= Vector3.left * 0.5f;
            if (pic2.transform.position.x > 20)
            {
                t3.SetActive(true);
                isStory2Done = false;
                StartCoroutine(AnimateText(T3, str[2], 3));
            }
        }
        else if (isStory3Done)
        {
            t3.SetActive(false);
            pic3.transform.position -= Vector3.left * 0.5f;
            if (pic3.transform.position.x > 20)
            {
                t4.SetActive(true);
                isStory3Done = false;
                StartCoroutine(AnimateText(T4, str[3], 4));
            }
        }
        else if (isStory4Done)
        {
            skipButton.text = "Start"; 
        }
    }
    public void SceneToLevelOne()
    {
        FindObjectOfType<AudioManager>().StopBGM();
        FindObjectOfType<AudioManager>().Play("GamePlayBGM");
        SceneManager.LoadScene("Level 1");
    }

    IEnumerator AnimateText(TextMeshProUGUI T, string str, int n)
    {
        int i = 0;
        T.text = "";
        while (i < str.Length)
        {
            T.text += str[i++];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2.0f);
        if (n == 1)  isStory1Done = true;
        if (n == 2) isStory2Done = true;
        if (n == 3) isStory3Done = true;
        if (n == 4) isStory4Done = true;
    }
}

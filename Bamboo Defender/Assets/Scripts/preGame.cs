using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preGame : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Start()
    {
        StartCoroutine("delayStart");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delayStart()
    {
        Time.timeScale = 0f;
        float delayTime = Time.realtimeSinceStartup + 3f;
        while(Time.realtimeSinceStartup < delayTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintHolder : MonoBehaviour
{
    public GameObject hint1;
    public GameObject hint2;
    private bool ishint1 = false;
    private bool ishint2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(ishint1){
                Time.timeScale = 1f;
                hint1.SetActive(false);
                ishint1 = false;
            }
            if (ishint2)
            {
                Time.timeScale = 1f;
                hint2.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        hint1.SetActive(true);
        ishint1 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        hint2.SetActive(true);
        ishint2 = true;
    }
}

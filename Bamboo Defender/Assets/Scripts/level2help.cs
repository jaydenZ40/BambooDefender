using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2help : MonoBehaviour
{
    public GameObject thunder;
    private int helpcount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1 && helpcount == 1)
        {
            Instantiate(thunder, transform.position, Quaternion.identity);
            helpcount = 2;
        }
    }
}

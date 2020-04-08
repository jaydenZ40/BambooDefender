using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protal_toturial : MonoBehaviour
{

    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
   
  
                Time.timeScale = 1f;
                portal.SetActive(false);
                Destroy(this.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        portal.SetActive(true);
        
    }
}

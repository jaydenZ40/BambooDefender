using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public GameObject destination;
    //private float cooldown = 2f;
    //private float cd = 0;
    //private bool canTeleport = true;
    
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    cd = PlayerPrefs.GetFloat("teleport");
    //    cd -= Time.deltaTime;
    //    if (cd <= 0)
    //    {
    //        canTeleport = true;

    //    }
    //    else
    //    {
    //        canTeleport = false;
    //    }
    //    PlayerPrefs.SetFloat("teleport", cd);
    //}
    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (canTeleport)
    //    {
    //        col.transform.position = destination.transform.position;
    //        PlayerPrefs.SetFloat("teleport", 2f);
    //    }
    //    //if (other.collider.CompareTag("Enemy"))
    //    //    Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
    //    //Destroy(gameObject);
    //}

    public float delayPortalTimer = 2.0f;

    private void Start()
    {
        Invoke("ActivePortal", delayPortalTimer);
    }

    void update()
    {

    }

    void ActivePortal()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject projectile;
    public float cooldown = 2;

    private float cd = 0;
    private bool fire = false;
    private int count;
    private Animator tower_animator;

    // Start is called before the first frame update
    void Start()
    {
        tower_animator = this.GetComponent<Animator>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if (cd <= 0 && count > 0)
        {
            tower_animator.SetBool("isShooting", true);
            Invoke("Shoot", 0.5f);
            cd = cooldown;
            Invoke("SetShootingFalse", 0.5f);
        }
    }

    void SetShootingFalse()
    {
        tower_animator.SetBool("isShooting", false);
    }
    
    void Shoot()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            count++;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            count--;
        }
    }
}

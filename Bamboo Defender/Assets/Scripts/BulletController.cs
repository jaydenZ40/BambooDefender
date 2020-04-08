using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int speed;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0) { Destroy(this.gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        float closestDis = Mathf.Infinity;
        GameObject closest = null;
        Destroy(this.gameObject, 1.5f);
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                Destroy(gameObject);
            }
            else
            {
                float currentDis = (enemy.transform.position - this.transform.position).sqrMagnitude;
                if (currentDis < closestDis)
                {
                    closestDis = currentDis;
                    closest = enemy;
                }
            }
        }
        if (closest != null)
            transform.position = Vector2.MoveTowards(transform.position, closest.transform.position, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        if (other.collider.CompareTag("Enemy"))
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        Destroy(gameObject);
    }
}

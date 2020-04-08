using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float WallLifeTime = 3.0f;

    private float timer = 0;
    private bool isFlashing = false;


    // Update is called once per frame
    void Update()
    {
        if (timer >= WallLifeTime - 1.5f)
        {
            StartCoroutine(FlashInLastSecond());
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    IEnumerator FlashInLastSecond()
    {
        this.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
        AstarPath.active.Scan();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}

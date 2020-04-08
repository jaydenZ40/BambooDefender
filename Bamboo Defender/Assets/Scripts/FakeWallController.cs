using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWallController : MonoBehaviour
{
    public GameObject backgroung;

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Enemy") || other.CompareTag("Wall") || other.CompareTag("WallFreeRange"))
    //    {
    //        PlayerController.instance.SetFakeWallPossible(false);
    //        backgroung.GetComponent<SpriteRenderer>().color = Color.red - Color.black * 0.8f;
    //    }
    //}
    //void OnTriggerExit2D (Collider2D other)
    //{
    //    if (other.CompareTag("Enemy") || other.CompareTag("Wall") || other.CompareTag("WallFreeRange"))
    //    {
    //        PlayerController.instance.SetFakeWallPossible(true);
    //        backgroung.GetComponent<SpriteRenderer>().color = Color.green - Color.black * 0.8f;
    //    }
    //}
}

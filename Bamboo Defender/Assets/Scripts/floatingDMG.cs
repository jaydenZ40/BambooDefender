using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingDMG : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset = new Vector3(1, 0.5f, 0);
    void Start()
    {
        Destroy(gameObject, 3f);

        transform.localPosition += offset;
    }
}

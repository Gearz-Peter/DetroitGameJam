using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoEnd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "End")
        {
            GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
        }
    }
}

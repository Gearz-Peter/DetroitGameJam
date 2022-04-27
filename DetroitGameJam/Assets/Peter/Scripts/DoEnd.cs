using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoEnd : MonoBehaviour
{
    [SerializeField] private Texture2D goodEnd;
    [SerializeField] private Texture2D mehEnd;
    [SerializeField] private Texture2D badEnd;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "End")
        {
            GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
            GameObject.FindWithTag("EndCanvas").GetComponent<Canvas>().enabled = true;
        }
    }
}

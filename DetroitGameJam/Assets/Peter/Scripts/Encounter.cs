using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image encounterImage;

    void start()
    {
        encounterImage.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            DoEncounter();
        }
    }

    void DoEncounter()
    {
        Debug.Log("Encounter");
        GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
    }
}

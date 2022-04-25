using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
    }
}

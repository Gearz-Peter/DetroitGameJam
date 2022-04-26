using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public bool flip = false;
    private bool isDo = true;

    void Update()
    {
        if (flip)
        {
            if (isDo)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMove>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<Interact>().enabled = false;
                GameObject.FindWithTag("OverworldCanvas").GetComponent<InventoryUI>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMove>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<Interact>().enabled = true;
                GameObject.FindWithTag("OverworldCanvas").GetComponent<InventoryUI>().enabled = true;
            }
            isDo = !isDo;
            flip = false;
        }
    }
}

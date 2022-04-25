using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool inRange = false;

    [SerializeField] private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && inRange)
        {
            Debug.Log("Interact");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            inRange = true;
            canvas.GetComponent<TextManager>().DisplayText("Press F to Interact!");
            Debug.Log("start interact prompt");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            inRange = false;
            canvas.GetComponent<TextManager>().RemoveText();
            Debug.Log("end interact prompt");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool inRange = false;

    private Collider2D NPCcollider;
    private NPCInformation info;

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
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
            info = NPCcollider.GetComponentInParent<NPCInformation>();
            if (info.DialogueState >= info.Dialogue.Length)
            {
                info.DialogueState--;
                GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = true;
                canvas.GetComponent<TextManager>().RemoveText();
            }
            else
            {
                canvas.GetComponent<TextManager>().DisplayText(info.Dialogue[info.DialogueState]);
                info.DialogueState++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            NPCcollider = collider;
            inRange = true;
            canvas.GetComponent<TextManager>().DisplayText("Press F to Interact!");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            NPCcollider = null;
            inRange = false;
            canvas.GetComponent<TextManager>().RemoveText();
        }
    }
}

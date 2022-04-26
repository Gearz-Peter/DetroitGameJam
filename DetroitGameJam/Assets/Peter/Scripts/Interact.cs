using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool inConvo = false;

    private bool inRange = false;

    private Collider2D NPCcollider;
    private NPCInformation info;
    private bool stopDialogue = false;

    [SerializeField] private Canvas canvas;

    void Update()
    {
        if (Input.GetKeyDown("f") && inRange && !canvas.GetComponent<InventoryUI>().invActive)
        {
            inConvo = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
            info = NPCcollider.GetComponentInParent<NPCInformation>();
            for (int i = 0; i < info.dialogueRequirement.Length; i++)
            {
                if (info.DialogueState == info.dialogueRequirement[i] + 1)
                {
                    stopDialogue = true;
                    List<string> items = GameObject.FindWithTag("Player").GetComponent<Items>().QuestItems;
                    for (int j = 0; j < items.Count; j++)
                    {
                        if (items[j] == info.requirementNames[i])
                        {
                            stopDialogue = false;
                        }
                    }
                }
            }

            if (info.DialogueState >= info.Dialogue.Length || stopDialogue)
            {
                info.DialogueState--;
                inConvo = false;
                GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = true;
                canvas.GetComponent<TextManager>().RemoveText();
                stopDialogue = false;
            }
            else
            {
                canvas.GetComponent<TextManager>().DisplayText(info.Dialogue[info.DialogueState],info.NPCName);
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
            canvas.GetComponent<TextManager>().DisplayText("Press F to Talk!","");
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

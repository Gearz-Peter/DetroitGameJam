using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] public List<int[]> items = new List<int[]>();
    [SerializeField] public List<string> QuestItems = new List<string>();
    [SerializeField] private Canvas canvas;

    private bool isQuest;
    private Collider2D itemCollider;
    private bool inRange = false;
    private bool destroyed = false;
    private ItemInfo info;

    void Update()
    {
        if (Input.GetKeyDown("f") && inRange)
        {
            if (isQuest)
            {
                QuestItems.Add(itemCollider.name);
                canvas.GetComponent<TextManager>().DisplayText("You Picked Up The " + itemCollider.name, "");
            }
            else
            {
                info = itemCollider.GetComponentInParent<ItemInfo>();
                canvas.GetComponent<TextManager>().DisplayText("You Picked Up " + info.ItemNumber + info.ItemName, "");
                for ()
                {

                }
                int[] temp = new int[2];
                temp[0] = info.ItemID;
                temp[1] = info.ItemNumber;
                items.Add(temp);
            }
            destroyed = true;
            StartCoroutine(Pickup());
            Destroy(itemCollider.gameObject);
        }
    }

    IEnumerator Pickup()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
        yield return new WaitForSeconds(1f);
        canvas.GetComponent<TextManager>().RemoveText();
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = true;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "QuestItem")
        {
            isQuest = true;
            itemCollider = collider;
            inRange = true;
            canvas.GetComponent<TextManager>().DisplayText("Press F to Pick Up!","");
        }
        if (collider.tag == "Item")
        {
            isQuest = false;
            itemCollider = collider;
            inRange = true;
            canvas.GetComponent<TextManager>().DisplayText("Press F to Pick Up!", "");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "QuestItem")
        {
            itemCollider = null;
            inRange = false;
            if (!destroyed)
            {
                canvas.GetComponent<TextManager>().RemoveText();
            }
        }
        if (collider.tag == "Item")
        {
            itemCollider = null;
            inRange = false;
            if (!destroyed)
            {
                canvas.GetComponent<TextManager>().RemoveText();
            }
        }
    }
}

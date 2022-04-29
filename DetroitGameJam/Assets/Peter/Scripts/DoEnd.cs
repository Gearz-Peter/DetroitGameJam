using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoEnd : MonoBehaviour
{
    [SerializeField] private Sprite goodEnd;
    [SerializeField] private Sprite mehEnd;
    [SerializeField] private Sprite badEnd;

    [SerializeField] private Image endScreen;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "End")
        {
            endScreen.sprite = mehEnd;
            if (GameObject.FindWithTag("Player").GetComponent<Items>().QuestItems.Count >= 5)
            {
                endScreen.sprite = goodEnd;
            }
            if (GameObject.FindWithTag("Player").GetComponent<Encounter>().ratsBeat >= 15)
            {
                endScreen.sprite = badEnd;
            }

            GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
            GameObject.FindWithTag("EndCanvas").GetComponent<Canvas>().enabled = true;
        }
    }
}

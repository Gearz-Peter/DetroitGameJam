using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventory;

    [SerializeField] private Image[] ItemImages = new Image[10];
    [SerializeField] private TextMeshProUGUI[] ItemQuantity = new TextMeshProUGUI[10];

    public bool invActive = false;

    void Start()
    {
        CloseInv();
    }

    void Update()
    {
        if (Input.GetKeyDown("tab") && GameObject.FindWithTag("Player").GetComponent<Interact>().inConvo == false)
        {
            if (!invActive)
            {
                OpenInv();
            }
            else
            {
                CloseInv();
            }
            invActive = !invActive;
        }
    }

    void OpenInv()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
        inventory.SetActive(true);

        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Items>().inventory;

        for (int i = 0; i < 10; i++)
        {
            ItemQuantity[i].text = "x" + inv.GetQuantity(i);
            if (inv.GetQuantity(i) == 0)
            {
                ItemImages[i].gameObject.GetComponent<Image>().enabled = false;
                ItemQuantity[i].gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            else
            {
                ItemImages[i].gameObject.GetComponent<Image>().enabled = true;
                ItemQuantity[i].gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            }
        }
    }

    void CloseInv()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = true;
        inventory.SetActive(false);
    }
}

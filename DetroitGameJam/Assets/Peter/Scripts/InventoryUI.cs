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

    private bool invActive = false;

    void Start()
    {
        CloseInv();
    }

    void Update()
    {
        if (Input.GetKeyDown("tab"))
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
        inventory.SetActive(true);

        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Items>().inventory;

        for (int i = 0; i < 10; i++)
        {
            ItemQuantity[i].text = "x" + inv.GetQuantity(i);
            Debug.Log(inv.GetQuantity(i));
            if (inv.GetQuantity(i) == 0)
            {
                Debug.Log(inv.GetQuantity(i));
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
        inventory.SetActive(false);
    }
}

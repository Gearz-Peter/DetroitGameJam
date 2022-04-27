using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] public Inventory inventory = new Inventory();
    [SerializeField] public List<string> QuestItems = new List<string>();
    [SerializeField] private Canvas canvas;

    private bool isQuest;
    private Collider2D itemCollider;
    private bool inRange = false;
    private bool destroyed = false;
    private ItemInfo info;

    void Update()
    {
        if (Input.GetKeyDown("f") && inRange && !canvas.GetComponent<InventoryUI>().invActive)
        {
            if (isQuest)
            {
                QuestItems.Add(itemCollider.name);
                canvas.GetComponent<TextManager>().DisplayText("You Picked Up The " + itemCollider.name, "");
            }
            else
            {
                info = itemCollider.GetComponentInParent<ItemInfo>();
                inventory.AddQuantity(info.ItemID,info.ItemNumber);
                canvas.GetComponent<TextManager>().DisplayText("You Picked Up " + info.ItemNumber + " " + inventory.GetItemName(info.ItemID), "");    
            }
            destroyed = true;
            StartCoroutine(Pickup());
            Destroy(itemCollider.gameObject);
        }
    }

    IEnumerator Pickup()
    {
        GameObject.FindWithTag("Player").GetComponent<Interact>().inConvo = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
        yield return new WaitForSeconds(1f);
        canvas.GetComponent<TextManager>().RemoveText();
        GameObject.FindWithTag("Player").GetComponent<Interact>().inConvo = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = true;
        destroyed = false;

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

public class Inventory
{
    static public List<Item> inventory = new List<Item>();
    private Item item;

    public Inventory()
    {
        item = new Item("cola");
        inventory.Add(item);
        item = new Item("pot");
        inventory.Add(item);
        item = new Item("defibrillator");
        inventory.Add(item);
        item = new Item("4");
        inventory.Add(item);
        item = new Item("5");
        inventory.Add(item);
        item = new Item("6");
        inventory.Add(item);
        item = new Item("7");
        inventory.Add(item);
        item = new Item("8");
        inventory.Add(item);
        item = new Item("9");
        inventory.Add(item);
        item = new Item("10");
        inventory.Add(item);
    }

    public string GetItemName(int Id)
    {
        return inventory[Id].GetName();
    }

    public void AddQuantity(int Id, int num)
    {
        inventory[Id].AddQuantity(num);
    }

    public int GetQuantity(int Id)
    {
        return inventory[Id].GetQuantity();
    }
}

public class Item
{
    private int quantity;
    private int ItemID;
    private string ItemName;
    static private int numberOfItems = 0;

    public Item(string _ItemName)
    {
        ItemName = _ItemName;
        ItemID = numberOfItems;
        numberOfItems++;
        quantity = 0;
    }

    public string GetName()
    {
        return ItemName;
    }

    public void AddQuantity(int num)
    {
        quantity += num;
    }

    public int GetQuantity()
    {
        return quantity;
    }
}

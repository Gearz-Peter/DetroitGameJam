using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAction : MonoBehaviour
{
    public Items Inventory;
    [SerializeField] Text[] TextsUI,QuantityTextUI;
    [SerializeField] string[] itemText;
    [SerializeField] int[] ItemQuantity;

    [SerializeField] InputField TextField;

    string CurrentText;


  
    [SerializeField] GameObject[] allyObjects;

    [SerializeField] GameObject MainHub,HealHub;
 

    [SerializeField] GameObject AllieList;

    public int ItemType;



    private void OnEnable()
    {
       // Inventory.inventory.AddQuantity(0, 5);
       // Inventory.inventory.AddQuantity(1, 5);
       // Inventory.inventory.AddQuantity(2, 5);
        TextField.ActivateInputField();


        for (int i=0;i<TextsUI.Length;i++)
        {
            string name = Inventory.inventory.GetItemName(i);
            TextsUI[i].text = name;
            itemText[i] = name;

            QuantityTextUI[i].text = "x" + Inventory.inventory.GetQuantity(i).ToString();
            ItemQuantity[i] = Inventory.inventory.GetQuantity(i);
        }
        

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            WeaponSelect();

        }
    }


 

    void WeaponSelect()
    {
        CurrentText = TextField.text.ToLower();
        TextField.text = "";

        if (CurrentText == "back")
        {
            MainHub.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {

            bool nonvalid = true;
            for (int i = 0; i < itemText.Length; i++)
            {
                if (itemText[i] == CurrentText && ItemQuantity[i] > 0)
                {
                    GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();

                    nonvalid = false;
                    ItemType = i;
                    //MainHub.SetActive(true);
                    HealHub.SetActive(true);
                    gameObject.SetActive(false);

                }
            }

            if (nonvalid)
            {
                TextField.ActivateInputField();
                GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordFail(TextField.text.Length);

            }


        }


    }



}

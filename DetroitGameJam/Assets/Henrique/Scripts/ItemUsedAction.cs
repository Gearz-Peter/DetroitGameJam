using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUsedAction : MonoBehaviour
{
    [SerializeField] InputField TextField;

    string CurrentText;

    [SerializeField] string[] allyText;
    [SerializeField] GameObject[] allyObjects;
    [SerializeField] Text[] allyTextsUI;
    [SerializeField] GameObject ItemHub, MainHub;

    [SerializeField] ItemAction itemTypeObj;

    public int itemtype;
    [SerializeField] GameObject AllyList;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            WeaponSelect();

        }
    }


    private void OnEnable()
    {
        itemtype = itemTypeObj.ItemType;

        TextField.ActivateInputField();


        AllyHealth[] Obs;
        Obs = AllyList.GetComponentsInChildren<AllyHealth>();



        GameObject[] ActiveObjs = new GameObject[3];
        int index = 0;
        for (int i = 0; i < Obs.Length; i++)
        {
            ActiveObjs[index] = Obs[i].gameObject;
            index++;
        }
        allyObjects = ActiveObjs;


        if (itemtype != 2)
        {
            for (int i = 0; i < 3; i++)
            {
                allyTextsUI[i].text = ActiveObjs[i].GetComponentInChildren<Text>().text;

                allyText[i] = allyTextsUI[i].text.ToLower();
            }
        }
        else
        {
            allyTextsUI[0].text = "";
            allyText[0] = "";

            allyTextsUI[2].text = "";
            allyText[2] = "";

            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    allyTextsUI[1].text = "HealAll";
                    allyText[1] = "healall";
                    break;
                case 1:
                    allyTextsUI[1].text = "Call911";
                    allyText[1] = "call911";
                    break;
                case 2:
                    allyTextsUI[1].text = "AOEHeal";
                    allyText[1] = "aoeheal";
                    break;

            }




        }





    }

    void WeaponSelect()
    {
        CurrentText = TextField.text.ToLower();
        TextField.text = "";

        if (CurrentText == "back")
        {
            ItemHub.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {

            bool nonvalid = true;
            for (int i = 0; i < allyText.Length; i++)
            {
                if (allyText[i] == CurrentText)
                {
                    nonvalid = false;

                    itemTypeObj.Inventory.inventory.AddQuantity(itemtype, -1);
                    GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();
                    switch (itemtype)
                    {
                        case 0:

                            allyObjects[i].GetComponent<AllyHealth>().Heal(30);


                            break;
                        case 1:
                            allyObjects[i].GetComponent<AllyHealth>().Revive();
                            break;
                        case 2:
                            for(int a=0;a<allyObjects.Length;a++)
                            {
                                allyObjects[a].GetComponent<AllyHealth>().Heal(20);
                            }
                      
                            break;
                    }

                    gameObject.SetActive(false);
                    MainHub.SetActive(true);

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

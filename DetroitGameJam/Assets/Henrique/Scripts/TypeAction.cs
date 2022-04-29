using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypeAction : MonoBehaviour
{
    [SerializeField] InputField TextField;
   
    string CurrentText;

    [SerializeField] GameObject SwitcHub,ItemHub;
    [SerializeField] GameObject SelectEnemyHub;
    [SerializeField] GameObject[] AttackHubs;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            MenuSelect();
        }
    }


    private void OnEnable()
    {
        TextField.ActivateInputField();

    }

    void MenuSelect()
    {
        CurrentText = TextField.text.ToLower();
        TextField.text = "";

        switch (CurrentText)
        {
            case "attack":

                GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();
                AttackHubs[ SelectEnemyHub.GetComponent<SelectEnemyAction>().GetID() ].SetActive(true);
                gameObject.SetActive(false);
                break;
            case "switch":
                GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();
                SwitcHub.SetActive(true);
                gameObject.SetActive(false);
                break;
            case "item":
                GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();
                ItemHub.SetActive(true);
                gameObject.SetActive(false);
                break;
            default:
                TextField.ActivateInputField();
                GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordFail(TextField.text.Length);
                break;
        }
        
    }



}

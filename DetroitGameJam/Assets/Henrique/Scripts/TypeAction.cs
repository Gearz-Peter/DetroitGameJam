using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypeAction : MonoBehaviour
{
    [SerializeField] InputField TextField;
   
    string CurrentText;

    [SerializeField] GameObject AttackHub,SwitcHub;
    

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
                AttackHub.SetActive(true);
                gameObject.SetActive(false);
                break;
            case "switch":
                SwitcHub.SetActive(true);
                gameObject.SetActive(false);
                break;
            case "item":
                break;
            default:
                TextField.ActivateInputField();
                break;
        }
        
    }



}

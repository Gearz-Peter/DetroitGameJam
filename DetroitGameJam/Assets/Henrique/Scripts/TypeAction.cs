using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeAction : MonoBehaviour
{
    [SerializeField] InputField TextField;
    int MenuIndex=0;
    string CurrentText; 


    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CheckType();
        }
    }

    void CheckType()
    {
        CurrentText = TextField.text.ToLower();


       switch(MenuIndex)
        {
            case 0:
                MenuSelect();
                break;
            case 1:

                break;
        }


    }


    void MenuSelect()
    {

        switch(CurrentText)
        {
            case "attack":
                MenuIndex = 1;
                AttackSelect();
                break;
            case "switch":
                MenuIndex = 2;
                break;
            case "item":
                MenuIndex = 3;
                break;
        }
    }


    void AttackSelect()
    {

    }




}

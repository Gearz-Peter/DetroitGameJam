using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAction : MonoBehaviour
{
    [SerializeField] InputField TextField;

    string CurrentText;

    [SerializeField] string[] attackText;

    [SerializeField] GameObject MenuHub;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            WeaponSelect();
        }
    }


    private void OnEnable()
    {
        TextField.ActivateInputField();
    }

    void WeaponSelect()
    {
        CurrentText = TextField.text.ToLower();
        TextField.text = "";

        if (CurrentText == "back")
        {
            MenuHub.SetActive(true);
            gameObject.SetActive(false);
        }
       else
        {
            bool nonvalid=true;
            for(int i=0;i<attackText.Length;i++)
            {
                if(attackText[i] == CurrentText)
                {
                    nonvalid = false;

                }
            }

            if(nonvalid)
            {
                TextField.ActivateInputField();

            }


        }
       
        
    }

}

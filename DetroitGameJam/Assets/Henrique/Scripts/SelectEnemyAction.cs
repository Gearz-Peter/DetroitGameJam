using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectEnemyAction : MonoBehaviour
{
    [SerializeField] InputField TextField;

    string CurrentText;

    [SerializeField] string[] enemyText;
    [SerializeField] GameObject[] enemyObjects;

    [SerializeField] GameObject AttackHub,MainHub;

    public GameObject SelectedCharacter;

    [SerializeField] AllyAttackStat SelectedCharacterStats;
    
    Vector2 InitialPos;

    [SerializeField] AttackingVisual attackvisual;

    public int AttackType;

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
         InitialPos = SelectedCharacter.transform.position;
    }

    void WeaponSelect()
    {
        CurrentText = TextField.text.ToLower();
        TextField.text = "";

        if (CurrentText == "back")
        {
            AttackHub.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
           
            bool nonvalid = true;
            for (int i = 0; i < enemyText.Length; i++)
            {
                if (enemyText[i] == CurrentText)
                {
                    nonvalid = false;
                    SelectedCharacterStats = SelectedCharacter.GetComponent<AllyAttackStat>();


                    switch(AttackType)
                    {
                        case 0:
                            attackvisual.AttackSingle(SelectedCharacter, enemyObjects[i], InitialPos, SelectedCharacterStats);
                            break;
                        case 1:
                            attackvisual.AttackSpecial(SelectedCharacter, enemyObjects[i], InitialPos, SelectedCharacterStats);
                            break;
                        case 2:
                            break;
                    }
                   
                    gameObject.SetActive(false);
                    MainHub.SetActive(true);
                 
                }
            }

            if (nonvalid)
            {
                TextField.ActivateInputField();

            }


        }


    }


   



}

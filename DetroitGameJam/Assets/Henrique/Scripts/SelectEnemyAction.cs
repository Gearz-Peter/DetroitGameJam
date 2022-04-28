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
    [SerializeField] Text[] enemyTextsUI;
    [SerializeField] GameObject AttackHub,MainHub;

    public GameObject SelectedCharacter;

    [SerializeField] AllyAttackStat SelectedCharacterStats;
    
    Vector2 InitialPos;

    [SerializeField] AttackingVisual attackvisual;
    [SerializeField] AttackAction AttackActionHud;

    public int AttackType;


    [SerializeField] GameObject EnemyList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            WeaponSelect();
            
        }
    }


    private void OnEnable()
    {
        AttackType = AttackActionHud.AttackSelected;

        TextField.ActivateInputField();
         InitialPos = SelectedCharacter.transform.position;

        EnemyHealth[] Obs;
        Obs = EnemyList.GetComponentsInChildren<EnemyHealth>();



        GameObject[] ActiveObjs = new GameObject[3];
        int index = 0;
        for (int i = 0; i < Obs.Length; i++)
        {
            ActiveObjs[index] = Obs[i].gameObject;
            index++;
        }
        enemyObjects = ActiveObjs;

      
        if (AttackType != 2)
        {
            for (int i = 0; i < 3; i++)
            {
                enemyTextsUI[i].text = ActiveObjs[i].GetComponentInChildren<Text>().text;

                enemyText[i] = enemyTextsUI[i].text.ToLower();
            }
        }
        else
        {
            enemyTextsUI[0].text = "";
            enemyText[0] = "";
            
            enemyTextsUI[2].text = "";
            enemyText[2] = "";

            int random = Random.Range(0, 3);
           switch(random)
            {
                case 0:
                    enemyTextsUI[1].text = "Everyone";
                    enemyText[1] = "everyone";
                    break;
                case 1:
                    enemyTextsUI[1].text = "Everything";
                    enemyText[1] = "everything";
                    break;
                case 2:
                    enemyTextsUI[1].text = "all";
                    enemyText[1] = "all";
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
            AttackHub.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
           
            bool nonvalid = true;
            for (int i = 0; i < enemyText.Length; i++)
            {
                if (enemyText[i] == CurrentText && SelectedCharacter.GetComponent<AllyHealth>().Health > 0)
                {
                    nonvalid = false;
                    SelectedCharacterStats = SelectedCharacter.GetComponent<AllyAttackStat>();

                    GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();

                    switch (AttackType)
                    {
                        case 0:

                            attackvisual.AttackSingle(SelectedCharacter, enemyObjects[i], InitialPos, SelectedCharacterStats);
                            break;
                        case 1:
                            attackvisual.AttackSpecial(SelectedCharacter, enemyObjects[i], InitialPos, SelectedCharacterStats);
                            break;
                        case 2:

                            attackvisual.AttackAOE(SelectedCharacter, enemyObjects, InitialPos, SelectedCharacterStats);

                            break;
                    }
                   
                    gameObject.SetActive(false);
                    MainHub.SetActive(true);
                 
                }
            }

            if (nonvalid)
            {
                GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordFail(TextField.text.Length);

                TextField.ActivateInputField();

            }


        }


    }


   



}

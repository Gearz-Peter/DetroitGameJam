using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchAction : MonoBehaviour
{
    [SerializeField] InputField TextField;

    string CurrentText;

    [SerializeField] Text[] AllyTextsUI;
    [SerializeField] string[] allyText;
    [SerializeField] GameObject[] allyObjects;

    [SerializeField] GameObject MainHub;
    [SerializeField] SelectEnemyAction selectEnemyhub;

    [SerializeField] GameObject AllieList;

    [SerializeField] GameObject ArrowObj;
    

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

        AllyHealth[] Obs;
        Obs = AllieList.GetComponentsInChildren<AllyHealth>();
        


        GameObject[] ActiveObjs = new GameObject[3];
        int index = 0;
        for(int i=0;i<Obs.Length;i++)
        {
            ActiveObjs[index] = Obs[i].gameObject;
            index++;
        }
        allyObjects = ActiveObjs;


        for(int i =0;i<3;i++)
        {
            AllyTextsUI[i].text = ActiveObjs[i].GetComponentInChildren<Text>().text;

            allyText[i] = AllyTextsUI[i].text.ToLower();
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
            for (int i = 0; i < allyText.Length; i++)
            {
                if (allyText[i] == CurrentText && allyObjects[i].GetComponent<AllyHealth>().Health > 0 )
                {
                    nonvalid = false;
                    GameObject.Find("WPMText").GetComponent<WordsPerMinute>().WordPassed();

                    selectEnemyhub.SelectedCharacter = allyObjects[i];
                    ArrowObj.transform.position = allyObjects[i].transform.position;
                    ArrowObj.transform.Translate(0, 380, 0);

                    MainHub.SetActive(true);
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

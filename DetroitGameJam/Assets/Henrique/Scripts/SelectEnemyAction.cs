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

    [SerializeField] GameObject AttackHub;

    public GameObject SelectedCharacter;

    Vector2 InitialPos;
    IEnumerator AttackCoroutine;
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
                    if(AttackCoroutine != null)
                    {
                        StopCoroutine(AttackCoroutine);
                    }
                  
                    AttackCoroutine = AttackingNumerator(i);
                    
                    StartCoroutine(AttackCoroutine);
                }
            }

            if (nonvalid)
            {
                TextField.ActivateInputField();

            }


        }


    }


    IEnumerator AttackingNumerator(int enemyindex)
    {
        


        while(Vector2.Distance(SelectedCharacter.transform.position, enemyObjects[enemyindex].transform.position) > 250  )
        {
           
            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, enemyObjects[enemyindex].transform.position, 7 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        while(SelectedCharacter.transform.eulerAngles.z < 45)
        {
           
            SelectedCharacter.transform.Rotate(0, 0, 5);
            yield return null;
        }
        yield return new WaitForSeconds(.15f);

        for(int i=0; i< 10;i++ )
        {
            SelectedCharacter.transform.Rotate(0, 0, -18);
            yield return null;
        }
        yield return new WaitForSeconds(.15f);

        while (Vector2.Distance(SelectedCharacter.transform.position, InitialPos) > 50 || SelectedCharacter.transform.eulerAngles.z > 15)
        {
            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, InitialPos, 15 * Time.deltaTime);


            if(SelectedCharacter.transform.eulerAngles.z > 15)
            {
                SelectedCharacter.transform.Rotate(0, 0, 8);
            }
            
            yield return null;
        }
        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);



    }



}

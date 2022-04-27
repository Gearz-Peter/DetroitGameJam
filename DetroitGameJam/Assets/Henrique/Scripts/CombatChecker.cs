using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatChecker : MonoBehaviour
{
    [SerializeField] GameObject AllyList;
    [SerializeField] GameObject[] allyObjects, enemyObjects;
    [SerializeField] GameObject EnemyList;
    [SerializeField] TransitionAnim TransitionOBJ;
    


    private void OnEnable()
    {
        StartCoroutine(WaitingBattleNumerator());
    }

    IEnumerator WaitingBattleNumerator()
    {
        yield return new WaitForSeconds(.5f);

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



        EnemyHealth[] Obs2;
        Obs2 = EnemyList.GetComponentsInChildren<EnemyHealth>();



        GameObject[] ActiveObjss = new GameObject[3];
        int indexx = 0;
        for (int i = 0; i < Obs2.Length; i++)
        {
            ActiveObjss[indexx] = Obs2[i].gameObject;
            indexx++;
        }
        enemyObjects = ActiveObjss;


        bool PlayerWon=false,EnemyWon=false;


        while(!PlayerWon && !EnemyWon )
        {

            yield return null;

            bool allRatsDied=true;
            for(int a=0;a<enemyObjects.Length;a++)
            {
                if(enemyObjects[a].GetComponent<EnemyHealth>().Health > 0 )
                {
                    allRatsDied = false;
                }
            }

            if(allRatsDied)
            {
                PlayerWon = true;
            }



            bool allPlayersDied = true;
            for (int a = 0; a < allyObjects.Length; a++)
            {
                if (allyObjects[a].GetComponent<AllyHealth>().Health > 0)
                {
                    allPlayersDied = false;
                }
            }

            if (allPlayersDied)
            {
                EnemyWon = true;
            }






        }


        if(PlayerWon)
        {
            TransitionOBJ.TransitionONFunc();
        }
        else
        {
            Debug.Log("You Lose");
        }



    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetter : MonoBehaviour
{

    [SerializeField] GameObject[] EnemiesPos1, EnemiesPos2, EnemiesPos3;
   public void GetInfo(int[] list )
    {
        for(int i=0;i<EnemiesPos1.Length;i++)
        {
            EnemiesPos1[i].SetActive(false);
        }
        for (int i = 0; i < EnemiesPos2.Length; i++)
        {
            EnemiesPos2[i].SetActive(false);
        }
        for (int i = 0; i < EnemiesPos3.Length; i++)
        {
            EnemiesPos1[3].SetActive(false);
        }


        EnemiesPos1[list[0]].SetActive(true);
        EnemiesPos2[list[1]].SetActive(true);
        EnemiesPos3[list[2]].SetActive(true);

    }
}

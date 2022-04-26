using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TransitionAnim : MonoBehaviour
{

   [SerializeField] RectTransform[] Images;
   public bool FullyOn;

    

    public void TransitionONFunc()
    {
        FullyOn = false;
        StartCoroutine(TransitionON());
    }


    public void TransitionOFFFunc()
    {
        for (int i = 0; i < Images.Length; i++)
        {
          
                Images[i].localScale = new Vector3(0,0,0);
                
         
           
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TransitionONFunc();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            TransitionOFFFunc();
        }
    }


   
  IEnumerator TransitionON()
    {

        yield return new WaitForSeconds(.3f);
        int SpeedAmout=70;

        for (int i =0;i<Images.Length;i++)
        {
            while (Images[i].localScale.x < 2)
            {
                Images[i].localScale = new Vector3(Images[i].localScale.x +  SpeedAmout * Time.deltaTime, Images[i].localScale.y + SpeedAmout * Time.deltaTime, Images[i].localScale.z);
                yield return null;
            }
            SpeedAmout += 1;
        }

        yield return new WaitForSeconds(.3f);
        FullyOn = true;


    }
}

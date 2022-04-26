using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image encounterImage;
    [SerializeField] TransitionAnim Transition;
    [SerializeField] GameObject BattleCanvas;
    void start()
    {
        encounterImage.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            DoEncounter();
        }
    }

    void DoEncounter()
    {
      
        GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
        Transition.TransitionONFunc();
        StartCoroutine(WaitingTransition());
    }

   IEnumerator WaitingTransition()
    {
        yield return new WaitForSeconds(.3f);
       while(!Transition.FullyOn)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        BattleCanvas.SetActive(true);
        Transition.TransitionOFFFunc();
    }
}

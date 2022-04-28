using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] TransitionAnim Transition;
    [SerializeField] GameObject BattleCanvas;

    public int ratsBeat = 0;

    void start()
    {
        canvas = GameObject.FindWithTag("OverworldCanvas").GetComponent<Canvas>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            DoEncounter(collider);
        }
    }

    void DoEncounter(Collider2D collider)
    {
        GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
        collider.GetComponent<RatPatrol>().speed = 0;
        Transition.TransitionONFunc();
        StartCoroutine(WaitingTransition(collider));
    }

   IEnumerator WaitingTransition(Collider2D collider)
    {
        yield return new WaitForSeconds(.3f);
       while(!Transition.FullyOn)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        BattleCanvas.SetActive(true);

        BattleCanvas.GetComponent<EnemySetter>().GetInfo(collider.GetComponent<EncounterInfo>().EnemyIds;);

        Transition.TransitionOFFFunc();


        while (Transition.FullyOn)
        {
            yield return null;
        }

        yield return new WaitForSeconds(.3f);
        while (!Transition.FullyOn)
        {
            yield return null;
        }
        BattleCanvas.SetActive(false);
        Transition.TransitionOFFFunc();
        GameObject.FindWithTag("Player").GetComponent<EnableDisable>().flip = true;
        Destroy(collider.gameObject);
        ratsBeat++;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image encounterImage;

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
        Debug.Log("Encounter");
        canvas.GetComponent<TextManager>().DisplayText("Some Enemies Have Appeared!","");
        encounterImage.enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().isMovementEnabled = false;
        StartCoroutine(EncounterCoroutine());
    }

    IEnumerator EncounterCoroutine()
    {
        yield return new WaitForSeconds(3f);
        encounterImage.enabled = false;
        canvas.GetComponent<TextManager>().RemoveText();
    }
}

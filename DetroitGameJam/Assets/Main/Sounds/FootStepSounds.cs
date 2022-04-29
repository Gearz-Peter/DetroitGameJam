using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSounds : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip[] footstepsounds;
    [SerializeField] float minTime, maxTime;
    float countdown;
    [SerializeField] PlayerMove playermovement;
    [SerializeField] GameObject BattleCanvas;
    private void Start()
    {
        countdown = minTime;
    }
    void Update()
    {
        if(playermovement.isMoving  && !BattleCanvas.activeSelf)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0)
            {
                countdown = Random.Range(minTime, maxTime);

                audiosource.clip = footstepsounds[Random.Range(0,footstepsounds.Length)];
                audiosource.Play();


            }

        }

    }
}

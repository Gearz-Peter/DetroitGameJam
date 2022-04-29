using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    [SerializeField] GameObject BattleCanvas;
    [SerializeField] AudioClip OtherWorldMusic, FightMusic;
    [SerializeField] AudioSource audioPlayer;
     private void Update()
    {
        if(BattleCanvas.activeSelf)
        {
           if(audioPlayer.clip != FightMusic)
            {
                audioPlayer.clip = FightMusic;
                audioPlayer.Play();
            }
        }
        else
        {
            if (audioPlayer.clip != OtherWorldMusic)
            {
                audioPlayer.clip = OtherWorldMusic;
                audioPlayer.Play();
            }
        }
    }
}

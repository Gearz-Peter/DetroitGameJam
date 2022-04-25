using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : MonoBehaviour
{
    [SerializeField] public string NPCName;
    [SerializeField] private int ID;
    [SerializeField] public int DialogueState = 0;
    [SerializeField] public string[] Dialogue;
}

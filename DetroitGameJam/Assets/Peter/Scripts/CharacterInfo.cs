using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private string CharacterName;
    [SerializeField] private int ID;
    [SerializeField] private float maxHealth;
    [SerializeField] private string[] attacks = new string[3];
}

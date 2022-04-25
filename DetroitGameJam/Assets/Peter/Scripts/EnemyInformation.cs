using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInformation : MonoBehaviour
{
    [SerializeField] private string enemyType;
    [SerializeField] private int ID;
    [SerializeField] private float maxHealth;
    [SerializeField] private string[] attacks;
}

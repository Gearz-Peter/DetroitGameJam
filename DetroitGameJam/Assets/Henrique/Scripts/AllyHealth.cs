using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AllyHealth : MonoBehaviour
{
   public int Health;
    [SerializeField] int MaxHealth;

    [SerializeField] Slider HealthSlider;

    private void Start()
    {
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;

    }
    public void DealDamage(int amout)
    {
        Health -= amout;
        HealthSlider.value = Health;

    }



}

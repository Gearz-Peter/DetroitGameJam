using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AllyHealth : MonoBehaviour
{
   public int Health;
    [SerializeField] int MaxHealth;

    [SerializeField] Slider HealthSlider;

 
    private void OnEnable()
    {
        Health = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;
    }
    public void DealDamage(int amout)
    {
        Health -= amout;
        HealthSlider.value = Health;

    }

    public void Revive()
    {
        if(Health <= 0)
        {
            Health = MaxHealth;
            HealthSlider.value = Health;
        }
    }

    public void Heal(int amout)
    {
        if(Health > 0)
        {
            Health += amout;
            HealthSlider.value = Health;
            if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
      

    }

}

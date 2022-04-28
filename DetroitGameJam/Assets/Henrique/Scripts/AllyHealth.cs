using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AllyHealth : MonoBehaviour
{
   public int Health;
    [SerializeField] int MaxHealth;

    [SerializeField] Slider HealthSlider;
    [SerializeField] Image spriteI;
    Vector3 ImageInitialPosition;
    [SerializeField] GameObject[] HurtSounds;
    [SerializeField] GameObject DeathSound;
    IEnumerator ShakeNumerator()
    {
        float ShakeAmout = 4;
        for (int i = 0; i < 15; i++)
        {


            yield return new WaitForSeconds(0.02f);
            spriteI.transform.localPosition = new Vector3(spriteI.transform.localPosition.x + Random.Range(-1, 1) * ShakeAmout, spriteI.transform.localPosition.y + Random.Range(-1, 1) * ShakeAmout, spriteI.transform.localPosition.z);
            yield return new WaitForSeconds(0.02f);
            spriteI.transform.localPosition = ImageInitialPosition;
        }
    }

    IEnumerator FlashRedNumerator()
    {

        spriteI.color = new Color(1, 0, 0, spriteI.color.a);
        yield return new WaitForSeconds(.3f);
        spriteI.color = new Color(1, 1, 1, spriteI.color.a);
    }


    private void OnEnable()
    {
        Health = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;


        StopAllCoroutines();

        ImageInitialPosition = spriteI.transform.localPosition;

        Health = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;


        spriteI.GetComponent<Animator>().enabled = true;

        spriteI.color = new Color(1, 1, 1, 1);

    }
    public void DealDamage(int amout)
    {
        if (Health > 0)
        {


            Health -= amout;
            HealthSlider.value = Health;


            StartCoroutine(FlashRedNumerator());
            StartCoroutine(ShakeNumerator());

            Instantiate(HurtSounds[Random.Range(0, HurtSounds.Length)], transform.position, Quaternion.identity);
        }

        if (Health <= 0)
        {
            spriteI.GetComponent<Animator>().enabled = false;

            spriteI.color = new Color(spriteI.color.r, spriteI.color.g, spriteI.color.b, .5f);

            Instantiate(DeathSound, transform.position, Quaternion.identity);

        }

    }

    public void Revive()
    {
        if(Health <= 0)
        {
            spriteI.GetComponent<Animator>().enabled = true;

            spriteI.color = new Color(1, 1, 1, 1);

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

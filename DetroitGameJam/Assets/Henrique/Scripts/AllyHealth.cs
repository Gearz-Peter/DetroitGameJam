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
    [SerializeField] GameObject HealCrossPrefab;
    [SerializeField] GameObject HealNumberPrefab;
    [SerializeField] GameObject BattleCanvas;
    [SerializeField] GameObject PlusAttackPrefab;
    [SerializeField] GameObject UpArrowsPrefab;

      int TemporaryAttackGains;
   
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
        TemporaryAttackGains = 0;
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
        if(amout <= 0)
        {
            return;
        }

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

            Heal(Health);
        }
    }

    public void IncreaseAttackTemporary(int amout)
    {
        if(Health > 0)
        {
            GameObject DmgNumber = Instantiate(PlusAttackPrefab, gameObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

            DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
            DmgNumber.GetComponent<Text>().text = "+" + amout;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(UpArrowsPrefab, new Vector3(transform.position.x + Random.Range(-200, 200), transform.position.y + Random.Range(50, 100), transform.position.z), Quaternion.identity, BattleCanvas.transform);
            }

            GetComponent<AllyAttackStat>().BasicDamage += amout;
            GetComponent<AllyAttackStat>().SpecialDamage += amout;
            GetComponent<AllyAttackStat>().AOEDamage += amout;
            TemporaryAttackGains += amout;
        }
       

    }




    private void OnDisable()
    {

        GetComponent<AllyAttackStat>().BasicDamage -= TemporaryAttackGains;
        GetComponent<AllyAttackStat>().SpecialDamage -= TemporaryAttackGains;
        GetComponent<AllyAttackStat>().AOEDamage -= TemporaryAttackGains;
    }

    public void Heal(int amout)
    {
        if(Health > 0)
        {
            GameObject DmgNumber = Instantiate(HealNumberPrefab, gameObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

            DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
            DmgNumber.GetComponent<Text>().text = "+" + amout;

            for (int i = 0; i < 3;i++)
            {
                Instantiate(HealCrossPrefab, new Vector3(transform.position.x + Random.Range(-200, 200), transform.position.y + Random.Range(50, 100), transform.position.z), Quaternion.identity, BattleCanvas.transform);
            }



            Health += amout;
            HealthSlider.value = Health;
            if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
      

    }

}

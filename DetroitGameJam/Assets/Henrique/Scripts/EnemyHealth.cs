using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
   public int Health;
    public int MaxHealth;
    
    [SerializeField] Slider HealthSlider;
    [SerializeField] GameObject DebuffPanel;
    [SerializeField] GameObject BleedIcon,CovidIcon;
    [SerializeField] GameObject DamageNumberPrefab;
    [SerializeField] GameObject BattleCanvas;

    RatAttackStats Stats;
    [SerializeField] Image spriteI;
    Vector3 ImageInitialPosition;
    [SerializeField] GameObject[] HurtSounds;
    [SerializeField] GameObject DeathSound;


    [SerializeField] GameObject HealCrossPrefab;
    [SerializeField] GameObject HealNumberPrefab;

    private void Start()
    {
        Stats = GetComponent<RatAttackStats>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();

        ImageInitialPosition = spriteI.transform.localPosition;

        Health = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;


        spriteI.GetComponent<Animator>().enabled = true;

        spriteI.color = new Color(1,1,1, 1);
    }

    public void Heal(int amout)
    {
        if (Health > 0)
        {
            GameObject DmgNumber = Instantiate(HealNumberPrefab, gameObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

            DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
            DmgNumber.GetComponent<Text>().text = "+" + amout;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(HealCrossPrefab, new Vector3(transform.position.x + Random.Range(-200, 200), transform.position.y + Random.Range(50, 100), transform.position.z), Quaternion.identity, BattleCanvas.transform);
            }



            Health += amout;
            HealthSlider.value = Health;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }


    }


    public void DealDamage(int amout)
    {
        if(amout <= 0)
        {
            return;
        }

        if(Health > 0)
        {

      
        Health -= amout;
        HealthSlider.value = Health;


        GameObject DmgNumber = Instantiate(DamageNumberPrefab, gameObject.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + amout;


        StartCoroutine(FlashRedNumerator());
        StartCoroutine(ShakeNumerator());

        Instantiate(HurtSounds[Random.Range(0,HurtSounds.Length)], transform.position, Quaternion.identity);
        }

        if (Health <= 0)
        {
            spriteI.GetComponent<Animator>().enabled = false;

            spriteI.color = new Color(spriteI.color.r, spriteI.color.g, spriteI.color.b, .5f);

            Instantiate(DeathSound, transform.position, Quaternion.identity);

        }
    }

    IEnumerator ShakeNumerator()
    {
        float ShakeAmout=4;
        for(int i=0;i<15;i++)
        {
           

            yield return new WaitForSeconds(0.02f);
            spriteI.transform.localPosition = new Vector3(spriteI.transform.localPosition.x + Random.Range(-1,1) * ShakeAmout, spriteI.transform.localPosition.y + Random.Range(-1, 1) * ShakeAmout, spriteI.transform.localPosition.z);
            yield return new WaitForSeconds(0.02f);
            spriteI.transform.localPosition = ImageInitialPosition;
        }
    }

    IEnumerator FlashRedNumerator()
    {
        Color oldColor = spriteI.color;
        spriteI.color = new Color(1, 0, 0, spriteI.color.a);
        yield return new WaitForSeconds(.3f);
        spriteI.color = oldColor;
    }


    public void ApplyBleed()
    {

        StartCoroutine(BleedNumerator());
    }
    IEnumerator BleedNumerator()
    {
        GameObject obj = Instantiate(BleedIcon, transform.position, Quaternion.identity, DebuffPanel.transform);
        Destroy(obj,8);
        for (int i=0;i<8;i++)
        {
            yield return new WaitForSeconds(1);
            DealDamage(1);

        }
        
    }


    public void ApplyCovid()
    {

        StartCoroutine(CovidNumerator());
    }
    IEnumerator CovidNumerator()
    {
        GameObject obj = Instantiate(CovidIcon, transform.position, Quaternion.identity, DebuffPanel.transform);
        Destroy(obj, 13);

        Stats.BasicDamage -= 3;
        spriteI.color = new Color(0, 1, 0, spriteI.color.a);
        yield return new WaitForSeconds(13);
        spriteI.color = new Color(1, 1, 1, spriteI.color.a);
        Stats.BasicDamage += 3;




    }


}

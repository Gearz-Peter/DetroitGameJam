using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
   public int Health;
    [SerializeField] int MaxHealth;
    
    [SerializeField] Slider HealthSlider;
    [SerializeField] GameObject DebuffPanel;
    [SerializeField] GameObject BleedIcon;
    [SerializeField] GameObject DamageNumberPrefab;
    [SerializeField] GameObject BattleCanvas;

    [SerializeField] Image spriteI;
    Vector3 ImageInitialPosition;
    private void OnEnable()
    {
      
        ImageInitialPosition = spriteI.transform.localPosition;

        Health = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;


        spriteI.GetComponent<Animator>().enabled = true;

        spriteI.color = new Color(1,1,1, 1);
    }

    public void DealDamage(int amout)
    {
        Health -= amout;
        HealthSlider.value = Health;


        GameObject DmgNumber = Instantiate(DamageNumberPrefab, gameObject.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + amout;


        StartCoroutine(FlashRedNumerator());
        StartCoroutine(ShakeNumerator());


        if(Health <= 0)
        {
            spriteI.GetComponent<Animator>().enabled = false;
            
            spriteI.color = new Color(spriteI.color.r, spriteI.color.g, spriteI.color.b, .5f);
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
       
        spriteI.color = new Color(1, 0, 0, spriteI.color.a);
        yield return new WaitForSeconds(.3f);
        spriteI.color = new Color(1, 1, 1, spriteI.color.a);
    }


    public void ApplyBleed()
    {

        StartCoroutine(BleedNumerator());
    }
    IEnumerator BleedNumerator()
    {
        GameObject obj = Instantiate(BleedIcon, transform.position, Quaternion.identity, DebuffPanel.transform);

        for(int i=0;i<10;i++)
        {
            yield return new WaitForSeconds(1);
            DealDamage(2);

        }
        Destroy(obj);
    }


}

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
    private void Start()
    {
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = Health;

    }
    public void DealDamage(int amout)
    {
        Health -= amout;
        HealthSlider.value = Health;


        GameObject DmgNumber = Instantiate(DamageNumberPrefab, gameObject.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + amout;

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

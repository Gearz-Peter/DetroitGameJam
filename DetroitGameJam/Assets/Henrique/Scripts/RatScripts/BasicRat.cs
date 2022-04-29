using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicRat : MonoBehaviour
{
    [SerializeField] GameObject AllieList;
    [SerializeField] GameObject[] AllyObjs;

    Vector3 BeforeInitialPos;
    GameObject SelectedCharacterBefore;

    IEnumerator AttackCoroutine;




    [SerializeField] GameObject DamageNumberPrefab;
    [SerializeField] GameObject BattleCanvas;


    [SerializeField] EnemyHealth EnemyHealth;
    [SerializeField] RatAttackStats ratStats;
    [SerializeField] GameObject EnemyObject;
        float TimeToAttack;

    Vector3 InitialPosition;


    private void OnEnable()
    {
        
        InitialPosition = transform.position;
        Debug.Log(InitialPosition.z);
        TimeToAttack = Random.Range(4, 10);
    }

    private void Update()
    {
       if(TimeToAttack <0 && EnemyHealth.Health > 0)
        {
            TimeToAttack = Random.Range(4, 8);
            PickFight();
        }
       else
        {
            TimeToAttack -= Time.deltaTime;
        }
        

    }

   void PickFight()
    {

        AllyHealth[] Obs;
        Obs = AllieList.GetComponentsInChildren<AllyHealth>();



        GameObject[] ActiveObjs = new GameObject[3];
        int index = 0;
        for (int i = 0; i < Obs.Length; i++)
        {
            ActiveObjs[index] = Obs[i].gameObject;
            index++;
        }
        AllyObjs = ActiveObjs;

        bool found = false;
        int searchtimeout = 50;
       while(!found && searchtimeout > 0)
        {
            int randomsearch = Random.Range(0, 3);

            if(AllyObjs[randomsearch].GetComponent<AllyHealth>().Health > 0 )
            {
                found = true;
                AttackSingle(gameObject, AllyObjs[randomsearch], InitialPosition, ratStats);

            }
            searchtimeout--;

        }



    }





    public void AttackSingle(GameObject SelectedCharacter, GameObject enemyObjects, Vector3 InitialPos, RatAttackStats stats)
    {
        if (AttackCoroutine != null)
        {
            SelectedCharacterBefore.transform.position = BeforeInitialPos;
            StopCoroutine(AttackCoroutine);
        }
        SelectedCharacterBefore = SelectedCharacter;
        BeforeInitialPos = InitialPos;
        AttackCoroutine = AttackingNumerator(SelectedCharacter, enemyObjects, InitialPos, stats);

        StartCoroutine(AttackCoroutine);

    }
    IEnumerator AttackingNumerator(GameObject SelectedCharacter, GameObject enemyObject, Vector3 InitialPos, RatAttackStats stats)
    {



        while (Vector2.Distance(SelectedCharacter.transform.position, enemyObject.transform.position) > 250)
        {

            SelectedCharacter.transform.position = Vector3.Lerp(SelectedCharacter.transform.position, enemyObject.transform.position, 7 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        float timeToHit = 0.1f;
        while(timeToHit > 0.0f)
        {
            timeToHit -= Time.deltaTime;
            SelectedCharacter.transform.Translate(-1, 0, 0);
            yield return null;
           
        }
     /*   for (int i = 0; i < 40; i++)
        {
            SelectedCharacter.transform.Translate(-1, 0, 0);
             yield return null;
        }*/
        yield return new WaitForSeconds(.15f);
        /*
        for (int i = 0; i < 40; i++)
        {
            SelectedCharacter.transform.Translate(1, 0, 0);
            yield return null;
        }
        */

        timeToHit = 0.1f;
        while (timeToHit > 0.0f)
        {
            timeToHit -= Time.deltaTime;
            SelectedCharacter.transform.Translate(1, 0, 0);
            yield return null;

        }
        GameObject DmgNumber = Instantiate(DamageNumberPrefab, SelectedCharacter.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + stats.BasicDamage.ToString();

        enemyObject.GetComponent<AllyHealth>().DealDamage(stats.BasicDamage);

        yield return new WaitForSeconds(.15f);




        while (Vector2.Distance(SelectedCharacter.transform.position, InitialPos) > 50 || SelectedCharacter.transform.eulerAngles.z > 15)
        {
            SelectedCharacter.transform.position = Vector3.Lerp(SelectedCharacter.transform.position, InitialPos, 15 * Time.deltaTime);


            if (SelectedCharacter.transform.eulerAngles.z > 15)
            {
                SelectedCharacter.transform.Rotate(0, 0, 8);
            }

            yield return null;
        }
        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
        SelectedCharacter.transform.position = InitialPos;

        Debug.Log(InitialPosition.z);
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SewerRat : MonoBehaviour
{
    [SerializeField] GameObject AllieList;
    [SerializeField] GameObject[] AllyObjs;

    Vector3 BeforeInitialPos;
    GameObject SelectedCharacterBefore;

    IEnumerator AttackCoroutine, SpecialAttackCoroutine;




    [SerializeField] GameObject DamageNumberPrefab;
    [SerializeField] GameObject BattleCanvas;


    [SerializeField] EnemyHealth EnemyHealth;
    [SerializeField] RatAttackStats ratStats;
    [SerializeField] GameObject EnemyObject;
    float TimeToAttack;

    Vector3 InitialPosition;


    [SerializeField] float MinTime=7, MaxTime=10;
    [SerializeField] int MaxRange;

    private void OnEnable()
    {
        MaxRange = 3;
        MinTime = 7;
        MaxTime = 10;

        InitialPosition = transform.position;

        TimeToAttack = Random.Range(MinTime, MaxTime);
    }

    private void Update()
    {
        if (TimeToAttack < 0 && EnemyHealth.Health > 0)
        {
            TimeToAttack = Random.Range(MinTime,MaxTime);
            PickFight();
        }
        else
        {
            TimeToAttack -= Time.deltaTime;
        }


        if(EnemyHealth.Health < EnemyHealth.MaxHealth / 2)
        {
            TimeToAttack -=  2 * Time.deltaTime;
            MaxRange = 1;
        }
        else
        {
            MaxRange = 3;
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
        while (!found && searchtimeout > 0)
        {
            int randomsearch = Random.Range(0, 3);

            if (AllyObjs[randomsearch].GetComponent<AllyHealth>().Health > 0)
            {
                found = true;
                int RandomAttack = Random.Range(0, MaxRange);
                if (RandomAttack == 0)
                {
                    SpecialAttack(gameObject, AllyObjs[randomsearch], InitialPosition, ratStats);

                }
                else
                {

                    AttackSingle(gameObject, AllyObjs[randomsearch], InitialPosition, ratStats);
                }


            }
            searchtimeout--;

        }



    }

    public void SpecialAttack(GameObject SelectedCharacter, GameObject enemyObjects, Vector3 InitialPos, RatAttackStats stats)
    {
        if (SpecialAttackCoroutine != null)
        {
            SelectedCharacterBefore.transform.position = BeforeInitialPos;
            StopCoroutine(SpecialAttackCoroutine);
        }
        SelectedCharacterBefore = SelectedCharacter;
        BeforeInitialPos = InitialPos;
        SpecialAttackCoroutine = SpecialAttackingNumerator(SelectedCharacter, enemyObjects, InitialPos, stats);

        StartCoroutine(SpecialAttackCoroutine);

    }

    IEnumerator SpecialAttackingNumerator(GameObject SelectedCharacter, GameObject enemyObject, Vector3 InitialPos, RatAttackStats stats)
    {


      for(int i =0;i<50;i++)
        {
            SelectedCharacter.transform.position = Vector3.Lerp(SelectedCharacter.transform.position, new Vector3(SelectedCharacter.transform.position.x, -999, SelectedCharacter.transform.position.z), 10 * Time.deltaTime);
            yield return null;
        }

        SelectedCharacter.transform.position = new Vector3(enemyObject.transform.position.x - 5, SelectedCharacter.transform.position.y, SelectedCharacter.transform.position.z) ;

        yield return new WaitForSeconds(.1f);

        while (Vector2.Distance(SelectedCharacter.transform.position, new Vector3(SelectedCharacter.transform.position.x, enemyObject.transform.position.y,SelectedCharacter.transform.position.z )) > 20 )
        {
            Debug.Log(Vector2.Distance(SelectedCharacter.transform.position, new Vector3(SelectedCharacter.transform.position.x, enemyObject.transform.position.y, SelectedCharacter.transform.position.z)));
            SelectedCharacter.transform.position = Vector3.Lerp(SelectedCharacter.transform.position, new Vector3(SelectedCharacter.transform.position.x, enemyObject.transform.position.y, SelectedCharacter.transform.position.z), 10 * Time.deltaTime);
            yield return null;
        }
      

        GameObject DmgNumber = Instantiate(DamageNumberPrefab, SelectedCharacter.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + stats.SpecialDamage.ToString();

        enemyObject.GetComponent<AllyHealth>().DealDamage(stats.SpecialDamage);

        yield return new WaitForSeconds(.15f);

        for (int i = 0; i < 100; i++)
        {
            SelectedCharacter.transform.position = Vector3.Lerp(SelectedCharacter.transform.position, new Vector3(SelectedCharacter.transform.position.x, -999, SelectedCharacter.transform.position.z), 8 * Time.deltaTime);
            yield return null;
        }

        SelectedCharacter.transform.position = new Vector3(InitialPos.x, SelectedCharacter.transform.position.y, InitialPos.z);

        while (Vector2.Distance(SelectedCharacter.transform.position, InitialPos) > 50 )
        {
            SelectedCharacter.transform.position = Vector3.Lerp(SelectedCharacter.transform.position, InitialPos, 15 * Time.deltaTime);


            yield return null;
        }
        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
        SelectedCharacter.transform.position = InitialPos;


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

        for (int i = 0; i < 40; i++)
        {
            SelectedCharacter.transform.Translate(-1, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(.15f);

        for (int i = 0; i < 40; i++)
        {
            SelectedCharacter.transform.Translate(1, 0, 0);
            yield return null;
        }

        GameObject DmgNumber = Instantiate(DamageNumberPrefab, SelectedCharacter.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + stats.BasicDamage.ToString();

        enemyObject.GetComponent<AllyHealth>().DealDamage(stats.BasicDamage);

        yield return new WaitForSeconds(.4f);




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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SusRat : MonoBehaviour
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

    [SerializeField] GameObject RatBusterPrefab;
    
    bool lastBreath=true;


    private void OnEnable()
    {
        lastBreath = true;
        InitialPosition = transform.position;

        TimeToAttack = Random.Range(6, 7);
    }

    private void Update()
    {
        if (TimeToAttack < 0 && EnemyHealth.Health > 0)
        {
            TimeToAttack = Random.Range(6, 7);
            PickFight();
        }
        else
        {
            TimeToAttack -= Time.deltaTime;
        }


        if(EnemyHealth.Health <= 0 && lastBreath )
        {
            lastBreath = false;
            PickFight();


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
                
                if (EnemyHealth.Health <= 0)
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
        SpecialAttackCoroutine = SpecialAttackingNumerator(SelectedCharacter, enemyObjects, InitialPos, stats,1);

        StartCoroutine(SpecialAttackCoroutine);
       SpecialAttackCoroutine = SpecialAttackingNumerator(SelectedCharacter, enemyObjects, InitialPos, stats, 1.5f);
        StartCoroutine(SpecialAttackCoroutine);
        SpecialAttackCoroutine = SpecialAttackingNumerator(SelectedCharacter, enemyObjects, InitialPos, stats, 2);
        StartCoroutine(SpecialAttackCoroutine);

    }

    IEnumerator SpecialAttackingNumerator(GameObject SelectedCharacter, GameObject enemyObject, Vector3 InitialPos, RatAttackStats stats, float delay)
    {

        yield return new WaitForSeconds(delay);
        GameObject RatMove = Instantiate(RatBusterPrefab, transform.position, Quaternion.identity, BattleCanvas.transform);


        while (Vector2.Distance(RatMove.transform.position,enemyObject.transform.position ) > 200 )
        {
            Debug.Log(Vector2.Distance(RatMove.transform.position, enemyObject.transform.position));
            RatMove.transform.position = Vector2.MoveTowards(RatMove.transform.position, enemyObject.transform.position, 4000 * Time.deltaTime);



            yield return null;
        }
        Debug.Log("close");
        yield return new WaitForSeconds(.1f);
       
        GameObject DmgNumber = Instantiate(DamageNumberPrefab, enemyObject.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + stats.SpecialDamage.ToString();

        enemyObject.GetComponent<AllyHealth>().DealDamage(stats.SpecialDamage);

        yield return new WaitForSeconds(.15f);



        while (Vector2.Distance(RatMove.transform.position,  new Vector3( enemyObject.transform.position.x - 900, enemyObject.transform.position.y, enemyObject.transform.position.z) ) > 5)
        {

            RatMove.transform.position = Vector2.MoveTowards(RatMove.transform.position, new Vector3(enemyObject.transform.position.x - 900, enemyObject.transform.position.y, enemyObject.transform.position.z), 4000 * Time.deltaTime);



            yield return null;
        }
        Destroy(RatMove);


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
        for (int i = 0; i < 3; i++)
        {
            GameObject DmgNumber = Instantiate(DamageNumberPrefab, SelectedCharacter.transform.position, Quaternion.identity, BattleCanvas.transform);
            DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

            DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
            DmgNumber.GetComponent<Text>().text = "-" + stats.BasicDamage.ToString();

            enemyObject.GetComponent<AllyHealth>().DealDamage(stats.BasicDamage);

            yield return new WaitForSeconds(.1f);
        }


        yield return new WaitForSeconds(.2f);


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

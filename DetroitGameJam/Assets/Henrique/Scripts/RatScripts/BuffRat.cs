using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffRat : MonoBehaviour
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



    [SerializeField] Animator animator;
    [SerializeField] GameObject EnemyList;
    [SerializeField] GameObject[] EnemyObjects;

    int alternate = 0;
    private void OnEnable()
    {

        InitialPosition = transform.position;

        TimeToAttack = Random.Range(5, 7);

        StartCoroutine(GetInfo());


    }

    IEnumerator GetInfo()
    {
        yield return new WaitForSeconds(.1f);

        EnemyHealth[] Obs;
        Obs = EnemyList.GetComponentsInChildren<EnemyHealth>();

        GameObject[] ActiveObjs = new GameObject[3];
        int index = 0;
        for (int i = 0; i < Obs.Length; i++)
        {
            ActiveObjs[index] = Obs[i].gameObject;
            index++;
        }
        EnemyObjects = ActiveObjs;
    }

    private void Update()
    {
        if (TimeToAttack < 0 && EnemyHealth.Health > 0)
        {
            TimeToAttack = Random.Range(5, 7);
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
        while (!found && searchtimeout > 0)
        {
            int randomsearch = Random.Range(0, 3);

            if (AllyObjs[randomsearch].GetComponent<AllyHealth>().Health > 0)
            {
                found = true;
             
                if (alternate == 0)
                {
                    alternate = 1;
                    SpecialAttack(gameObject, AllyObjs[randomsearch], InitialPosition, ratStats);

                }
                else
                {
                    alternate = 0;

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



        animator.SetBool("Flex", true);
        
        yield return new WaitForSeconds(1.5f);

        animator.SetBool("Flex", false);

        for (int i=0;i<EnemyObjects.Length; i++)
        {
          
                EnemyObjects[i].GetComponent<EnemyHealth>().IncreaseAttackTemporary(3);
               
        }

      

        yield return new WaitForSeconds(.15f);




        
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

        Vector3 OgPos = SelectedCharacter.transform.position;

        for (int i = 0; i < 30; i++)
        {
            SelectedCharacter.transform.Translate(Random.Range(-8,8), Random.Range(-8,8), 0);
            yield return new WaitForSeconds(0.01f);
            if (i == 20 || i == 40 || i ==50)
            {
                for (int a = 0; a < 30; a++)
                {
                    SelectedCharacter.transform.Translate(0, 2, 0);
                    OgPos = SelectedCharacter.transform.position;
                    yield return null;
                }
            }
            SelectedCharacter.transform.position = OgPos;

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

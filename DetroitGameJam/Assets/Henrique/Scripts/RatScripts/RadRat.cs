using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadRat : MonoBehaviour
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
    [SerializeField] GameObject RadTextObj;
    [SerializeField] GameObject kickFlipSound;
    float TimeToAttack;

    int wickedIncrement=0;

    Vector3 InitialPosition;




    private void OnEnable()
    {

        InitialPosition = transform.position;

        TimeToAttack = Random.Range(4, 7);
    }

    private void Update()
    {
        if (TimeToAttack < 0 && EnemyHealth.Health > 0)
        {
            TimeToAttack = Random.Range(3, 4);
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
                int RandomAttack = Random.Range(0, 2);

                if (EnemyHealth.Health < EnemyHealth.MaxHealth)
                {
                    SpecialAttack(gameObject, AllyObjs[randomsearch], InitialPosition, ratStats);
                    TimeToAttack -= 2;
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
            EnemyHealth.Heal(1);
            SelectedCharacterBefore.transform.position = BeforeInitialPos;
            StopCoroutine(SpecialAttackCoroutine);
            wickedIncrement++;
        }
        else
        {
            wickedIncrement = 1;
        }
        SelectedCharacterBefore = SelectedCharacter;
        BeforeInitialPos = InitialPos;
        SpecialAttackCoroutine = SpecialAttackingNumerator(SelectedCharacter, enemyObjects, InitialPos, stats);

        StartCoroutine(SpecialAttackCoroutine);

    }

    IEnumerator SpecialAttackingNumerator(GameObject SelectedCharacter, GameObject enemyObject, Vector3 InitialPos, RatAttackStats stats)
    {


      
        yield return new WaitForSeconds(.1f);

        for(int i=0;i<10;i++)
        {
            yield return new WaitForSeconds(0.01f);
            SelectedCharacter.transform.Translate(0, 1, 0);
        }
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            SelectedCharacter.transform.Rotate(0, 0, 12);
        }
        GameObject KickS =  Instantiate(kickFlipSound, transform.position, Quaternion.identity, BattleCanvas.transform);
        KickS.GetComponent<AudioSource>().pitch = 1 + wickedIncrement * .1f;

        GameObject textwik=  Instantiate(RadTextObj, transform.position, Quaternion.identity, BattleCanvas.transform);
        textwik.GetComponentInChildren<Text>().text = "wicked kflip x" + wickedIncrement;
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
            SelectedCharacter.transform.Translate(0, -1, 0);
        }
       

        yield return new WaitForSeconds(.15f);

        EnemyHealth.Heal(1);



       
        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
        SelectedCharacter.transform.position = InitialPos;
        SpecialAttackCoroutine = null;

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

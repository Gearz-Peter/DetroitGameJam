using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackingVisual : MonoBehaviour
{
    IEnumerator AttackCoroutine, AttackSpecialCoroutine, AttackAOECoroutine;
    Vector2 BeforeInitialPos;
    GameObject SelectedCharacterBefore;


    [SerializeField] GameObject[] WooshSound, NormalAttackSound;
    [SerializeField] GameObject MetalAttackSound,BottleSound;
    [SerializeField] Transform AOEposition;

    [SerializeField] GameObject[] AttackAnimes;
    [SerializeField] GameObject BattleCanvas;


    public void AttackSingle(GameObject SelectedCharacter, GameObject enemyObjects, Vector2 InitialPos, AllyAttackStat stats)
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
    IEnumerator AttackingNumerator(GameObject SelectedCharacter, GameObject enemyObject, Vector2 InitialPos, AllyAttackStat stats)
    {


        Instantiate(WooshSound[Random.Range(0,WooshSound.Length)], transform.position, Quaternion.identity);
        while (Vector2.Distance(SelectedCharacter.transform.position, enemyObject.transform.position) > 250)
        {

            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, enemyObject.transform.position, 7 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        while (SelectedCharacter.transform.eulerAngles.z < 45)
        {

            SelectedCharacter.transform.Rotate(0, 0, 5);
            yield return null;
        }
        yield return new WaitForSeconds(.15f);

        for (int i = 0; i < 14; i++)
        {
            SelectedCharacter.transform.Rotate(0, 0, -10);
            yield return null;

            if(i == 10)
            {
                Instantiate(AttackAnimes[0], enemyObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            }
        }


        Instantiate(NormalAttackSound[Random.Range(0,NormalAttackSound.Length)], transform.position, Quaternion.identity);
        enemyObject.GetComponent<EnemyHealth>().DealDamage(stats.BasicDamage);

        yield return new WaitForSeconds(.15f);




        while (Vector2.Distance(SelectedCharacter.transform.position, InitialPos) > 50 || SelectedCharacter.transform.eulerAngles.z > 15)
        {
            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, InitialPos, 15 * Time.deltaTime);


            if (SelectedCharacter.transform.eulerAngles.z > 15)
            {
                SelectedCharacter.transform.Rotate(0, 0, 8);
            }

            yield return null;
        }
        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
        SelectedCharacter.transform.position = InitialPos;

    }













    public void AttackSpecial(GameObject SelectedCharacter, GameObject enemyObjects, Vector2 InitialPos, AllyAttackStat stats)
    {
        if (AttackSpecialCoroutine != null)
        {
            SelectedCharacterBefore.transform.position = BeforeInitialPos;
            StopCoroutine(AttackSpecialCoroutine);
        }
        SelectedCharacterBefore = SelectedCharacter;
        BeforeInitialPos = InitialPos;
        AttackSpecialCoroutine = AttackSpecialNumerator(SelectedCharacter, enemyObjects, InitialPos, stats);
        StartCoroutine(AttackSpecialCoroutine);
    }


    IEnumerator AttackSpecialNumerator(GameObject SelectedCharacter, GameObject enemyObject, Vector2 InitialPos, AllyAttackStat stats)
    {

        Instantiate(WooshSound[Random.Range(0, WooshSound.Length)], transform.position, Quaternion.identity);


        while (Vector2.Distance(SelectedCharacter.transform.position, enemyObject.transform.position) > 250)
        {

            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, enemyObject.transform.position, 7 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);



        Vector2 oldPos = SelectedCharacter.transform.position;
        for (int i = 0; i < 30; i++)
        {
            SelectedCharacter.transform.position = new Vector2(SelectedCharacter.transform.position.x + Random.Range(-15, 15), SelectedCharacter.transform.position.y + Random.Range(-15, 15));
            yield return new WaitForSeconds(0.01f);
            SelectedCharacter.transform.position = oldPos;
            if(i == 20)
            {
                Instantiate(AttackAnimes[1], enemyObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            }

                }


        Instantiate(BottleSound, transform.position, Quaternion.identity);

        enemyObject.GetComponent<EnemyHealth>().DealDamage(stats.SpecialDamage);
        if(stats.Bleeding)
        {
            enemyObject.GetComponent<EnemyHealth>().ApplyBleed();
        }

        yield return new WaitForSeconds(.15f);

        while (Vector2.Distance(SelectedCharacter.transform.position, InitialPos) > 50 )
        {
            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, InitialPos, 15 * Time.deltaTime);
            yield return null;
        }
        
        SelectedCharacter.transform.position = InitialPos;

    }





    public void AttackAOE(GameObject SelectedCharacter, GameObject[] enemyObjects, Vector2 InitialPos, AllyAttackStat stats)
    {
        if (AttackAOECoroutine != null)
        {
            SelectedCharacterBefore.transform.position = BeforeInitialPos;
            StopCoroutine(AttackAOECoroutine);
        }
        SelectedCharacterBefore = SelectedCharacter;
        BeforeInitialPos = InitialPos;
        AttackAOECoroutine = AttackAOENumerator(SelectedCharacter, enemyObjects, InitialPos, stats);
        StartCoroutine(AttackAOECoroutine);
    }


    IEnumerator AttackAOENumerator(GameObject SelectedCharacter, GameObject[] enemyObject, Vector2 InitialPos, AllyAttackStat stats)
    {

        Instantiate(WooshSound[Random.Range(0, WooshSound.Length)], transform.position, Quaternion.identity);


        while (Vector2.Distance(SelectedCharacter.transform.position, AOEposition.position) > 100)
        {

            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, AOEposition.position, 7 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.1f);



        Vector2 oldPos = SelectedCharacter.transform.position;
        float RotateSpeed = 1;
        for (int i = 0; i < 700; i++)
        {
            SelectedCharacter.transform.Rotate(0, 0, 1 * RotateSpeed);
            if(i == 200 || i == 400 || i == 600)
            {
                Instantiate(WooshSound[Random.Range(0, WooshSound.Length)], transform.position, Quaternion.identity);

                for (int a = 0; a < enemyObject.Length; a++)
                {
                    yield return new WaitForSeconds(0.05f);
                   
                    Instantiate(AttackAnimes[2], enemyObject[a].transform.position, Quaternion.identity, BattleCanvas.transform);

                  
                }

               
            }

            yield return null;
            RotateSpeed += Time.deltaTime * 5;
        }


        for (int i=0;i<enemyObject.Length;i++)
        {
            yield return new WaitForSeconds(0.05f);
            Instantiate(MetalAttackSound, transform.position, Quaternion.identity);



            enemyObject[i].GetComponent<EnemyHealth>().DealDamage(stats.AOEDamage);
        }
      


        yield return new WaitForSeconds(.15f);

        while (Vector2.Distance(SelectedCharacter.transform.position, InitialPos) > 50)
        {
            SelectedCharacter.transform.position = Vector2.Lerp(SelectedCharacter.transform.position, InitialPos, 15 * Time.deltaTime);
            SelectedCharacter.transform.Rotate(0, 0, -7);
            yield return null;
        }

        SelectedCharacter.transform.position = InitialPos;
        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
    }



}

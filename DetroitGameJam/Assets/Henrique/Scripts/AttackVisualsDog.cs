using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackVisualsDog : MonoBehaviour
{
    IEnumerator AttackCoroutine, AttackSpecialCoroutine, AttackAOECoroutine;
    Vector2 BeforeInitialPos;
    GameObject SelectedCharacterBefore;


    [SerializeField] GameObject[] WooshSound;
    [SerializeField] GameObject[] DogSounds;
  
    [SerializeField] Transform AOEposition;

    [SerializeField] GameObject MetalSound;
    [SerializeField] GameObject[] AttackAnimes;
    [SerializeField] GameObject BattleCanvas;
    [SerializeField] CameraShake BattleFieldShake;

    [SerializeField] GameObject[] allieObjects;






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


        
       
        yield return new WaitForSeconds(.1f);

        while (SelectedCharacter.transform.eulerAngles.z < 30)
        {

            SelectedCharacter.transform.Rotate(0, 0, 5);
            yield return null;
        }
        yield return new WaitForSeconds(.15f);

        for (int i = 0; i < 30; i++)
        {
            SelectedCharacter.transform.Rotate(0, 0, -10);
            yield return null;

            if (i == 10)
            {
                Instantiate(DogSounds[0], enemyObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            }
        }

       // BattleFieldShake.ShakeCamera(15f, .3f, 0.05f);

        
        enemyObject.GetComponent<EnemyHealth>().DealDamage(stats.BasicDamage);

        yield return new WaitForSeconds(.15f);

        for(int i=0;i<allieObjects.Length;i++)
        {
            if(allieObjects[i] != SelectedCharacter)
            {
                allieObjects[i].GetComponent<AllyHealth>().IncreaseAttackTemporary(5);
            }
           
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


        yield return new WaitForSeconds(.1f);

        while (SelectedCharacter.transform.eulerAngles.z < 30)
        {

            SelectedCharacter.transform.Rotate(0, 0, 5);
            yield return null;
        }
        yield return new WaitForSeconds(.15f);

        for (int i = 0; i < 30; i++)
        {
            SelectedCharacter.transform.Rotate(0, 0, -10);
            yield return null;
            BattleFieldShake.ShakeCamera(15f, .3f, 0.05f);
            if (i == 10)
            {
                Instantiate(DogSounds[0], enemyObject.transform.position, Quaternion.identity, BattleCanvas.transform);
            }
        }

         


        enemyObject.GetComponent<EnemyHealth>().DealDamage(stats.SpecialDamage);

        yield return new WaitForSeconds(.15f);

       
       SelectedCharacter.GetComponent<AllyHealth>().IncreaseAttackTemporary(3);
       

        SelectedCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
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
            if (i == 200 || i == 400 || i == 600)
            {
                Instantiate(WooshSound[Random.Range(0, WooshSound.Length)], transform.position, Quaternion.identity);
                if (i == 600)
                {
                    for (int a = 0; a < enemyObject.Length; a++)
                    {
                        yield return new WaitForSeconds(0.05f);

                        Instantiate(AttackAnimes[Random.Range(0,AttackAnimes.Length)], enemyObject[a].transform.position, Quaternion.identity, BattleCanvas.transform);


                    }
                }



            }

            yield return null;
            RotateSpeed += Time.deltaTime * 5;
        }

        BattleFieldShake.ShakeCamera(15f, .3f, 0.05f);

        for (int i = 0; i < enemyObject.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            Instantiate(MetalSound, transform.position, Quaternion.identity);



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

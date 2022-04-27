using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackingVisual : MonoBehaviour
{
    IEnumerator AttackCoroutine, AttackSpecialCoroutine, AttackAOECoroutine;
    Vector2 BeforeInitialPos;
    GameObject SelectedCharacterBefore;

    [SerializeField] GameObject DamageNumberPrefab;
    [SerializeField] GameObject BattleCanvas;

    [SerializeField] Transform AOEposition;





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
        }

        GameObject DmgNumber = Instantiate(DamageNumberPrefab, SelectedCharacter.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + stats.BasicDamage.ToString();

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
                }

        GameObject DmgNumber = Instantiate(DamageNumberPrefab, SelectedCharacter.transform.position, Quaternion.identity, BattleCanvas.transform);
        DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

        DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
        DmgNumber.GetComponent<Text>().text = "-" + stats.SpecialDamage.ToString();

        enemyObject.GetComponent<EnemyHealth>().DealDamage(stats.SpecialDamage);

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
            yield return null;
            RotateSpeed += Time.deltaTime * 5;
        }
        for(int i=0;i<enemyObject.Length;i++)
        {
            GameObject DmgNumber = Instantiate(DamageNumberPrefab, enemyObject[i].transform.position, Quaternion.identity, BattleCanvas.transform);
            DmgNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 200, Random.Range(5, 6) * 150), ForceMode2D.Impulse);

            DmgNumber.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5, 5) * 23000);
            DmgNumber.GetComponent<Text>().text = "-" + stats.AOEDamage.ToString();


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
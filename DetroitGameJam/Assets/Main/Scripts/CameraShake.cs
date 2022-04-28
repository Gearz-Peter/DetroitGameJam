using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 InitialLocalPos;
    private void Start()
    {
        InitialLocalPos = transform.localPosition;
    }

    public void ShakeCamera(float Power, float Time, float Frequency)
    {
        StartCoroutine(Shake(Power, Time, Frequency));
    }

    IEnumerator Shake(float power, float time, float frequency)
    {
        
        while(time > 0)
        {
            yield return new WaitForSeconds(frequency / 2);
            transform.localPosition = new Vector3(transform.localPosition.x + Random.Range(-1, 1) * power, transform.localPosition.y + Random.Range(-1,1) * power , transform.localPosition.z);
            yield return new WaitForSeconds(frequency / 2);
            transform.localPosition = InitialLocalPos;

            time -= Time.deltaTime + frequency;
        }
        transform.localPosition = InitialLocalPos;


    }

}

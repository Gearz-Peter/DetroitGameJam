using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    [SerializeField] float min, max;
    private void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(min, max);
    }
}

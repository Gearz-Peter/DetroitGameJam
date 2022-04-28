using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameSetter : MonoBehaviour
{
    [SerializeField] string[] Names;
    [SerializeField] int pos;


    private void Awake()
    {
        GetComponentInChildren<Text>().text = Names[pos];
    }
}

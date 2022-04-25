
using UnityEngine;

public class Autodelete : MonoBehaviour
{
    [SerializeField] float time;
    void Start()
    {
        Destroy(gameObject, time);
    }

  
}

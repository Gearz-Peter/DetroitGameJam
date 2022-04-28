using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPatrol : MonoBehaviour
{
    [SerializeField] private bool Yaxis;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private float speedMod;

    public float speed;

    void Start()
    {
        speed = -speedMod;
    }

    private void Update()
    {
        if (Yaxis)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        if (Yaxis)
        {
            if (this.transform.localPosition.y > max && this.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                speed = -speedMod;
            }
            if (this.transform.localPosition.y < min && this.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                speed = speedMod;
            }
        }
        else
        {
            if (this.transform.localPosition.x > max && this.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                this.transform.localScale = new Vector2(1, 1);
                speed = -speedMod;
            }
            if (this.transform.localPosition.x < min && this.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                this.transform.localScale = new Vector2(-1, 1);
                speed = speedMod;
            }
        }
    }
}

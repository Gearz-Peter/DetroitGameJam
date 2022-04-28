using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPatrol : MonoBehaviour
{
    [SerializeField] private bool Yaxis;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private float speed;

    void Start()
    {
        speed = -Mathf.Abs(speed);
        if (Yaxis)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
    }

    private void Update()
    {
        if (Yaxis)
        {
            if(this.transform.position.y > max && this.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                
            }
        }
        else
        {
            if (this.transform.position.x > max && this.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                this.transform.localScale = new Vector2(1, 1);
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            }
            if (this.transform.position.x < min && this.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                this.transform.localScale = new Vector2(-1, 1);
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            }
        }
    }
}

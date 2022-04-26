using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D PlayerBody;

    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 6.36f;
    [SerializeField] public bool isMovementEnabled = true;

    bool isMovingUp;
    bool isMoving;
    bool ismovingLeft;

    private void Awake()
    {
        PlayerBody = GetComponent<Rigidbody2D>();
    }

    void Update() // Once per frame
    {

    }

    private void FixedUpdate() //Called multiples time per frame
    {
        Move();
    }

    private void Move()
    {
        PlayerBody.velocity = new Vector2(0, 0);
        isMoving = false;
        if (Input.GetKey("w"))
        {
            PlayerBody.velocity = new Vector2(0,movementSpeed);
            isMoving = true;
            isMovingUp = true;
        }
        if (Input.GetKey("s"))
        {
            PlayerBody.velocity = new Vector2(0,-movementSpeed);
            isMoving = true;
            ismovingUp = false;
        }
        if (Input.GetKey("a"))
        {
            PlayerBody.velocity = new Vector2(-movementSpeed, 0);
            isMoving = true;
            ismovingLeft = true;
        }
        if (Input.GetKey("d"))
        {
            PlayerBody.velocity = new Vector2(movementSpeed, 0);
            isMoving = true;
            ismovingLeft = false;
        }




        /* if (isMovementEnabled)
         {
             PlayerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, Input.GetAxis("Vertical") * movementSpeed);
         }
         if (!isMovementEnabled)
         {
             PlayerBody.velocity = new Vector2(0, 0);
         }*/
    }
}
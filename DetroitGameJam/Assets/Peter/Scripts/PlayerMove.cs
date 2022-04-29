using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D PlayerBody;

    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 6.36f;
    [SerializeField] public bool isMovementEnabled = true;
    public bool isMoving = false;

    private void Awake()
    {
        PlayerBody = GetComponent<Rigidbody2D>();
        animator.SetInteger("direction", 2);
    }

    private void FixedUpdate() //Called multiples time per frame
    {
        Move();
    }

    private void Move()
    {
        PlayerBody.velocity = new Vector2(0, 0);
        isMoving = false;
        animator.SetBool("isMoving", false);
        if (isMovementEnabled)
        {
            if (Input.GetKey("w"))
            {
                isMoving = true;

                PlayerBody.velocity = new Vector2(0, movementSpeed);
                animator.SetBool("isMoving", true);
                animator.SetInteger("direction", 0);
            }

            if (Input.GetKey("a"))
            {
                isMoving = true;
                PlayerBody.velocity = new Vector2(-movementSpeed, 0);
                animator.SetBool("isMoving",true);
                animator.SetInteger("direction", 1);
            }

            if (Input.GetKey("s"))
            {
                isMoving = true;
                PlayerBody.velocity = new Vector2(0, -movementSpeed);
                animator.SetBool("isMoving", true);
                animator.SetInteger("direction", 2);
            }
            
            if (Input.GetKey("d"))
            {
                isMoving = true;
                PlayerBody.velocity = new Vector2(movementSpeed, 0);
                animator.SetBool("isMoving", true);
                animator.SetInteger("direction", 3);
            }
        }
    }
}
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D PlayerBody;

    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 6.36f;
    [SerializeField] public bool isMovementEnabled = true;

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
        animator.SetBool("isMoving", false);
        if (isMovementEnabled)
        {
            if (Input.GetKey("w"))
            {
                PlayerBody.velocity = new Vector2(0, movementSpeed);
                animator.SetBool("isMoving", true);
                animator.SetInteger("direction", 0);
            }

            if (Input.GetKey("a"))
            {
                PlayerBody.velocity = new Vector2(-movementSpeed, 0);
                animator.SetBool("isMoving",true);
                animator.SetInteger("direction", 1);
            }

            if (Input.GetKey("s"))
            {
                PlayerBody.velocity = new Vector2(0, -movementSpeed);
                animator.SetBool("isMoving", true);
                animator.SetInteger("direction", 2);
            }
            
            if (Input.GetKey("d"))
            {
                PlayerBody.velocity = new Vector2(movementSpeed, 0);
                animator.SetBool("isMoving", true);
                animator.SetInteger("direction", 3);
            }
        }
    }
}
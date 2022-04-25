using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D PlayerBody;

    [SerializeField] private Animator animator;

    [SerializeField] private float movementSpeed = 6.36f;

    [SerializeField] private bool isMovementEnabled = true;

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
        if (isMovementEnabled)
        {
            PlayerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, Input.GetAxis("Vertical") * movementSpeed);
        }
    }
}

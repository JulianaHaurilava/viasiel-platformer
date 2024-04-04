using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed = 10f;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 500f;
    [SerializeField]
    float airMultiplier;

    [Header("Ground Check")]
    [SerializeField]
    LayerMask whatIsGround;
    private bool grounded = true;

    [Header("Health")]
    private PlayerHealth playerHealth;

    private Rigidbody2D rb;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float input)
    {
        if (grounded)
        {
            rb.velocity = new Vector2(input * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(input * speed * airMultiplier, rb.velocity.y);
        }

        if (input > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (input < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        bool moving = input != 0;
        animator.SetBool("Run", moving);
    }

    public void Jump()
    {
        if (grounded)
        {
            animator.SetBool("IsJumping", true);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            grounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        animator.SetBool("IsJumping", false);
    }
}

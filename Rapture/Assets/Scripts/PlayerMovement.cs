using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Basic References
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;


    // Movement Reqs
    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 8f;
    public bool isFacingRight = true;
    [SerializeField] bool playerAlive;

    // Animation Reqs
    public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMover();

        // Set animator to enable running when horizontal axis input is detected
        if(horizontal > 0 || horizontal < 0)
        animator.SetBool("isRunning", true);
        else
        animator.SetBool("isRunning", false);


        if(!isFacingRight && horizontal > 0f && playerAlive)
        {
            Flip();
        }
        else if(isFacingRight && horizontal < 0f && playerAlive)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void PlayerMover()
    {
        playerAlive = this.GetComponent<PlayerHealthManager>().playerAlive;
        if (playerAlive)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else if(!playerAlive)
        {
            rb.velocity = new Vector2(horizontal * speed * 0, rb.velocity.y);

        }
    }
}

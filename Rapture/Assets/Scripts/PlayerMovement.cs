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
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    // Enemy Damage Reqs
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public int attackDamage = 40;

    // Player Health reqs
    public int playerHealth = 100;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if(isFacingRight && horizontal < 0f)
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

    public void Attack(InputAction.CallbackContext context)
    {
        // Play Attack Animation

        // Get Keycode performance
        if(context.performed)
        {

        // Detects Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        // Damages Enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("Hit enemy");
        }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}

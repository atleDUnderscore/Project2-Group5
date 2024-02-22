using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    // Combat Reqs
    public Transform attackPoint;
    public float radius;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    
    public Animator animator;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        // Get Keycode performance
        if(context.performed && Time.time >= nextAttackTime)
        {
        // Attack animation
        animator.SetTrigger("Attack");

        // Detects Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, radius, enemyLayers);

        // Damages Enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("We Hit" + enemy.name);
        }

        //Reset Attack Time
        nextAttackTime = Time.time + 1f / attackRate;

        }
    

    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}

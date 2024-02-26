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

    public AudioClip attackSwing;
    public AudioClip attackHit;
    private AudioSource playerAudio;


    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
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
        playerAudio.PlayOneShot(attackSwing);

        // Detects Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, radius, enemyLayers);

        // Damages Enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealthManager>().TakeDamage(attackDamage);
            playerAudio.PlayOneShot(attackHit);
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

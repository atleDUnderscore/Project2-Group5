using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    // Combat Reqs
    public Transform attackPoint;
    public float radius;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    [SerializeField] GameObject projectileType;
    [SerializeField] GameObject projectilePos;
    public bool isGrounded;


    
    private Animator animator;

    public AudioClip attackSwing;
    public AudioClip attackHit;
    public AudioClip soulAttack;
    private AudioSource playerAudio;

    //Soul Counter
    int soulCount;
    [SerializeField] Image soulCOne;
    [SerializeField] Image soulCTwo;
    [SerializeField] Image soulCThree;
    [SerializeField] HealthBar healthBar;

    



    void Start()
    {
        isGrounded = GetComponent<PlayerMovement>().IsGrounded();
        playerAudio = GetComponent<AudioSource>();
        soulCount = 0;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && soulCount > 0)
        {
            soulCount--;
            animator.SetTrigger("RangedAttack");
        }
        if (Input.GetKeyDown(KeyCode.F) && soulCount > 0)
        {
            animator.SetTrigger("Heal");
            soulCount--;
            this.GetComponent<PlayerHealthManager>().playerHealth += 30;
            healthBar.SetHealth((int)this.GetComponent<PlayerHealthManager>().playerHealth);
        }

        //Health Cheat
        if(Input.GetKeyDown(KeyCode.M))
        {
            this.GetComponent<PlayerHealthManager>().playerHealth = 100000;
        }
        
        SoulCounter();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        
        // Get Keycode performance
        if (context.performed && Time.time >= nextAttackTime)
        {

        // Attack animation
            if(!isGrounded)
            {
                animator.SetTrigger("AirAttack");
                playerAudio.PlayOneShot(attackSwing);
            }
            else
            {
                animator.SetTrigger("Attack");
                playerAudio.PlayOneShot(attackSwing);
            }

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

    public void RangedAttack(InputAction.CallbackContext context)
    {
        
        if(context.performed && soulCount > 0)
        {
            //animator.SetTrigger("Attack");
            playerAudio.PlayOneShot(soulAttack);
            FireSoul();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Soul")
        {
            //Debug.Log("Soul Hit");
            Destroy(col.gameObject);
            soulCount++;
        }
    }

    void SoulCounter()
    {
        if (soulCount >= 3)
        {
            soulCOne.enabled = true;
            soulCTwo.enabled = true;
            soulCThree.enabled = true;
            soulCount = 3;
        }
        else if (soulCount == 2)
        {
            soulCOne.enabled = true;
            soulCTwo.enabled = true;
            soulCThree.enabled = false;
        }
        else if (soulCount == 1)
        {
            soulCOne.enabled = true;
            soulCTwo.enabled = false;
            soulCThree.enabled = false;
        }
        else if (soulCount == 0)
        {
            soulCOne.enabled = false;
            soulCTwo.enabled = false;
            soulCThree.enabled = false;
        }
        else if(soulCount < 0)
        {
            soulCount = 0;
        }
    }

    void FireSoul()
    {
        bool isFacingRight = this.GetComponent<PlayerMovement>().isFacingRight;
        GameObject playerProj = Instantiate(projectileType, projectilePos.transform.position, Quaternion.identity);
        Rigidbody2D playerProjRb = playerProj.GetComponent<Rigidbody2D>();
        if (isFacingRight)
        {
            playerProjRb.velocity = new Vector2(10, 0);
        }
        else if(!isFacingRight)
        {
            playerProjRb.velocity = new Vector2(-10, 0);
        }
        //Debug.Log("Fired");
    }
}

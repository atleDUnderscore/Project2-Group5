using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttacks : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRb;


    private float attackTimer;
    public float meleeRadius;
    public Transform meleePos;
    public LayerMask playerLayer;
    public float meleeDamage;

    public float KickbackForce;

    [SerializeField] HealthBar healthBar;

    private Animator animator;

    AudioSource flyingEnemyAudio;
    public AudioClip meleeAttack;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        animator = GetComponent<Animator>();
        flyingEnemyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(meleePos.position, player.transform.position);
        attackTimer += Time.deltaTime;
        //Debug.Log("Distance is " + distance);

        if(attackTimer > 4)
        {
            if (distance <= 3)
            {
                //Melee attack method is called on the animator component so the damage isn't dealt until the boss hand comes down
                animator.SetTrigger("Attack");
                flyingEnemyAudio.PlayOneShot(meleeAttack);         
            }           
            attackTimer = 0;
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(meleePos.position, meleeRadius, playerLayer);

        foreach(Collider2D player in hitPlayer)
        {
        player.GetComponent<PlayerHealthManager>().playerHealth -= meleeDamage;
        healthBar.SetHealth((int)player.GetComponent<PlayerHealthManager>().playerHealth);

        if(transform.position.x > player.transform.position.x)
        {
            playerRb.velocity = new Vector2(-KickbackForce, KickbackForce);
        }
        else
        {
            playerRb.velocity = new Vector2(KickbackForce, KickbackForce);
        }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleePos.transform.position, meleeRadius);
    }
}

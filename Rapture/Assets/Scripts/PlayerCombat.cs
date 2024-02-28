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
    
    public Animator animator;

    public AudioClip attackSwing;
    public AudioClip attackHit;
    private AudioSource playerAudio;

    //Soul Counter
    int soulCount;
    [SerializeField] Image soulCOne;
    [SerializeField] Image soulCTwo;
    [SerializeField] Image soulCThree;
    



    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        soulCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && soulCount > 0)
        {
            soulCount--;
            Debug.Log(soulCount);
        }
        SoulCounter();
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
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Soul")
        {
            Debug.Log("Soul Hit");
            Destroy(col.gameObject);
            soulCount++;
            Debug.Log(soulCount);
        }
    }

    void SoulCounter()
    {
        if (soulCount >= 3)
        {
            soulCOne.enabled = true;
            soulCTwo.enabled = true;
            soulCThree.enabled = true;
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
            Debug.Log(soulCount);
        }
    }
}

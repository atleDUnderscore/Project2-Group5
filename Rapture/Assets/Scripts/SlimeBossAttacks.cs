using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossAttacks : MonoBehaviour
{
    public GameObject projectileType;
    public GameObject projectile2Type;
    public Transform projectilePos;
    private GameObject player;
    [SerializeField] HealthBar healthBar;
    private float attackTimer;
    private float rapidfireTimer;
    public float radius;
    public float meleeRadius;
    public float meleeDamage;
    public Transform eruptPointA;
    public Transform eruptPointB;
    public Transform eruptPointC;
    public Transform meleePos;
    public LayerMask playerLayer;
    private Animator animator;

    public AudioClip[] audioClipArray;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(meleePos.position, player.transform.position);
        attackTimer += Time.deltaTime;
        //Debug.Log("Distance is " + distance);

        if(attackTimer > 4)
        {
            if (distance <= 5)
            {
                //Melee attack method is called on the animator component so the damage isn't dealt until the boss hand comes down
                animator.SetTrigger("isMeleeAttacking");              
            }
            else
            {
                animator.SetTrigger("isRangedAttacking");
                int RngAttack = Random.Range(0, 2);
                if(RngAttack == 1)
                {
                    FireProjectiles();
                }          
                else
                {
                    EruptProjectiles();
                }
            }
            attackTimer = 0;
        }

    }

    private IEnumerator Rapidfire()
    {
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
    }

    void FireProjectiles()
    {
        StartCoroutine(Rapidfire());
    }

    void EruptProjectiles()
    {
        Instantiate(projectile2Type, eruptPointA.position, Quaternion.identity);
        Instantiate(projectile2Type, eruptPointB.position, Quaternion.identity);
        Instantiate(projectile2Type, eruptPointC.position, Quaternion.identity);
    }

    void MeleeAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(meleePos.position, meleeRadius, playerLayer);
        player.GetComponent<PlayerHealthManager>().playerHealth -= meleeDamage;
        healthBar.SetHealth((int)player.GetComponent<PlayerHealthManager>().playerHealth);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(eruptPointA.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointB.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointC.transform.position, radius);
        Gizmos.DrawWireSphere(projectilePos.transform.position, radius);
        Gizmos.DrawWireSphere(meleePos.transform.position, meleeRadius);

    }
}

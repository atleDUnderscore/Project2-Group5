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

    public AudioClip bossSpawnVocal;
    public AudioClip meleeAttackAudio;
    public AudioClip meleeAttackVocal;
    public AudioClip rangedAttackAudio;
    public AudioClip rapidFireVocal;
    public AudioClip explosionAttackVocal;
    public AudioClip explosionAttackAudio;
    public AudioClip takeDamageAudio;
    public AudioClip giveDamageAudio;
    public AudioClip dieAudio;
    AudioSource boss1Audio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        animator = GetComponent<Animator>();
        boss1Audio = GetComponent<AudioSource>();
        boss1Audio.PlayOneShot(bossSpawnVocal);
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(meleePos.position, player.transform.position);
        attackTimer += Time.deltaTime;
        //Debug.Log("Distance is " + distance);

        if(attackTimer > 4)
        {
            if (distance <= 2)
            {
                //Melee attack method is called on the animator component so the damage isn't dealt until the boss hand comes down
                animator.SetTrigger("isMeleeAttacking");
                boss1Audio.PlayOneShot(meleeAttackVocal);
                boss1Audio.PlayOneShot(meleeAttackAudio);             
            }
            else
            {
                animator.SetTrigger("isRangedAttacking");
                int RngAttack = Random.Range(0, 2);
                if(RngAttack == 1)
                {
                    FireProjectiles();
                    boss1Audio.PlayOneShot(rapidFireVocal);
                    boss1Audio.PlayOneShot(rangedAttackAudio);
                }          
                else
                {
                    EruptProjectiles();
                    boss1Audio.PlayOneShot(explosionAttackAudio, 0.6f);
                    boss1Audio.PlayOneShot(explosionAttackVocal);
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
        boss1Audio.PlayOneShot(giveDamageAudio);
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

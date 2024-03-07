using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_BossChild : MonoBehaviour
{
    private HealthBar healthBar;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private GameObject player;
    private Rigidbody2D playerRb;
    [SerializeField] private Transform playerTransform;
    private PlayerMovement playerMovement;
    private float damage = 10;
    private float kickbackForce;

    private float chaseTimer;


    private AudioSource spiderAudio;
    public AudioClip hitSound;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        speed = 5;
        chaseTimer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        chaseTimer += Time.deltaTime;
        if(chaseTimer > 4)
        {
            ChasePlayer();
        }
        else
        {
            Stop();
        }

        if(transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector3(1,1,1);
            }
        else
            {
                transform.localScale = new Vector3(-1,1,1);
            }        
    }

    private void ChasePlayer()
    {
        speed = 5;
        animator.SetBool("isPatrol", true);
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            kickbackForce = 3;
            other.gameObject.GetComponent<PlayerHealthManager>().playerHealth -= damage;
            healthBar.SetHealth((int)other.gameObject.GetComponent<PlayerHealthManager>().playerHealth);
            playerRb.velocity = new Vector2(-kickbackForce, 0);
            spiderAudio.PlayOneShot(hitSound);
            playerMovement.playerIsWebbed = false;
            chaseTimer = 0;
            //Stop();
        }
    }
    private void Stop()
    {
        animator.SetBool("isPatrol", false);
        speed = 0;
        rb.velocity = new Vector2(speed, 0);
    }
}

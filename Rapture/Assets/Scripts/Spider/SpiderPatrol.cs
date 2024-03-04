using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatrol : MonoBehaviour
{
    private HealthBar healthBar;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private float speed;
    private GameObject player;
    [SerializeField] private Transform playerTransform;
    private PlayerMovement playerMovement;
    private float damage = 10f;

    private AudioSource spiderAudio;
    public AudioClip hitSound;
    public AudioClip projectileAudio;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        currentPoint = pointA.transform;
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance > 12)
        {
            Patrol();
        }
        else
        {
            Stop();
        }
    }

    private void Patrol()
    {
        animator.SetBool("isPatrol", true);
        speed = 4;
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }

        
    }

    private void Stop()
    {
        pointA.SetActive(false);
        pointB.SetActive(false);
        if(playerMovement.playerIsWebbed == false)
        {
            animator.SetBool("isPatrol", false);
            speed = 0;
            rb.velocity = new Vector2(speed, 0);
                if(transform.position.x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(1,1,1);
                }
                else
                {
                    transform.localScale = new Vector3(-1,1,1);
                }
        }
        else
        {
            animator.SetBool("isPatrol", true);
            speed = 8;
            rb.velocity = new Vector2(speed, 0);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                if(transform.position.x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(1,1,1);
                }
                else
                {
                    transform.localScale = new Vector3(-1,1,1);
                }
        }
    }

    private void OnColliderEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthManager>().playerHealth -= damage;
            healthBar.SetHealth((int)other.gameObject.GetComponent<PlayerHealthManager>().playerHealth);
            spiderAudio.PlayOneShot(hitSound);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

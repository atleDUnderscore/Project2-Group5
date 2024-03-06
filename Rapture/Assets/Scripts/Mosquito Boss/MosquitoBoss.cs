using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoBoss : MonoBehaviour
{
    //Player References
    private GameObject player;
    private Rigidbody2D playerRb;
    [SerializeField] HealthBar healthBar;


    //Self References
    private Animator animator;
    private Rigidbody2D rb;

    //Movement References
    public GameObject boss2Activator;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;
    public GameObject pointE;
    public GameObject pointF;
    private Transform currentPoint;
    [SerializeField] private float speed;
    private float movementTimer;

    //Combat Variables
    private float attackTimer;
    private float kbForce = 3.0f;
    private float damage = 15f;
    public Transform projectilePos;
    public GameObject projectileType;

    //Audio
    private AudioSource mosBossAudio;
    public AudioClip fireProjSound;
    public AudioClip changePosSound;
    public AudioClip chargeWarningSound;
    

    // Start is called before the first frame update
    void Start()
    {
        //Get Player scripts and colliders
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();

        //Get self stuff
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mosBossAudio = GetComponent<AudioSource>();

        //Lets go
        currentPoint = boss2Activator.transform;
        speed = 10;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Flips Sprite based on if player is on the left or right
        if(transform.position.x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(1,1,1);
                }
                else
                {
                    transform.localScale = new Vector3(-1,1,1);
                } 

        //Set up timers        
        movementTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        //Check boss' current active point
        CheckBossPos();   

        if(attackTimer > 3 && speed == 0 && currentPoint != pointA.transform)
        {
            FireProjectile();
            attackTimer = 0;
        }     
    }

    private void CheckBossPos()
    {
        if(currentPoint == boss2Activator.transform)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, pointA.transform.position, speed * Time.deltaTime);
        }
        else if(currentPoint == pointA.transform)
        {
            PointA();
        }
        else if(currentPoint == pointB.transform)
        {
            PointB();
        }
        else if(currentPoint == pointC.transform)
        {
            PointC();
        }
        else if(currentPoint == pointD.transform)
        {
            PointD();
        }
        else if(currentPoint == pointE.transform)
        {
            PointE();
        }
        else if(currentPoint == pointF.transform)
        {
            PointF();
        }
    }
    private void PointA()
    {
        speed = 0;
        if(movementTimer > 3)
        {
            speed = 8;   
            transform.position = Vector2.MoveTowards(this.transform.position, pointB.transform.position, speed * Time.deltaTime);
        }
    }
    private void PointB()
    {
        speed = 0;
        if(movementTimer > 15)
        {
            speed = 8;
            transform.position = Vector2.MoveTowards(this.transform.position, pointC.transform.position, speed * Time.deltaTime);
        }
    }
    private void PointC()
    {
        speed = 0;
        if(movementTimer > 30)
        {
            speed = 30;
            transform.position = Vector2.MoveTowards(this.transform.position, pointD.transform.position, speed * Time.deltaTime);
        }
    }
    private void PointD()
    {
        speed = 0;
        if(movementTimer > 45)
        {
            speed = 8;
            transform.position = Vector2.MoveTowards(this.transform.position, pointE.transform.position, speed * Time.deltaTime);
        }
    }
    private void PointE()
    {
        speed = 0;
        if(movementTimer > 60)
        {
            speed = 8;
            transform.position = Vector2.MoveTowards(this.transform.position, pointF.transform.position, speed * Time.deltaTime);
        }

    }
    private void PointF()
    {
        movementTimer = 0;
        transform.position = Vector2.MoveTowards(this.transform.position, pointA.transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthManager>().playerHealth -= damage;
            healthBar.SetHealth((int)other.gameObject.GetComponent<PlayerHealthManager>().playerHealth);
            playerRb.velocity = new Vector2(-kbForce, 0);
        }
        if(other.gameObject.CompareTag("MB_PointA"))
        {
            currentPoint = pointA.transform;      
        }
        else if(other.gameObject.CompareTag("MB_PointB"))
        {
            currentPoint = pointB.transform;            
        }
        else if(other.gameObject.CompareTag("MB_PointC"))
        {
            currentPoint = pointC.transform;            
        }
        else if(other.gameObject.CompareTag("MB_PointD"))
        {
            currentPoint = pointD.transform;           
        }
        else if(other.gameObject.CompareTag("MB_PointE"))
        {
            currentPoint = pointE.transform;            
        }
        else if(other.gameObject.CompareTag("MB_PointF"))
        {
            currentPoint = pointF.transform;         
        }       
    }
    private void FireProjectile()
    {
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        mosBossAudio.PlayOneShot(fireProjSound);
    }
    private void OnDrawGizmos()
    {   //Draw gizmos for movement points & projectile shoot point
        Gizmos.DrawWireSphere(projectilePos.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointC.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointD.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointE.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointF.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        Gizmos.DrawLine(pointB.transform.position, pointC.transform.position);
        Gizmos.DrawLine(pointC.transform.position, pointD.transform.position);
        Gizmos.DrawLine(pointD.transform.position, pointE.transform.position);
        Gizmos.DrawLine(pointE.transform.position, pointF.transform.position);
        Gizmos.DrawLine(pointE.transform.position, pointB.transform.position);
        Gizmos.DrawLine(pointF.transform.position, pointA.transform.position);
    }
}

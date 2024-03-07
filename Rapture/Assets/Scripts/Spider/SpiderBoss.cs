using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : MonoBehaviour
{
    //Combat
    public GameObject projectileType;
    public Transform projectilePos;
    private float attackTimer;
    private float rapidfireTimer;
    public float meleeRadius;
    private float meleeDamage = 25.0f;
    public float KickbackForce = 3.0f;
    public Transform meleePos;
    public LayerMask playerLayer;
    private float spawnTimer;

    //Movement
    private float speed;
    public GameObject jumptoPos;
    public GameObject floorPos;
    [SerializeField] private bool isRoof = false;
    private float movementTimer;

    //References
    private Rigidbody2D selfRb;
    private GameObject player;
    private Rigidbody2D playerRb;
    private HealthBar healthBar;
    private Animator animator;
    public GameObject enemyType;
    public Transform enemy1Pos;
    public Transform enemy2Pos;
    private float gizmoRadius = 2.0f;




    // Start is called before the first frame update
    void Start()
    {
        //Get Player scripts and colliders
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();

        //Get self stuff
        animator = GetComponent<Animator>();
        selfRb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        //Flips Sprite based on if player is on the left or right
        if(transform.position.x > player.transform.position.x && isRoof == false)
            {
                transform.localScale = new Vector3(1,1,1);
            }
        else if((transform.position.x < player.transform.position.x && isRoof == false))
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        else if((transform.position.x > player.transform.position.x && isRoof == true))
            {
                transform.localScale = new Vector3(1,-1,1);
            }
        else if((transform.position.x < player.transform.position.x && isRoof == true))
            {
                transform.localScale = new Vector3(-1,-1,1);
            }
        
        
        //Creates distance variable        
        float distance = Vector2.Distance(meleePos.position, player.transform.position);

        //Set Attack Timer
        attackTimer += Time.deltaTime;
        Debug.Log("Attack Timer: " + attackTimer);

        //Set Movement Timer
        movementTimer += Time.deltaTime;

        if(movementTimer > 10 && isRoof == false)
        {
            JumpToRoof();
        }
        
        if(movementTimer > 20 && isRoof == true)
        {
            JumpToGround();
        }

        if(attackTimer > 5)
        {
            FireProjectiles();
            attackTimer = 0;
        }
    }

private IEnumerator Rapidfire()
    {
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
    }

    void FireProjectiles()
    {
        StartCoroutine(Rapidfire());
    }

    private void JumpToRoof()
    {
        
        attackTimer = 0;
        speed = 10;
        movementTimer = 0;
        isRoof = true;
        animator.SetTrigger("Jump");
        selfRb.gravityScale = -1;
        transform.position = Vector2.MoveTowards(this.transform.position, jumptoPos.transform.position, speed * Time.deltaTime);
        transform.localScale = new Vector3(1,-1,1);
        Instantiate(enemyType, enemy1Pos.position, Quaternion.identity);
        Instantiate(enemyType, enemy2Pos.position, Quaternion.identity);
    }
    private void JumpToGround()
    {
        speed = 10;
        movementTimer = 0;
        isRoof = false;
        animator.SetTrigger("Jump");
        selfRb.gravityScale = 1;
        transform.position = Vector2.MoveTowards(this.transform.position, floorPos.transform.position, speed * Time.deltaTime);
        transform.localScale = new Vector3(1,1,1);
    }

    private void OnDrawGizmos()
    {
        //Draw Spheres
        Gizmos.DrawWireSphere(jumptoPos.transform.position, gizmoRadius);
        Gizmos.DrawWireSphere(floorPos.transform.position, gizmoRadius);
        Gizmos.DrawLine(jumptoPos.transform.position, floorPos.transform.position);

        //Draw Line through spheres
        Gizmos.DrawWireSphere(enemy1Pos.transform.position,gizmoRadius);
        Gizmos.DrawWireSphere(enemy2Pos.transform.position,gizmoRadius);

    }
}

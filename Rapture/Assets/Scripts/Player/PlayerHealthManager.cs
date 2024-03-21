using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject playerSpawn;
    public float maxHealth = 100;
    public float playerHealth;
    public bool playerAlive;
    public bool isTakingDamage;
    private Animator animator;

    public PlayerDeathScreen playerDeathScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        healthBar.SetMaxHealth((int)maxHealth);
        playerAlive = true;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if(playerHealth <= 0)
        {
            Debug.Log("Player is Dead");
            playerHealth = 1;
            playerAlive = false;
            animator.SetBool("isDead",true);
            //StartCoroutine(PlayerDeathTimer());
            playerDeathScreen.Setup();

        }

        //if(Input.GetKeyDown(KeyCode.LeftShift))
       // {
        //    playerAlive = true;
        //    playerHealth = maxHealth;
        //    healthBar.SetMaxHealth((int)maxHealth);
       // }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "KillPlayer")
        {
            healthBar.SetHealth(0);
            playerHealth = 0;
        }
        else if (col.tag == "HealPlayer")
        {
            healthBar.SetMaxHealth((int)maxHealth);
            playerHealth = maxHealth;
        }
        else if(col.tag == "Respawn")
        {
            playerSpawn.transform.position = col.gameObject.transform.GetChild(0).position;
        }
    }

    /*IEnumerator PlayerDeathTimer()
    {
        yield return new WaitForSeconds(2.5f);
        this.gameObject.transform.position = playerSpawn.transform.position;
        playerHealth = maxHealth;
        playerAlive = true;
        healthBar.SetMaxHealth((int)maxHealth);
    }*/

}

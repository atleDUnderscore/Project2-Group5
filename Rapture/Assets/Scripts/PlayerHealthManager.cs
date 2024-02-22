using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    public float maxHealth = 100;
    public float playerHealth;
    public bool playerAlive;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        healthBar.SetMaxHealth((int)maxHealth);
        playerAlive = true;

    }

    private void Update()
    {
        if(playerHealth <= 0)
        {
            Debug.Log("Player is Dead");
            playerHealth = 1;
            playerAlive = false;

        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAlive = true;
            playerHealth = maxHealth;
            healthBar.SetMaxHealth((int)maxHealth);
        }
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
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;
    [SerializeField] Slider enemyHBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyHBar.maxValue = maxHealth;
        enemyHBar.value = currentHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyHBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        // Die Animation

        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;

        Destroy(gameObject);
       
    }
}

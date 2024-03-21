using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;
    [SerializeField] Slider enemyHBar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] GameObject soulObject;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip deathSound;
    private Collider2D collider2D;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemyHBar.maxValue = maxHealth;
        enemyHBar.value = currentHealth;
        collider2D = GetComponent<Collider2D>();
    }


    public void TakeDamage(int damage)
    {
        damageFlash.Flash();
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
        //Play Death Sound
        audioSource.PlayOneShot(deathSound);

        // Die Animation
        animator.SetTrigger("isDead");

        //Disable Collider
        collider2D.enabled = false;
        StartCoroutine(AutoDestroy());
        //Spawn Soul
        Instantiate(soulObject, gameObject.transform.position, Quaternion.identity);       
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(.7f);
        if (gameObject)
        {
            Destroy(gameObject);    
        }
    }
}

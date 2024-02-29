using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileNonFollowing : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float damage;

    public bool isSticky;

    private AudioSource projectileAudio;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        projectileAudio = GetComponent<AudioSource>();

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(-1, 0).normalized * force;

        //float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthManager>().playerHealth -= damage;
            healthBar.SetHealth((int)other.gameObject.GetComponent<PlayerHealthManager>().playerHealth);
            projectileAudio.PlayOneShot(hitSound);
            Destroy(gameObject);
        }
    }
}

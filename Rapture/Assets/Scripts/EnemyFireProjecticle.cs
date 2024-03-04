using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireProjectile : MonoBehaviour
{

    public GameObject projectileType;
    public Transform projectilePos;

    private AudioSource slime2Audio;
    public AudioClip slime2ProjectileAudio;

    private GameObject player;

    private float timer;
    public float attackDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slime2Audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 15)
        {
            timer += Time.deltaTime;

            if(timer > attackDelay)
            {
                timer = 0;
                FireProjectile();
            }
        }

    }

    void FireProjectile()
    {
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        slime2Audio.PlayOneShot(slime2ProjectileAudio);
    }


    
}

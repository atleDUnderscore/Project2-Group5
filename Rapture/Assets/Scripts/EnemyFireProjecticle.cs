using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireProjectile : MonoBehaviour
{

    public GameObject projectileType;
    public Transform projectilePos;

    private GameObject player;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 15)
        {
            timer += Time.deltaTime;

            if(timer > 3)
            {
                timer = 0;
                FireProjectile();
            }
        }

    }

    void FireProjectile()
    {
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
    }


    
}

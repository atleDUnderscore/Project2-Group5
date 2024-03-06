using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoNest : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform spawnPos;
    private float spawnCount;
    private float spawnTimer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer > 8)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, spawnPos.position, Quaternion.identity);
    }

}

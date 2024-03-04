using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<EnemyHealthManager>().TakeDamage(50);
            Destroy(this.gameObject);
        }
    }
}

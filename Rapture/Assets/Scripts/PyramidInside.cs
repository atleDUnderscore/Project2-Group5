using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidInside : MonoBehaviour
{
    [SerializeField] GameObject insideSpawn;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.transform.position = insideSpawn.transform.position;
        }
    }
}

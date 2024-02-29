using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderPounce : MonoBehaviour
{

    private GameObject player;
    private PlayerMovement playerMovement;
    private Rigidbody2D playerRB;
    private Rigidbody2D spiderRB;

    private float pounceForce = 5f;
    public float knockBackForce;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        spiderRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        attackTimer += Time.deltaTime;

        if (attackTimer > 4)
        {
            if (distance <= 10)
            {
                Pounce();
            }
        }
        attackTimer = 0;
    }
    void Pounce()
    {
        Debug.Log("Pounce");
        if (transform.position.x > player.transform.position.x)
        {
            spiderRB.velocity = new Vector2(-pounceForce, pounceForce);
        }
        else
        {
            spiderRB.velocity = new Vector2(pounceForce, pounceForce);
        }
    }
}

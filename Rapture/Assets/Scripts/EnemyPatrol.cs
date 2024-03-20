using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private float speed;
    private GameObject player;
    [SerializeField] private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentPoint = pointA.transform;
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance > 12)
        {
            Patrol();
        }
        else
        {
            Stop();
        }
    }

    private void Patrol()
    {
        speed = 2;
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }

        
    }

    private void Stop()
    {
        speed = 0;
        rb.velocity = new Vector2(speed, 0);
        if(transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private float speed;
    private GameObject player;
    bool isAdvancing;
    [SerializeField] private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentPoint = pointA.transform;
        speed = 2;
        isAdvancing = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > 12)
        {
            Patrol();
            isAdvancing = false;
        }
        else
        {
            Advance();
        }

        if (transform.position.x > player.transform.position.x && isAdvancing)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(transform.position.x < player.transform.position.x && isAdvancing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Patrol()
    {
        speed = 1;
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {

            rb.velocity = new Vector2(speed, 0);
        }
        else
        {

            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }


    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        Debug.Log("Flipped");
    }

    public void Advance()
    {
        speed = 2;
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
        isAdvancing = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}

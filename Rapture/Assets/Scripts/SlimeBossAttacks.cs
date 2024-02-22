using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossAttacks : MonoBehaviour
{
    public GameObject projectileType;
    public Transform projectilePos;
    private GameObject player;
    private float attackTimer;
    private float rapidfireTimer;
    public float radius;
    public Transform eruptPointA;
    public Transform eruptPointB;
    public Transform eruptPointC;
    public Transform eruptPointD;
    public Transform eruptPointE;
    public Transform eruptPointF;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        attackTimer += Time.deltaTime;


        if (distance < 2 && attackTimer > 4)
        {
            attackTimer = 0;
            MeleeAttack();
        }
        else if(distance > 2 && attackTimer > 4)
        {
            attackTimer = 0;
            FireProjectiles();
        }

    }
    
    private IEnumerator Rapidfire()
    {
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(projectileType, projectilePos.position, Quaternion.identity);
    }

    void FireProjectiles()
    {
        StartCoroutine(Rapidfire());
    }

    void EruptProjectiles()
    {
        Instantiate(projectileType, eruptPointA.position, Quaternion.identity);
        Instantiate(projectileType, eruptPointB.position, Quaternion.identity);
        Instantiate(projectileType, eruptPointC.position, Quaternion.identity);
        Instantiate(projectileType, eruptPointD.position, Quaternion.identity);
        Instantiate(projectileType, eruptPointE.position, Quaternion.identity);
        Instantiate(projectileType, eruptPointF.position, Quaternion.identity);
    }

    void MeleeAttack()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(eruptPointA.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointB.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointC.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointD.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointE.transform.position, radius);
        Gizmos.DrawWireSphere(eruptPointF.transform.position, radius);
        Gizmos.DrawWireSphere(projectilePos.transform.position, radius);

    }
}

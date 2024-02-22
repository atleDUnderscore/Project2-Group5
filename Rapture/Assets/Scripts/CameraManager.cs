using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 3f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public float lookAheadDistance = 5f, lookAheadSpeed = 3f;

    private float lookOffset;

    [SerializeField] private Transform target;
    public PlayerMovement player;



    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if(player.rb.velocity.x > 0f)
        {
            lookOffset = lookAheadDistance;
        }
        if(player.rb.velocity.x < 0f)
        {
            lookOffset = -lookAheadDistance;
        }

        targetPosition.x = player.transform.position.x + lookOffset;

        
    }
}

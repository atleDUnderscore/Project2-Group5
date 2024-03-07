using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyStop : MonoBehaviour
{
    [SerializeField] GameObject mummy;
    Vector2 mummyPosition;
    int count;

    public void Start()
    {
        count = 0;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(count < 1)
            {
                mummyPosition = mummy.transform.position;
                count++;
            }
            else if(count >= 1)
            {
                mummy.transform.position = mummyPosition;
            }
            
        }
    }
}

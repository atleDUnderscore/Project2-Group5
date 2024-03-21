using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCon : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Game Ends (Cutscene or back to title screen)");
        }
    }
}

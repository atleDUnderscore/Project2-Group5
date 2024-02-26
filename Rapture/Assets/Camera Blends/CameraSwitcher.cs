using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public Animator animator;
    public AudioSource audio;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SwitchState();
        }
    }

    void SwitchState()
    {
        animator.Play("Boss Room Camera");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public Animator animator;
    public Level1MusicManager musicManager;
    [SerializeField] private Transform bossLoc;
    [SerializeField] private GameObject boss;
    

    void Start()
    {
        musicManager.GetComponent<AudioSource>();        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SwitchState();
            musicManager.musicAudio.Stop();
            musicManager.musicAudio.clip = musicManager.bossMusic;
            musicManager.musicAudio.Play();
            SpawnBoss();
            Destroy(gameObject);
        }
    }

    void SpawnBoss()
    {
        Instantiate(boss, bossLoc.position, Quaternion.identity);
    }

    void SwitchState()
    {
        animator.Play("Boss Room Camera");
    }
}

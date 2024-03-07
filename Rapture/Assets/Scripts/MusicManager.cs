using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainMusic;
    public AudioClip bossMusic;
    public AudioSource musicAudio;
    // Start is called before the first frame update

    void Awake()
    {
        musicAudio = GetComponent<AudioSource>();
        
    }
    void Start()
    {
        musicAudio.clip = mainMusic;
        musicAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBG : MonoBehaviour
{
    private AudioSource music;
    [SerializeField] private List<AudioClip> clips;
    

    public static MusicBG Instance { get { return instance; } }
    private static MusicBG instance;


    void Start()
    {
        music = GetComponent<AudioSource>();
        
        MusicPlay();
    }

    private void MusicPlay()
    {
        music.clip = clips[UnityEngine.Random.Range(0, clips.Count)];
        music.Play();
    }
}

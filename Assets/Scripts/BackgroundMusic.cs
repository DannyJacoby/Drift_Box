using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> soundtrack;
    private int maxSize;
    public AudioSource musicPlayer;

    private int lastPlayed = 0;
    // public List<AudioClip> carSounds;
    // public AudioSource carPlayer;

    // public AudioSource carSlipAndSlideSound;
    //
    private void Awake()
    {
        var objs = GameObject.FindGameObjectsWithTag("Audio Player");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        maxSize = soundtrack.Count;
        DontDestroyOnLoad(this);
        PlayNextSong();
    }

    
    private void FixedUpdate()
    {
        if (!musicPlayer.isPlaying)
        {
            PlayNextSong();
        }

        if (!Input.GetKey(KeyCode.N)) return;
        musicPlayer.Stop();
        PlayNextSong();

        // if (!carPlayer.isPlaying)
        // {
        //     PlayIdle();
        // }
        
    }

    public void PlayNextSong()
    {
        if (musicPlayer.isPlaying) return;
        var index = Random.Range(0, maxSize);
        if (index == lastPlayed) index = (index + 1) % maxSize;
        lastPlayed = index;
        musicPlayer.clip = soundtrack[index];
        musicPlayer.Play();
    }
    
    // 0 is start, 1 is idle, 2 is loud, 3 is squeal
    // public void PlayEngineStart()
    // {
    //     carPlayer.clip = carSounds[0];
    //     carPlayer.Play();
    //     carPlayer.volume = 0.2f;
    //     carPlayer.loop = false;
    // }
    //
    // public void PlayIdle()
    // {
    //     carPlayer.clip = carSounds[1];
    //     carPlayer.Play();
    //     carPlayer.volume = 0.2f;
    //     carPlayer.loop = true;
    // }

    
    // public void PlayMovementSound()
    // {
    //     if (carPlayer.isPlaying && carPlayer.clip == carSounds[2] ) return;
    //     carPlayer.clip = carSounds[2];
    //     carPlayer.Play();
    //     carPlayer.volume = 0.035f;
    //     carPlayer.loop = true;
    // }

    // TODO currently fighting the above forward sound so yaaay bad thing wooo
    // public void PlaySlidingSound()
    // {
    //     if (carPlayer.clip == soundtrack[0] || carPlayer.clip == soundtrack[1])
    //     {
    //         carSlipAndSlideSound.Stop();
    //         return;
    //     }
    //     if (carSlipAndSlideSound.isPlaying) return;
    //     carSlipAndSlideSound.Play();
    // }

}

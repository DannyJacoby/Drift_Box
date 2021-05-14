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
    public AudioSource musicPlayer;
    
    public int currentSong = -1;
    // public List<AudioClip> carSounds;
    // public AudioSource carPlayer;

    // public AudioSource carSlipAndSlideSound;
    private void Awake()
    {
        // var objs = GameObject.FindGameObjectsWithTag("Audio Player");
        // if (objs.Length > 1)
        // {
        //     Destroy(this.gameObject);
        //     return;
        // }
        
        Shuffle();
        DontDestroyOnLoad(this);
    }

    
    private void FixedUpdate()
    {
        if (!musicPlayer.isPlaying)
        {
            PlayNextSong();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayLastSong();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayNextSong();
        }

    }

    public void PlayNextSong()
    {
        musicPlayer.Stop();
        currentSong += 1;
        if (currentSong >= soundtrack.Count) currentSong = 0;
        musicPlayer.clip = soundtrack[currentSong];
        musicPlayer.Play();
    }

    public void PlayLastSong()
    {
        musicPlayer.Stop();
        currentSong -= 1;
        if (currentSong < 0) currentSong = soundtrack.Count - 1;
        musicPlayer.clip = soundtrack[currentSong];
        musicPlayer.Play();
    }

    public void Shuffle()
    {
        for(var i = 0; i < soundtrack.Count; i++)
        {
            var clip = soundtrack[i];
            var randomInt = Random.Range(i, soundtrack.Count);
            soundtrack[i] = soundtrack[randomInt];
            soundtrack[randomInt] = clip;
        }
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

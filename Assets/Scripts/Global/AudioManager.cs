using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clip;

    public void Start()
    {
        int i = Random.Range(2, clip.Length);
        PlayAudio(i);
    }

    public void PlayAudio(int i)
    {
        GetComponent<AudioSource>().PlayOneShot(clip[i]);
    }
}

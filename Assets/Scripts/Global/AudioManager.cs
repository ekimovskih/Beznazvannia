using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Agred;

    public void PlayAudioAgred()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Agred.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Agred;
    public AudioClip[] Death;
    public AudioClip[] Attack;
    public AudioClip[] AggroLost;
    public AudioClip[] PickUp;
    public AudioClip[] Hit;
    public AudioClip[] Mine;

    public void PlayAudioAgred()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Agred.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
    
    public void PlayAudioDeath()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Death.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
    
    public void PlayAudioAttack()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Attack.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
    
    public void PlayAudioAggroLost()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, AggroLost.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
    
    public void PlayAudioPickUp()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, PickUp.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
    
    public void PlayAudioHit()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Hit.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }
    
    public void PlayAudioMine()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Mine.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i]);
    }

}

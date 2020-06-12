using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Agred;
    public AudioClip[] Death;
    public AudioClip[] Attack;
    public AudioClip[] AggroLost;
    public AudioClip[] PickUp;
    public AudioClip[] Hit;
    public AudioClip[] Mine;

    public int state = 0;

    public void PlayAudioAgred()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Agred.Length);
        GetComponent<AudioSource>().PlayOneShot(Agred[i], 0.5f);
    }
    
    public void PlayAudioDeath(int i)
    {
        //Debug.Log(Agred.Length);
        //int i = Random.Range(0, Death.Length);
        GetComponent<AudioSource>().PlayOneShot(Death[i], 0.5f);
    }
    
    public void PlayAudioAttack()
    {
        int i = Random.Range(0, Attack.Length);
        Debug.Log(i);
        GetComponent<AudioSource>().PlayOneShot(Attack[i], 0.5f);
    }
    
    public void PlayAudioAggroLost()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, AggroLost.Length);
        GetComponent<AudioSource>().PlayOneShot(AggroLost[i], 0.5f);
    }
    
    public void PlayAudioPickUp()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, PickUp.Length);
        GetComponent<AudioSource>().PlayOneShot(PickUp[i], 0.5f);
    }
    
    public void PlayAudioHit()
    {
        //Debug.Log(Agred.Length);
        int i = Random.Range(0, Hit.Length);
        GetComponent<AudioSource>().PlayOneShot(Hit[i], 0.5f);
    }
    
    public void PlayAudioMine(int i)
    {
        //Debug.Log(Agred.Length);
        //int i = Random.Range(0, Mine.Length);
        GetComponent<AudioSource>().PlayOneShot(Mine[i], 0.5f);
    }

}

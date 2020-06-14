using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_1_scr : Enemy_propertys_scr
{
    void Start()
    {
        player = GameObject.Find("Player");
        //go2 = this.gameObject;
        audioController = this.gameObject.GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        Zcorrector();
        if (CanMove)
        {
            IsAgred(player);
            if (distance < AttackRadius)
            {
                if (CanAttack && CanMove)
                {
                    StartCoroutine(RadiusAttack());
                    //audioController.PlayAudio(Random.Range(1,5));
                }
            }
            else if (CanAttack && Agred && CanMove)
            {
                //StartCoroutine(Jump(dirrection));
                AgredMovement();
                if (EnemyState == 0)
                {
                    GetComponent<AudioManager>().PlayAudioAgred();
                    EnemyState = 1;
                }
                
            }
            else if (!Agred && CanMove)
            {
                //Debug.Log("Tupa otdihaem");
                if (EnemyState == 1)
                {
                    Debug.Log("jnjhdfkcz");
                    audioController.PlayAudioAggroLost();
                    EnemyState = 0;
                }
                

                SimplePatrole();
                
            }
        }
        if (player == null)
        {
            StopAllCoroutines();
            CanMove = false;
        }
    }

}

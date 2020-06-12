using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_1_scr : Enemy_propertys_scr
{
    
    void Start()
    {
        player = GameObject.Find("Player");
        go2 = this.gameObject;
        audioController = go2.GetComponent<AudioManager>();

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
                    audioController.PlayAudio(Random.Range(1,5));
                }
            }
            else if (CanAttack && Agred && CanMove)
            {
                //StartCoroutine(Jump(dirrection));
                AgredMovement();
                audioController.PlayAudio(0);
                
            }
            else if (!Agred && CanMove)
            {
                //Debug.Log("Tupa otdihaem");
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

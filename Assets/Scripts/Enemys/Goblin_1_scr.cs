using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_1_scr : Enemy_propertys_scr
{
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
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
                }
            }
            else if (CanAttack && Agred && CanMove)
            {
                //StartCoroutine(Jump(dirrection));
                AgredMovement();
            }
            else if (!Agred && CanMove)
            {
                //Debug.Log("Tupa otdihaem");
                SimplePatrole();
            }
        }
    }


}

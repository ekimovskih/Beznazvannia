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
    void Update()
    {
        AttackDirrection();
        Zcorrector();
        IsAgred(player);
        if (distance < AttackRadius)
        {
            if (CanAttack)
            {
                StartCoroutine(RadiusAttack());
            }
        }
        else if (CanAttack && Agred)
        {
            //StartCoroutine(Jump(dirrection));
            SimpleMovement();
        }
        else if (CanAttack && !Agred)
        {
            float valuex = Random.Range(-1f, 1f);
            float valuey = Random.Range(-1f, 1f);
            //Debug.Log(valuex + " " + valuey);
            float value = Mathf.Sqrt(valuex * valuex + valuey * valuey);
            //value = 1f;
            Vector2 NewDir = new Vector2(valuex / value, valuey / value);
            //StartCoroutine(Jump(NewDir));
        }
    }
}

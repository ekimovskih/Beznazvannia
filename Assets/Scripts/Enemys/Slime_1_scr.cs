using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_1_scr : Enemy_propertys_scr
{
    // Start is called before the first frame update
    //private GameObject player = null;
    //private Vector3 playerPosition;
    public float JumpMoveWaitTime = 1f;
    
    
    void Start()
    {
        player = GameObject.Find("Player");
        audioController = this.gameObject.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Zcorrector();
        if (CanMove)
        {
            
            IsAgred(player); // вместе со всем высчитывает и дисанцию до игрока
            if (distance < AttackRadius)
            {
                if (CanAttack)
                {
                    StartCoroutine(JumpAttack());
                }
            }
            else if (CanAttack && Agred)
            {
                StartCoroutine(Jump(dirrection, JumpMoveWaitTime));
                //SimpleMovement();
            }
            else if (CanAttack && !Agred)
            {
                float valuex = Random.Range(-1f, 1f);
                float valuey = Random.Range(-1f, 1f);
                Vector2 NewDir = new Vector2(valuex, valuey).normalized;
                StartCoroutine(Jump(NewDir, JumpMoveWaitTime));
            }
        }
            
    }
}

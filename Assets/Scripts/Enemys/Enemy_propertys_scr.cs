using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_propertys_scr : MonoBehaviour
{
    public float Health = 100f;
    public int Damage;
    
    public float MovementSpeed = 50f;
    public float AgroRadius;
    public float DisAgroRadius;
    [HideInInspector] public bool Agred = false;
    [HideInInspector] public float distance;
    public float AttackRadius;
    [HideInInspector] public Vector2 dirrection;
    public bool CanAttack = true;
    [HideInInspector] public bool CanChillWalk = false;
    [HideInInspector] public bool SupportBool = true;
    [HideInInspector] public bool TookDMG = false;
    public bool CanMove = true;
    public GameObject AttackZone = null;
    public float AttackRotatiionSpeed = 10;
    public float JumpAttackSpeed = 2f;
    public float AttackPrepare = 3f;
    public float AttackDuration = 3f;
    public float AttackRelax =3f;
    public GameObject[] DeathDrop;
    public int[] DropRates;
    public bool imune = false;
    public GameObject rip;
    public int DeathSound = 0;
    public int state = 0;
    
    //protected GameObject go2 = null;
    protected AudioManager audioController;

    
    [HideInInspector]public GameObject player = null;

    public void Zcorrector()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.y);
    }
    public void IsAgred(GameObject player)
    {
        Vector3 ThisPos = this.gameObject.transform.position;
        Vector3 PlayerPos = player.transform.position;
        ThisPos = PlayerPos - ThisPos;
        distance = Mathf.Sqrt(ThisPos.x * ThisPos.x + ThisPos.y * ThisPos.y);
        if (distance < AgroRadius)
        {
            Agred = true;
            dirrection = new Vector2(ThisPos.x / distance, ThisPos.y / distance);
        }
        else if (distance > DisAgroRadius)
        {
            Agred = false;
        }

    }

    public void CheckLives()
    {
        if (Health<1)
        {
            StopAllCoroutines();
            Health = 10001;
            DropItems();
            GameObject grave = Instantiate(rip, transform.position, Quaternion.identity);
            grave.GetComponent<AudioManager>().PlayAudioDeath(DeathSound);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int dmg, float KnockBack, Vector3 point)
    {
        if (Health< 10000)
        {
            Health -= dmg;
            if (!TookDMG)
            {
                audioController.PlayAudioHit();
            }
            TookDMG = true;
            Debug.Log(this.gameObject + " took dmg " + dmg);
            StopAllCoroutines();
            StartCoroutine(Stopattck());
            CheckLives();
            GetComponent<Rigidbody2D>().AddForce((transform.position - point) * KnockBack);
            
        }
    }
    public IEnumerator Stopattck()
    {
        if (!SupportBool)
        {
            SupportBool = false;
           
            //StopAllCoroutines();
            //Debug.Log("# Start Stop " + Time.time);
            //StopCoroutine(Stopattck());
            //StopCoroutine(Stopattck());
            //StopCoroutine(Stopattck());

            //CanAttack = false;
            yield return new WaitForSeconds(AttackRelax);
            //imune = false ;
            CanMove = true;
            CanAttack = true;
            SupportBool = true; ;
            //Debug.Log("# Stop Stop " + Time.time);
        }
        else if (TookDMG)
        {
            //StopAllCoroutines();
            //StopCoroutine(Stopattck());
            TookDMG = false;
            SupportBool = false;
            //Debug.Log("# Start Stop " + Time.time);
            //StopAllCoroutines();
            //StopCoroutine(Stopattck());
            //StopCoroutine(Stopattck());
            CanAttack = false;
            CanMove = false;
            //CanAttack = false;
            yield return new WaitForSeconds(AttackRelax);
            CanMove = true;
            CanAttack = true;
            SupportBool = true; ;
            //Debug.Log("# Stop Stop " + Time.time);
        }
        /*
        StopCoroutine(Stopattck());
        yield return new WaitForSeconds(2f);
        Debug.Log("Hello");
        */
    }


    public IEnumerator JumpAttack()
    {
        if (SupportBool)
        {
            CanAttack = false;
            CanMove = false;
            SupportBool = false; 
            yield return new WaitForSeconds(AttackPrepare);
            StartCoroutine(Stopattck());
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(dirrection * JumpAttackSpeed);
            GameObject attack = Instantiate(AttackZone, this.gameObject.transform);
            attack.GetComponent<EnemyAttack_scr>().Damage = Damage;
            audioController.PlayAudioAttack();
            //StartCoroutine(Stopattck());
            //Debug.Log("Attack " + Time.time);
        }
    }

    public IEnumerator RadiusAttack()
    {
        if (SupportBool)
        {
            CanAttack = false;
            CanMove = false;
            SupportBool = false; 
            yield return new WaitForSeconds(AttackPrepare);
            StartCoroutine(Stopattck());
            Instantiate(AttackZone, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            audioController.PlayAudioAttack();
            //StartCoroutine(Stopattck());
            //Debug.Log("Attack " + Time.time);
        }
        
    }
    public void AgredMovement()
    {
        transform.Translate(dirrection * Time.deltaTime * MovementSpeed);
    }
    public void SimplePatrole()
    {
        if (CanChillWalk)
        {
            //Debug.Log("Just move");
            transform.Translate(dirrection * Time.deltaTime * MovementSpeed/2);
        }
        else
        {
            if (SupportBool)
            {
                //Debug.Log("New dirrection");
                StartCoroutine(NewPatroleDirrection(4f));
            }
        }
    }

    public IEnumerator Jump(Vector2 dir, float waitTime)
    {
        //Debug.Log("Jump move");
        CanAttack = false;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * MovementSpeed);
        //transform.Translate(dirrection * Time.deltaTime);
        yield return new WaitForSeconds(waitTime);
        CanAttack = true;
    }
    public IEnumerator NewPatroleDirrection(float timer)
    {
        SupportBool = false;
        float valuex = Random.Range(-1f, 1f);
        float valuey = Random.Range(-1f, 1f);
        dirrection = new Vector2(valuex, valuey).normalized;
        CanChillWalk = false;
        yield return new WaitForSeconds(timer/2);
        CanChillWalk = true;
        yield return new WaitForSeconds(timer);
        CanChillWalk = false;
        SupportBool = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanChillWalk)
        {
            StopAllCoroutines();
            StartCoroutine(NewPatroleDirrection(2f));
        }     
    }

    public void DropItems()
    {
        for (int i = 0; i< DeathDrop.Length; i++)
        {
            int chance = Random.Range(0, 100);
            if (chance < DropRates[i])
            {
                GameObject drop = Instantiate(DeathDrop[i], transform.position + Vector3.up * i / 100f, Quaternion.identity);
                //drop.GetComponent<Drop_scr>().count = Random.Range(0, DropRates[i].y+1);
            }
        }
    }
}

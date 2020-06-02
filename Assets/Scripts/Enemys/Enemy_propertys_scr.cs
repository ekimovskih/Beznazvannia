using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_propertys_scr : MonoBehaviour
{
    public float Health = 100f;
    //public float Damage;
    
    public float MovementSpeed = 50f;
    public float AgroRadius;
    public float DisAgroRadius;
    [HideInInspector] public bool Agred = false;
    [HideInInspector] public float distance;
    public float AttackRadius;
    [HideInInspector] public Vector2 dirrection;
    public bool CanAttack = true;
    public GameObject AttackZone = null;
    public float AttackRotatiionSpeed = 10;
    public float JumpAttackSpeed = 2f;
    public float AttackPrepare = 3f;
    public float AttackDuration = 3f;
    public float AttackRelax =3f;

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

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    public void AttackDirrection()
    {
        Transform trans = AttackZone.transform;
        Vector3 vectorToTarget = player.transform.position - trans.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * AttackRotatiionSpeed);
        trans.position = transform.position;
    }

    public IEnumerator JumpAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(AttackPrepare);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dirrection * JumpAttackSpeed);
        AttackZone.SetActive(true);
        yield return new WaitForSeconds(AttackDuration);
        AttackZone.SetActive(false);
        yield return new WaitForSeconds(AttackRelax);
        CanAttack = true;
    }

    public IEnumerator RadiusAttack()
    {
        CanAttack = false;
        //Debug.Log("Prepare");
        yield return new WaitForSeconds(AttackPrepare);
        //Debug.Log("Attack");
        /*
        AttackZone.GetComponent<EnemyAttack_scr>().CanDealDMG = true;
        yield return new WaitForSeconds(AttackDuration);
        AttackZone.GetComponent<EnemyAttack_scr>().CanDealDMG = false;
        */
        //AttackZone.SetActive(true);
        AttackZone.GetComponent<EnemyAttack_scr>().Attack();
        yield return new WaitForSeconds(AttackDuration);
        AttackZone.GetComponent<EnemyAttack_scr>().Attack();
        //AttackZone.SetActive(false);
        //Debug.Log("Brfrfrfrf");
        yield return new WaitForSeconds(AttackRelax);
        CanAttack = true;
    }
    public void SimpleMovement()
    {
        transform.Translate(dirrection * Time.deltaTime * MovementSpeed);
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
}

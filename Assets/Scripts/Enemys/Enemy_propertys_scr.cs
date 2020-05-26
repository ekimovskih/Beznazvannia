using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_propertys_scr : MonoBehaviour
{
    public float Health = 100f;
    //public float Damage;
    public float AttackSpeed = 2f;
    public float MovementSpeed = 50f;
    public float AgroRadius;
    public float DisAgroRadius;
    [HideInInspector] public bool Agred = false;
    [HideInInspector] public float distance;
    public float AttackRadius;
    [HideInInspector] public Vector2 dirrection;
    public bool CanAttack = true;
    public GameObject AttackZone = null;

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

    public float AttackDirrection()
    {
        Vector3 Self = this.gameObject.transform.position;
        Vector3 Other = player.transform.position;
        float x = Other.x - Self.x;
        float y = Other.y - Self.y;
        float r = Mathf.Sqrt(x * x + y * y);
        Debug.Log(Mathf.Acos((x / r) / 180f) * Mathf.PI);
        return Mathf.Acos(x / r);
        /*
        Vector3 Self = this.gameObject.transform.RotateAround(;
        Vector3 Other = player.transform.position;
        return new Vector3(Other.x, Other.y, Self.z);
    */
    }
}

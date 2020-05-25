using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_propertys_scr : MonoBehaviour
{
    public float Health = 100f;
    public float Damage;
    public float AgroRadius;
    public float DisAgroRadius;
    [HideInInspector] public bool Agred = false;
    [HideInInspector] public float distance;
    public float AttackRadius;
    [HideInInspector] public Vector2 dirrection;
    public bool CanAttack = true;
    public GameObject AttackZone = null;
    

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
}

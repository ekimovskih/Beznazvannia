using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public int Damage = 5;
    public float Strength;
    //public bool Follow = false;
    //private bool ComandToAttack = false;
    //private Transform Parent;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInParent<Transform>();
        //Parent = GetComponentInParent<Transform>();
        //Enemy_propertys_scr ggTimer = GetComponentInParent<Enemy_propertys_scr>();
        AttackDirrection();
        GetComponent<Rigidbody2D>().AddForce((player.position - transform.position).normalized * Strength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player_movement_scr>().TakeDamage(Damage, Strength, transform);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Cell")
        {
            Destroy(this.gameObject);
        }
    }
    public void AttackDirrection()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
        //transform.position = new Vector3(0, 0, 0);
    }
}

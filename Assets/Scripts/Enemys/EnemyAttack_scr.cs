using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_scr : MonoBehaviour
{
    public int Damage = 5;
    public float Strength;
    public bool CanDealDMG = false;
    private bool AAA = false;
    private GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if (CanDealDMG && AAA)
        {
            player.GetComponent<Player_movement_scr>().TakeDamage(Damage, Strength, transform);
            //CanDealDMG = !CanDealDMG;
        }
    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("hola");
        if (collision.gameObject.tag == "Player" && CanDealDMG)
        {
            player.GetComponent<Player_movement_scr>().TakeDamage(Damage, Strength, transform);
        }         
    }
    */
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CanDealDMG = true;
            //player.GetComponent<Player_movement_scr>().TakeDamage(Damage, Strength, transform);
        }  
        else
        {
            CanDealDMG = false;
        }
    }
    */
    public void Attack()
    {
        AAA = !AAA;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CanDealDMG = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CanDealDMG = false;
        }
    }
    
}

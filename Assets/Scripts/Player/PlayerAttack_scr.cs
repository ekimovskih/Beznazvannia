using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_scr : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Cursor;
    private Drop_scr Item;
    //public GameObject Player = null;
    public int damage;
    public float lifetime;
    public float KnockBack;
    private bool attack = false;
    private GameObject player;
    private Player_movement_scr playerComponent;

    private void Awake()
    {
        Cursor = GameObject.Find("Cursor");
        Item = Cursor.GetComponent<Cursor_scr>().InActiveSlot;
        damage = Item.DMG;
        KnockBack = Item.knockBack;
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<Player_movement_scr>();
        lifetime = 0.1f;//Item.ActionSpeed/3f;
    }
    private void Start()
    {
        if (!(playerComponent.wasHited))
        {
            playerComponent.CanMove = false;
            playerComponent.WorkInd.GetComponent<SpriteRenderer>().enabled = true;
            //playerComponent.WorkInd.SetActive(true);
            AttackDirrection();
        }
        else
        {
            Destroy(this.gameObject);
        }
           
        
            //StartCoroutine(SelfDestroy(lifetime));
           
    }
    private void Update()
    {
        if (playerComponent.wasHited&&!attack)
        {
            StartCoroutine(SelfDestroy(lifetime));
            Debug.Log("deal dmg");
        }
        //Debug.Log(playerComponent.wasHited);
        if (Input.GetMouseButtonUp(0))
        {
            attack = true;
            StartCoroutine(SelfDestroy(lifetime));
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerComponent.CanMove = true;
            StartCoroutine(SelfDestroy(lifetime));
        }
        
        transform.position = player.transform.position;
        AttackDirrection();
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"&& attack)
        {
            //Debug.Log("deal dmg");
            collision.transform.GetComponent<Enemy_propertys_scr>().TakeDamage(damage, KnockBack, transform.position);
            StartCoroutine(SelfDestroy(lifetime));
        }
    }

    IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        playerComponent.CanMove = true;
        playerComponent.WorkInd.GetComponent<SpriteRenderer>().enabled = false;
        //playerComponent.WorkInd.SetActive(false);
        Destroy(this.gameObject);
        //Debug.Log("selfdestroy");
        
    }

    void AttackDirrection()
    {
        //Transform trans = this.transform;
        Vector3 vectorToTarget = Cursor.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);


        playerComponent.SpriteMoveChanger(vectorToTarget.x, vectorToTarget.y);
        //trans.position = transform.position;
    }
}

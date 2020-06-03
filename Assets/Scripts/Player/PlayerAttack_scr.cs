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
    private void Awake()
    {
        Cursor = GameObject.Find("Cursor");
        Item = Cursor.GetComponent<Cursor_scr>().InHand;
        damage = Item.DMG;
        KnockBack = Item.knockBack;
        lifetime = Item.ActionSpeed/3f;
    }
    private void Start()
    {
        StartCoroutine(SelfDestroy(lifetime));
        AttackDirrection();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("deal dmg");
            collision.transform.GetComponent<Enemy_propertys_scr>().TakeDamage(damage, KnockBack, transform.position);
        }
    }

    IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
        //Debug.Log("selfdestroy");
    }

    void AttackDirrection()
    {
        Transform trans = this.transform;
        Vector3 vectorToTarget = Cursor.transform.position - trans.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * 1000);
        //trans.position = transform.position;
    }
}

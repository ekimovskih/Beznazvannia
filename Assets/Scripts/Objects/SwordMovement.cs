using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //public Quaternion dir;
    public float xy;
    public float mod;
    private PlayerAttack_scr papa;
    bool ab;
    void Start()
    {
        papa = transform.GetComponentInParent<PlayerAttack_scr>();
        mod = 2.75f / papa.lifetime / 2f;
        ab = papa.above;
        
        
        xy = transform.rotation.eulerAngles.z;
        //transform.rotation = Quaternion.AngleAxis(30, new Vector3(0,0,1));
        //Debug.Log(mod);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ab)
        {
            transform.position = papa.transform.position + new Vector3(0, 0, 0.1f);
        }
        //Debug.Log(transform.rotation.eulerAngles.z);
        //transform.rotation = Quaternion.AngleAxis(xy, new Vector3(0, 0, 1));
        Quaternion n = Quaternion.AngleAxis(xy, new Vector3(0, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, n, Time.deltaTime * 1000);
        xy += mod;
        //Debug.Log(transform.rotation.eulerAngles);
        //transform.rotation = Quaternion.AngleAxis(xy, new Vector3(0,0,1));
        //transform.rotation = Quaternion.Euler(0, 0, xy);
        //transform.rotation = dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("deal dmg");
            collision.transform.GetComponent<Enemy_propertys_scr>().TakeDamage(papa.damage, papa.KnockBack, transform.position);
        }
    }
}

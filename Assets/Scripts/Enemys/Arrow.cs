using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damage = 5;
    public float Strength;
    public Sprite sprite;
    //public bool Follow = false;
    //private bool ComandToAttack = false;
    //private Transform Parent;
    public bool friendly = true;
    private string Target;
    private Vector3 TrgetPos;
    private bool kostil = false;
    // Start is called before the first frame update
    void Start()
    {
        
        if(sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
        if (friendly)
        {
            Target = "Enemy";
            TrgetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            Target = "Player";
            TrgetPos = GameObject.Find("Player").GetComponentInParent<Transform>().position + Vector3.up*0.5f;
        }
        Vector3 dirrection = (TrgetPos - transform.position).normalized;
        Vector3 dir = dirrection;
        transform.position += new Vector3(0, 0.3f, -0.3f);// + dirrection*5;
        Debug.Log(Target + dirrection);
        //Parent = GetComponentInParent<Transform>();
        //Enemy_propertys_scr ggTimer = GetComponentInParent<Enemy_propertys_scr>();
        AttackDirrection();
        GetComponent<Rigidbody2D>().AddForce(dir * Strength);
        StartCoroutine(SelfDestroy());
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Target))
        {
            if (friendly)
            {
                collision.transform.GetComponent<Enemy_propertys_scr>().TakeDamage(Damage, Strength, transform.position);
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.GetComponent<Player_movement_scr>().Vulnerable)
            {
                collision.GetComponent<Player_movement_scr>().TakeDamage(Damage, Strength, transform);
                Debug.Log(collision.gameObject);
                Destroy(this.gameObject);
            }

         
        }
        if (collision.gameObject.tag == "UpWall")
        {
            Destroy(this.gameObject);
            //StartCoroutine(SelfDestroy(0.01f));
        }
        if (kostil && collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<Enemy_propertys_scr>().TakeDamage(Damage/2, Strength, transform.position);
            Destroy(this.gameObject);
        }
    }
    
    public void AttackDirrection()
    {
        Vector3 vectorToTarget = TrgetPos - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
        //transform.position = new Vector3(0, 0, 0);
    }
    
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        kostil = true;
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
        //Debug.Log("selfdestroy");
    }
}

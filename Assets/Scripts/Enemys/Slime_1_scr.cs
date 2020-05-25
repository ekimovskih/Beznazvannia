using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_1_scr : Enemy_propertys_scr
{
    // Start is called before the first frame update
    private GameObject player = null;
    private Vector3 playerPosition;
    public float AttackSpeed = 2f;
    public float MovementSpeed = 50f;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.y);
        IsAgred(player); // вместе со всем высчитывает и дисанцию до игрока
        if (distance < AttackRadius)
        {
            if (CanAttack)
            {
                StartCoroutine(Attack());
            }
        }
        else if (CanAttack && Agred)
        {
            StartCoroutine(Jump(dirrection));
            //SimpleMovement();
        }
        else if (CanAttack && !Agred)
        {
            float valuex = Random.Range(-1f, 1f);
            float valuey = Random.Range(-1f, 1f);
            Debug.Log(valuex + " " + valuey);
            float value = Mathf.Sqrt(valuex * valuex + valuey * valuey);
            //value = 1f;
            Vector2 NewDir = new Vector2(valuex / value, valuey / value);
            StartCoroutine(Jump(NewDir));
        }
    }
    
    IEnumerator Attack()
    {
        CanAttack = false;
        Debug.Log("Prepare");
        yield return new WaitForSeconds(1f);
        Debug.Log("Jump attack");
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dirrection * AttackSpeed);
        Debug.Log("Brfrfrfrf");
        yield return new WaitForSeconds(3f);
        CanAttack = true;
    }
    
    IEnumerator Jump(Vector2 dir)
    {
        Debug.Log("Jump move");
        CanAttack = false;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * MovementSpeed);
        //transform.Translate(dirrection * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        CanAttack = true;
    }
    void SimpleMovement()
    {
        transform.Translate(dirrection * Time.deltaTime * 1.5f);
    }
}

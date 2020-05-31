using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_1_scr : Enemy_propertys_scr
{
    // Start is called before the first frame update
    public Quaternion dir = new Quaternion(0, 0, 0, 0);
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.Rotate(new Vector3(0,0,1),AttackDirrection());
        this.gameObject.transform.LookAt(player.transform);
        Zcorrector();
        IsAgred(player);
        if (distance < AttackRadius)
        {
            if (CanAttack)
            {
                StartCoroutine(Attack());
            }
        }
        else if (CanAttack && Agred)
        {
            //StartCoroutine(Jump(dirrection));
            SimpleMovement();
        }
        else if (CanAttack && !Agred)
        {
            float valuex = Random.Range(-1f, 1f);
            float valuey = Random.Range(-1f, 1f);
            //Debug.Log(valuex + " " + valuey);
            float value = Mathf.Sqrt(valuex * valuex + valuey * valuey);
            //value = 1f;
            Vector2 NewDir = new Vector2(valuex / value, valuey / value);
            //StartCoroutine(Jump(NewDir));
        }
    }

    void SimpleMovement()
    {
        transform.Translate(dirrection * Time.deltaTime * 1.5f);
    }
    IEnumerator Attack()
    {
        CanAttack = false;
        //Debug.Log("Prepare");
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("Jump attack");

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dirrection * AttackSpeed);
        AttackZone.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        AttackZone.SetActive(false);
        //Debug.Log("Brfrfrfrf");
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }

}

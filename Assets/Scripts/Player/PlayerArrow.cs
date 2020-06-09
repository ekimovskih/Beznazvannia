using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public int damage = 5;
    public float Strength;
    public Sprite sprite;

    public bool friendly = true;
    private string Target;
    private Vector3 TrgetPos;
    private GameObject Cursor;
    private Drop_scr Item;

    public float lifetime;
    public float KnockBack;
    private bool attack = false;
    private GameObject player;
    private Player_movement_scr playerComponent;
    public GameObject Arrow;
    private Color inc = Color.red;
    // Start is called before the first frame update

    private void Awake()
    {
        Cursor = GameObject.Find("Cursor");
        Item = Cursor.GetComponent<Cursor_scr>().InActiveSlot;
        damage = Item.DMG;
        KnockBack = Item.knockBack;
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<Player_movement_scr>();
        playerComponent.WorkInd.GetComponent<SpriteRenderer>().enabled = true;
        lifetime = 0.1f;//Item.ActionSpeed/3f;
    }
    void Start()
    {
        inc.a = 0;
        GetComponent<SpriteRenderer>().color = inc;
        if (!(playerComponent.wasHited))
        {
            playerComponent.CanMove = false;
            //playerComponent.WorkInd.SetActive(true);
            AttackDirrection();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (playerComponent.wasHited && !attack)
        {
            StartCoroutine(SelfDestroy(lifetime));
            //Debug.Log("deal dmg");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 d3 = Cursor.transform.position - transform.position;
            Vector2 d2 = new Vector2(d3.x, d3.y);
            if (!(d2.magnitude < 1.5f))
            {
                Instantiate(Arrow, transform.position, Quaternion.identity);
                
            }
            StartCoroutine(SelfDestroy(lifetime));
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerComponent.CanMove = true;
            StartCoroutine(SelfDestroy(lifetime));
        }

        if (inc.a < 1)
        {
            inc.a += 0.01f;
        }
        GetComponent<SpriteRenderer>().color = inc;
        AttackDirrection();
    }
    
    public void AttackDirrection()
    {
        Vector3 Ppos = player.transform.position;
        transform.position = Ppos;
        Vector3 vectorToTarget = Cursor.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);

        playerComponent.SpriteMoveChanger(vectorToTarget.x, vectorToTarget.y);
        //transform.position = new Vector3(0, 0, 0);
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
}

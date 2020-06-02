using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_scr : MonoBehaviour
{
    public int Health = 100;
    [HideInInspector] private float horizInput;
    [HideInInspector] private float verticInput;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] private bool Vulnerable = true;
    private bool CanMove = true;
    public float speed=3f; // сорость бега
    public float ShiftSpeed = 2f; // + скорость с зажатым LeftShift
    public float JumpStrengh = 3000f;
    public float JMPImune = 1f;
    [HideInInspector] public string playerView = "none";
    public Sprite[] playerStates = new Sprite[3]; // стоячие положения игрока
    private SpriteRenderer PlayerSprite;
    public GameObject InHand;
    //private GameObject Cursor = null;
    //[HideInInspector] public int CurrInvSlot = 0;
    public GameObject WorkInd = null;
    private bool InventoryFull;
    private Inventory_scr Inventory = null;
    private Transform PlayerStuff;
    public GameObject AttackZone = null;
    public Transform Cursor;

    private void Awake()
    {
        Cursor = GameObject.Find("Cursor").transform;
        PlayerStuff = GameObject.Find("PlayerStuff").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //Cursor = GameObject.Find("Cursor");
        Inventory = GameObject.Find("InventoryManager").GetComponent<Inventory_scr>();
        InventoryFull = Inventory.isFull;
        //ChangeInHand(CurrInvSlot);
        PlayerSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //InventoryFull = Inventory.GetComponent<Inventory_scr>().isFull;
        if (CanMove)
        {
            Movement();
        }
        AttackDirrection();
    }
    public void Movement()
    {
        Jump();
        //InventoryAction();
        horizInput = Input.GetAxis("Horizontal");
        verticInput = Input.GetAxis("Vertical");
        bool Shift = Input.GetKey(KeyCode.LeftShift);
        float Boost;
        {
            if (Shift)
            {
                Boost = ShiftSpeed;
            }
            else
            {
                Boost = 0f;
            }
        }
        transform.Translate(horizInput * (speed + Boost) * Time.deltaTime, 0, 0);
        transform.Translate(0, verticInput * (speed + Boost) * Time.deltaTime, 0);
        

        //float x = horizInput * (speed + Boost) * Time.deltaTime;
        //float y = verticInput * (speed + Boost) * Time.deltaTime;
        //Vector2 Move = new Vector3(x,y);

        ///rb.MovePosition(transform.position + Move * Time.deltaTime);
        //rb.velocity = Move;

        PlayerStuff.position = new Vector3(0,0,transform.position.y);
        //transform.;




        SpriteMoveChanger(horizInput, verticInput);
    }

    void Jump()
    { if (Input.GetKeyDown(KeyCode.Space))
        {
            //Vector2 Move = new Vector2(JumpStrengh * horizInput, JumpStrengh * verticInput);
            
            Vector2 f = new Vector2(horizInput, verticInput);
            this.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(f.normalized * JumpStrengh);
            StartCoroutine(JumpImune(JMPImune));
            
            //rb.velocity = Move;
            Debug.Log("Jump");
        }
    }
    public void SpriteMoveChanger(float horizInput, float verticInput)
    {
        if (horizInput==0 && verticInput == 0)
        {
            playerView = "none";
            //PlayerSprite.flipX = false;
        }
        else if (playerView== "none" && horizInput != 0 || Mathf.Abs(verticInput) <= Mathf.Abs(horizInput))
        {
            if (horizInput > 0)
            {
                playerView = "right";
                PlayerSprite.flipX = false;
                PlayerSprite.sprite = playerStates[1];
            }
            else
            {
                playerView = "left";
                PlayerSprite.flipX = true;
                PlayerSprite.sprite = playerStates[1];
            }
        }
        else if (playerView == "none" && verticInput != 0 || Mathf.Abs(verticInput) >= Mathf.Abs(horizInput))
        {
            if (verticInput > 0)
            {
                playerView = "up";
                PlayerSprite.flipX = false;
                PlayerSprite.sprite = playerStates[2];
            }
            else
            {
                playerView = "down";
                PlayerSprite.sprite = playerStates[0];
                PlayerSprite.flipX = false;
            }
        }
    }

    public IEnumerator MouseHitAction(float WaitTime, Vector2 CurrDir)
    {
        CanMove = false;
        SpriteMoveChanger(CurrDir.x, CurrDir.y);
        yield return new WaitForSeconds(WaitTime);
        CanMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Drop")
        {
            if (InventoryFull)
            {
                collision.transform.GetComponent<Drop_scr>().Bounce(this.gameObject);
            }
            else if (Inventory != null)
            {
                Inventory.AddItem(collision.gameObject);
            }
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vulnerable)
        {
            if (collision.gameObject.tag == "EnemyAttack")
            {
                EnemyAttack_scr attack = collision.gameObject.GetComponent<EnemyAttack_scr>();
                int dmg = attack.Damage; // оставил если придетя визуализировать урон
                InertionFromEnAttack(collision.gameObject.transform, attack.Strength);
                TakeDamage(dmg);
                Debug.Log("You took dmg " + dmg + " Now Your health " + Health);
            }
        }
    }
    */

    public void TakeDamage(int dmg)
    {
        if (Vulnerable)
        {
            StartCoroutine(Imune());
            Health -= dmg;
            Debug.Log("You took dmg " + dmg + " Now Your health " + Health);

        }
    }
    public void TakeDamage(int dmg, float strength, Transform point)
    {
        if (Vulnerable)
        {
            StartCoroutine(Imune());
            Health -= dmg;
            Debug.Log("You took dmg " + dmg + " Now Your health " + Health);
            InertionFromEnAttack(point, strength);
        }
    }
    IEnumerator Imune()
    {
        Vulnerable = false;
        yield return new WaitForSeconds(0.5f);
        CanMove = true;
        yield return new WaitForSeconds(1f);
        Vulnerable = true;
    }
    IEnumerator JumpImune(float time)
    {
        Vulnerable = false;
        CanMove = false;
        yield return new WaitForSeconds(time);
        CanMove = true;
        Vulnerable = true;
        Debug.Log("NoImune");
    }
    void AttackDirrection()
    {
        Transform trans = AttackZone.transform;
        Vector3 vectorToTarget = Cursor.transform.position - trans.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * 100);
        trans.position = transform.position;
    }
    void InertionFromEnAttack(Transform point, float strength)
    {
        CanMove = false;
        //Debug.Log("Uletai");
        Vector3 dirrection = transform.position - point.position;
        Vector2 dir = new Vector2(dirrection.x, dirrection.y);
        rb.AddForce(dir.normalized * strength);
        //this.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(dir * strength);
    }
}

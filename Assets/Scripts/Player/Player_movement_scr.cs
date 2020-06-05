using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_scr : MonoBehaviour
{
    public int Health = 100;
    public int Stamina = 20;
    [HideInInspector] private float horizInput;
    [HideInInspector] private float verticInput;
    [HideInInspector] public Rigidbody2D rb;
    public bool Vulnerable = true;
    public bool CanMove = true;
    //public bool CanIteract = true;
    public float speed=3f; // сорость бега
    public float ShiftSpeed = 2f; // + скорость с зажатым LeftShift
    public float JumpStrengh = 3000f;
    public int JumpWaste = 5;
    public float JMPImune = 1f;
    private bool CanJump = true;
    [HideInInspector] public string playerView = "none";
    public Sprite[] playerStates = new Sprite[3]; // стоячие положения игрока
    private SpriteRenderer PlayerSprite;
    public GameObject InHand;
    //private GameObject Cursor = null;
    //[HideInInspector] public int CurrInvSlot = 0;
    public GameObject WorkInd = null;
    private bool InventoryFull;
    private Inventory_scr Inventory = null;
    private Transform PlayerContainer;
    public GameObject AttackZone = null;
    public Transform Cursor;
    private bool regenerate = true;
    public int RegenHP = 0;
    public int RegenSTM = 1;

    private void Awake()
    {
        Cursor = GameObject.Find("Cursor").transform;
        PlayerContainer = GameObject.Find("PlayerContainer").transform;
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
        Camera.main.transform.position = transform.position + new Vector3(0,0,-30);
        //InventoryFull = Inventory.GetComponent<Inventory_scr>().isFull;
        if (CanMove)
        {
            Movement(speed);
        }
        else
        {
            Movement(speed/4);
        }
    }
    private void Update()
    {
        Jump();
        if (Health < 0)
        {
            this.gameObject.SetActive(false);
            
        }
        StartCoroutine(Regen());
    }
    public void Movement(float speed)
    {
        
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

        PlayerContainer.position = new Vector3(0,0,transform.position.y);
        //transform.;



        if (CanMove)
        {
            SpriteMoveChanger(horizInput, verticInput);
        }
    }

    void Jump()
    { if (Input.GetKeyDown(KeyCode.Space) && CanJump && (Stamina- JumpWaste > 0))
        {
            //Vector2 Move = new Vector2(JumpStrengh * horizInput, JumpStrengh * verticInput);
            Stamina -= JumpWaste;
            Vector2 f = new Vector2(horizInput, verticInput);
            this.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(f.normalized * JumpStrengh);
            StartCoroutine(JumpImune(JMPImune));
            
            //rb.velocity = Move;
            //Debug.Log("Jump");
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
        StopCoroutine("MouseHitAction");
        CanMove = false;
        //CanIteract = false;
        SpriteMoveChanger(CurrDir.x, CurrDir.y);
        yield return new WaitForSeconds(WaitTime);//+ WaitTime*0.5f);
        CanMove = true;
        //CanIteract = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Drop")
        {
            if (InventoryFull)
            {
                //collision.transform.GetComponent<Drop_scr>().Bounce(this.gameObject);
                collision.transform.GetComponent<CircleCollider2D>().isTrigger = true;
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
        if (collision.tag == "EnemyAttack"&& Vulnerable)
        {
            EnemyAttack_scr attack = collision.gameObject.GetComponent<EnemyAttack_scr>();
            TakeDamage(attack.Damage, attack.Strength, collision.transform);
            //Destroy(collision.gameObject);
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
            KnockBackFromEnAttack(point, strength);
        }
    }

    public void DealDamage(Drop_scr item)
    {
        if (CanMove)
        {
            int dmg = item.DMG;

        }
    }
    IEnumerator Imune()
    {
        Vulnerable = false;
        CanMove = false;
        yield return new WaitForSeconds(JMPImune);
        CanMove = true;
        yield return new WaitForSeconds(1 - JMPImune);
        Vulnerable = true;

    }
    IEnumerator JumpImune(float time)
    {
        Vulnerable = false;
        CanMove = false;
        CanJump = false;
        //Debug.Log("cant JUMP");
        yield return new WaitForSeconds(time);
        CanMove = true;
        Vulnerable = true;
        yield return new WaitForSeconds(0.5f);
        CanJump = true;
        //Debug.Log("CAN JUMP");
    }
    
    void KnockBackFromEnAttack(Transform point, float strength)
    {
        Vector3 dirrection = transform.position - point.position;
        rb.AddForce(dirrection.normalized * strength);
    }
    IEnumerator Regen()
    {
        if (regenerate)
        {
            regenerate = false;
            yield return new WaitForSeconds(1f);
            Health += RegenHP;
            Stamina += RegenSTM;
            regenerate = true;

        }
    }
}

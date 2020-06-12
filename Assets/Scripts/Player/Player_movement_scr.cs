using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_movement_scr : MonoBehaviour
{
    public int Completedlevels = 0;
    [HideInInspector] private float horizInput;
    [HideInInspector] private float verticInput;
    [HideInInspector] public Rigidbody2D rb;
    public bool Vulnerable = true;
    public bool CanMove = true;
    //public bool CanIteract = true;
    [HideInInspector] public bool alive = true;
    public Sprite Grave;
    public float JMPImune = 1f;
    private bool CanJump = true;
    public GameObject JumpShadow;
    public int JumpShadowAmoung;

    [HideInInspector] public string playerView = "none";
    public Sprite[] playerStates = new Sprite[3]; // стоячие положения игрока
    private SpriteRenderer PlayerSprite;
    public GameObject InHand;
    //private GameObject Cursor = null;
    //[HideInInspector] public int CurrInvSlot = 0;
    public GameObject WorkInd = null;
    private bool InventoryFull;
    private Inventory_scr Inventory = null;
    [HideInInspector] public Transform PlayerContainer;
    public GameObject AttackZone = null;
    public Transform Cursor;
    private bool regenerate = true;
    [HideInInspector] public bool wasHited = false;

    public int MaxHealth = 100;
    public int Health = 100;
    public int Stamina = 20;
    public int MaxStamina = 20;
    public int RegenHP = 0;
    public int RegenSTM = 1;
    public float speed = 3f; // сорость бега
    public float ShiftSpeed = 2f; // + скорость с зажатым LeftShift
    public float JumpStrengh = 3000f;
    public int JumpWaste = 5;
    public int Armor = 0;
    private AudioManager PlayerContainerAudio;
    private AudioSource PlayerContainerAudioSource;

    //private GameObject go1 = null;
    private AudioManager audioController;
    

    public Vector3 CameraOffset = new Vector3(0, 5, -30);

    private void Awake()
    {
        Cursor = GameObject.Find("Cursor").transform;
        PlayerContainer = GameObject.Find("PlayerContainer").transform;
        PlayerContainerAudio = PlayerContainer.GetComponent<AudioManager>();
        PlayerContainerAudioSource = PlayerContainer.GetComponent<AudioSource>();
        //StatsTab = GameObject.Find("StatsTexts").GetComponent<StatsTabUpdater>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //Cursor = GameObject.Find("Cursor");
        Inventory = GameObject.Find("InventoryManager").GetComponent<Inventory_scr>();
        InventoryFull = Inventory.isFull;
        //ChangeInHand(CurrInvSlot);
        PlayerSprite = this.gameObject.GetComponent<SpriteRenderer>();
        
        audioController = this.gameObject.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position = transform.position + CameraOffset;
        //InventoryFull = Inventory.GetComponent<Inventory_scr>().isFull;
        if (alive)
        {
            if (CanMove)
            {
                Movement(speed);
                Jump();
            }
            else
            {
                Movement(speed / 4);
            }
        }
    }
   
    private void Update()
    {
        if (alive)
        {
            if (Health < 0)
            {
                StartCoroutine(Death());
                //
                //this.gameObject.SetActive(false);

            }
            else if (regenerate)
            {
                StartCoroutine(Regen());
            }
        }
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
        if(horizInput!=0||verticInput!=0)
        {
            if (speed > 2&& PlayerContainerAudio.state != 2)
            {
                PlayerContainerAudioSource.Stop();
                PlayerContainerAudioSource.clip = PlayerContainerAudio.Mine[1];
                PlayerContainerAudioSource.Play();
                //PlayerContainerAudio.PlayAudioMine(1);
                PlayerContainerAudio.state = 2;
            }
            else if (speed < 2 && PlayerContainerAudio.state != 1)
            {
                PlayerContainerAudioSource.Stop();
                //PlayerContainerAudio.PlayAudioMine(0);
                PlayerContainerAudioSource.clip = PlayerContainerAudio.Mine[0];
                PlayerContainerAudioSource.Play();
                PlayerContainerAudio.state = 1;
            }
        }
        else if(PlayerContainerAudio.state != 0)
        {
            PlayerContainerAudioSource.Stop();
            PlayerContainerAudio.state = 0;
        }
        

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
    IEnumerator Death()
    {
        alive = false;
        audioController.PlayAudioDeath(0);
        GetComponent<SpriteRenderer>().sprite = Grave;
        yield return new WaitForSeconds(3f);
        GameObject.Find("DarkScreen").GetComponent<DarkScreen>().MakeDarker();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("Village");
        GetComponent<SpriteRenderer>().sprite = playerStates[0];
        Inventory.DeathLose();
        GameObject.Find("DarkScreen").GetComponent<DarkScreen>().MakeLighter();
        alive = true;

        //GetComponent<SpriteRenderer>().sprite = Grave;
    }

    void Jump()
    { if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            if (Stamina - JumpWaste > 0)
            {
                //Vector2 Move = new Vector2(JumpStrengh * horizInput, JumpStrengh * verticInput);
                Stamina -= JumpWaste;
                Vector2 f = new Vector2(horizInput, verticInput);
                this.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(f.normalized * JumpStrengh);
                StartCoroutine(JumpImune(JMPImune));
                audioController.PlayAudioAgred();
            }
            else
            {

                GetComponent<AudioSource>().Stop();
                audioController.PlayAudioAggroLost();
            }
            
            
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
        
    }

    public IEnumerator MouseHitAction(float WaitTime, Vector2 CurrDir)
    {
        StopCoroutine("MouseHitAction");
        CanMove = false;
        //Debug.Log("ghjfghf");
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
            audioController.PlayAudioHit();
            Health -= Mathf.Max(1, dmg - Armor);
            //wasHited = true;
            Debug.Log("You took dmg " + dmg + " Now Your health " + Health + " "+ wasHited);
            //wasHited = false;

        }
    }
    public void TakeDamage(int dmg, float strength, Transform point)
    {
        if (Vulnerable)
        {
            StartCoroutine(Imune());
            //
            Health -= Mathf.Max(1,dmg-Armor);
            Debug.Log("You took dmg " + dmg + " Now Your health " + Health + " " + wasHited);
            KnockBackFromEnAttack(point, strength);
            //
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
        wasHited = true;
        if (alive)
        {
            StartCoroutine(ColorDamage());
        }
        yield return new WaitForSeconds(JMPImune);
        CanMove = true;
        wasHited = false;
        yield return new WaitForSeconds(1 - JMPImune);
        Vulnerable = true;

    }
    IEnumerator ColorDamage()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("change color");
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator JumpImune(float time)
    {
        Vulnerable = false;
        CanMove = false;
        CanJump = false;
        //Debug.Log("cant JUMP");
        for (int i = 0; i < JumpShadowAmoung; i++)
        {
            Instantiate(JumpShadow, transform.position + new Vector3(0,0,0.05f), Quaternion.identity);
            yield return new WaitForSeconds(time/ JumpShadowAmoung);
        }
        //yield return new WaitForSeconds(time);
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
        
            regenerate = false;
            yield return new WaitForSeconds(1f);
            Health = Mathf.Min(Health + RegenHP,MaxHealth);
            Stamina = Mathf.Min(Stamina + RegenSTM, MaxStamina);
            regenerate = true;
        
    }

    public void EquipmentEffects(Drop_scr drop, int sign)
    {
        /*
    public int MaxHealth = 100;
    public int Health = 100;
    public int Stamina = 20;
    public int MaxStamina = 20;
    public int RegenHP = 0;
    public int RegenSTM = 1;
    public float speed = 3f; // сорость бега
    public float ShiftSpeed = 2f; // + скорость с зажатым LeftShift
    public float JumpStrengh = 3000f;
    public int JumpWaste = 5;
    public int Armor = 0;
        */
        MaxHealth += sign * drop.MaxHealth;
        Health += sign * drop.Health;
        Stamina += sign * drop.Stamina;
        MaxStamina += sign * drop.MaxStamina;
        RegenHP += sign * drop.RegenHP;
        RegenSTM += sign * drop.RegenSTM;
        speed += sign * drop.speed;
        JumpStrengh += sign * drop.JumpStrengh;
        JumpWaste += sign * drop.JumpWaste;
        Armor += sign * drop.Armor;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if (Stamina > MaxStamina)
        {
            Stamina = MaxStamina;
        }
    }
}

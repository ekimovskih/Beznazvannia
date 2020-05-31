using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_scr : MonoBehaviour
{
    public int Health = 100;
    [HideInInspector] private float horizInput;
    [HideInInspector] private float verticInput;
    private bool CanMove = true;
    public float speed=3f; // сорость бега
    public float ShiftSpeed = 2f; // + скорость с зажатым LeftShift
    [HideInInspector] public string playerView = "none";
    public Sprite[] playerStates = new Sprite[3]; // стоячие положения игрока
    private SpriteRenderer PlayerSprite;
    public GameObject InHand;
    public float ActionSpeed = 5f; // определяет перерыв перед совершением следующего действия (в секундах)
    private GameObject Cursor = null;
    [HideInInspector] public int CurrInvSlot = 0;
    public GameObject WorkInd = null;
    private bool InventoryFull;
    private GameObject Inventory = null;
    void Start()
    {
        Cursor = GameObject.Find("Cursor");
        Inventory = GameObject.Find("InventoryManager");
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
    }
    public void Movement()
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
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);

        
            SpriteMoveChanger(horizInput, verticInput);
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
                Inventory.GetComponent<Inventory_scr>().AddItem(collision.gameObject);
            }
        }
    }
    /*
    public void ChangeInHand(int slot)
    {
        InHand = WorkInd.GetComponent<WorkIndicator_scr>().FastSlots[slot-1];
        Cursor.GetComponent<Cursor_scr>().ChangeInHandItem(InHand);
        WorkInd.GetComponent<SpriteRenderer>().sprite = InHand.GetComponent<SpriteRenderer>().sprite;
    }
    */
    /*
    void InventoryAction()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrInvSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrInvSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrInvSlot = 3;
        }
        ChangeInHand(CurrInvSlot);
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            int dmg = collision.gameObject.GetComponent<EnemyAttack_scr>().Damage; // оставил если придетя визуализировать урон
            Health -= dmg;
            //Debug.Log("You took dmg " + dmg + " Now Your health " + Health);
        }
    }
}

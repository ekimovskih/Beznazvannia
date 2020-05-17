using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_scr : MonoBehaviour
{
    private float horizInput;
    private float verticInput;
    public float speed=3f;
    public float ShiftSpeed = 2f; 
    [HideInInspector] public string playerView = "none";
    public Sprite[] playerStates = new Sprite[3];
    private SpriteRenderer PlayerSprite;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    public void Movement()
    {
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

        SpriteChanger();

    }
    public void SpriteChanger()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Drop")
        {
            collision.transform.GetComponent<Drop_scr>().Bounce(this.gameObject);
        }
    }
}

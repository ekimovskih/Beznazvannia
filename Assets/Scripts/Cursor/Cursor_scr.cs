using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_scr : MonoBehaviour
{
    // Start is called before the first frame update
    //private GameObject UI = null;
    public SpriteRenderer CursorSprite;
    public Sprite[] SpritesArray = new Sprite[1];
    void Start()
    {
        Cursor.visible = false;
        CursorSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"),0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cursor_scr : MonoBehaviour
{
    // Start is called before the first frame update
    //private GameObject UI = null;
    public SpriteRenderer CursorSprite;
    public Sprite[] SpritesArray = new Sprite[1];
    private Vector2 cursorpos;
    private Vector2 currentCell;
    public GameObject cell = null;
    [HideInInspector] public bool cellActive = true;
    public GameObject testobject = null;

    void Start()
    {
        Cursor.visible = false;
        CursorSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        cursorpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorpos;
        currentCell = new Vector2(ChoseCell(transform.position.x),ChoseCell(-transform.position.y));

        if (cellActive)
        {
            cell.transform.position = currentCell*(new Vector2(1,-1));
        }


        PutTreeInCell();
    }

    int ChoseCell(float axis)
    {
        axis = Mathf.Clamp(axis, 0, 100000);
        return System.Convert.ToInt32(axis - axis % 1);
    }

    void PutTreeInCell()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("alalala");
            Instantiate(testobject, new Vector3(currentCell.x+0.5f, -currentCell.y-0.5f, -currentCell.y), Quaternion.identity);
        }
    }
}

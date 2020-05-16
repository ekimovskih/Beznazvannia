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
    //public float currentCellDistance = 1.5f;
    public GameObject cell = null;
    [HideInInspector] public bool cellActive = true;
    //public GameObject Player = null;
    public GameObject testobject = null;
    public GameObject testobject2 = null;

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

        ShowCurrentCell();


        PutTreeInCell();
        MineFromCell();
    }

    void ShowCurrentCell()
    {
        //cellActive = ToPlayerDistance();
        if (cellActive)
        {
            cell.transform.GetComponent<SpriteRenderer>().enabled = cellActive;
            cell.transform.position = currentCell * (new Vector2(1, -1));
        }
        else
        {
            cell.transform.GetComponent<SpriteRenderer>().enabled = cellActive;
        }
    }

    int ChoseCell(float axis)
    {
        axis = Mathf.Clamp(axis, 0, 100000);
        return System.Convert.ToInt32(axis - axis % 1);
    }

    void PutTreeInCell()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("alalala");
            testobject2.transform.GetComponent<GridBuilder_scr>().PutOnCell(testobject, ChoseCell(transform.position.x), ChoseCell(-transform.position.y));
        }
    }

    void MineFromCell()
    {
        if (Input.GetMouseButtonDown(0))
        {
            testobject2.transform.GetComponent<GridBuilder_scr>().GetFromCell(ChoseCell(transform.position.x), ChoseCell(-transform.position.y));
        }
    }
}

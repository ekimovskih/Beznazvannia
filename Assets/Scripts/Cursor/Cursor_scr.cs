using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cursor_scr : MonoBehaviour
{
    public SpriteRenderer CursorSprite;
    public Sprite[] SpritesArray = new Sprite[1];
    private Vector2 cursorpos;
    private Vector2Int currentCell;
    private Vector2Int ActiveCell;
    public GameObject cell = null;
    private bool cellActive = true;
    private GameObject Player = null;

    private Vector2Int PlayerCell; // клетка с игроком
    public GameObject InHand;
    private float ActionPossible;

    public GameObject testobject = null;
    public GameObject testobject2 = null;
    public GameObject testobject3 = null;
    
    void Start()
    {
        ActionPossible = 0; //возможно возникновение БАГОВ при иге более 1000к часов подряд
        //cell.transform.GetComponent<SpriteRenderer>().enabled = false;
        Player = GameObject.Find("Player");
        //ChangeInHandType(Player.GetComponent<Player_movement_scr>().InHand);
        Cursor.visible = false;
        CursorSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        cursorpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorpos;
        currentCell = new Vector2Int(ChoseCell(transform.position.x),ChoseCell(-transform.position.y));

        //ShowActiveCell();
        ShowCurrentCell();

        PutTreeInCell();
        InUseArea();
        if (ActionPossible<Time.time)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IteracteWithCell();

            }
        }
    }

    void ShowActiveCell()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cellActive = true;
            //cellActive = true;
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            cellActive = false;
        }
    }
    void InUseArea()
    {
        Vector3 other = Player.transform.position;
        PlayerCell = new Vector2Int(ChoseCell(other.x), ChoseCell(-other.y));
        if (Mathf.Abs(currentCell.x- PlayerCell.x)<=1&&(Mathf.Abs(currentCell.y - PlayerCell.y) <= 1))
        {
            ActiveCell = currentCell;
        }
        else
        {
            Vector3 self = this.gameObject.transform.position;
            float x = self.x-other.x;
            float y = -self.y+other.y;
            if (Mathf.Abs(x)> Mathf.Abs(y))
            {
                ActiveCell = PlayerCell + new Vector2Int(Math.Sign(x), 0);
            }
            else
            {
                ActiveCell = PlayerCell + new Vector2Int(0, Math.Sign(y));
            }
        }
    } 
    void ShowCurrentCell()
    {
        //cellActive = ToPlayerDistance();
        if (cellActive)
        {
            cell.transform.GetComponent<SpriteRenderer>().enabled = cellActive;
            cell.transform.position = ActiveCell * (new Vector2(1, -1));
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
            testobject2.transform.GetComponent<GridBuilder_scr>().PutOnCell(testobject, ChoseCell(transform.position.x), ChoseCell(-transform.position.y));
        }
        if (Input.GetMouseButtonDown(2))
        {
            testobject2.transform.GetComponent<GridBuilder_scr>().PutOnCell(testobject3, ChoseCell(transform.position.x), ChoseCell(-transform.position.y));
        }
    }

    void IteracteWithCell()
    {
        testobject2.transform.GetComponent<GridBuilder_scr>().GetFromCell(ActiveCell.x, ActiveCell.y, InHand);
        //Debug.Log(Time.time + " Start");
        float WaitTime = Player.GetComponent<Player_movement_scr>().ActionSpeed;
        ActionPossible = Time.time + WaitTime;
        //Debug.Log(ActionPossible + " Needed");
        StartCoroutine(ShowWork(WaitTime));

        //Vector3 plpos = Player.transform.position;
        //Vector2 PlayerDir = new Vector2(plpos.x , plpos.y) * new Vector2(0, -1);
        Vector2 PlayerDir = ActiveCell - PlayerCell;
        
        StartCoroutine(Player.GetComponent<Player_movement_scr>().MouseHitAction(WaitTime, PlayerDir));
    }

    IEnumerator ShowWork(float WaitTime)
    {
        GameObject obj = Player.GetComponent<Player_movement_scr>().WorkInd;
        obj.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(WaitTime);
        obj.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ChangeInHandType(GameObject Hand)
    {
        InHand = Hand;
    }
}

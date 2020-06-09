using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;



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
    private Player_movement_scr PlayerComponent;
    private DropCatalog_scr DropCatalog;

    private Vector2Int PlayerCell; // клетка с игроком
    
    public GameObject InHandIndicator = null;
    private float ActionPossible;

    //public GameObject testobject = null;
    public GameObject GridBuilder = null;
    //public GameObject testobject3 = null;
    //public Drop_scr EmptyHand;
    public Drop_scr InActiveSlot;
    //private bool InvOpen = false;
    public int HandContainer;
    public int HandContainerCount;
    public bool HandContainerFull = false;



    private void Awake()
    {
        Player = GameObject.Find("Player");
        DropCatalog = GameObject.Find("DropCatalog").GetComponent<DropCatalog_scr>();
        PlayerComponent = Player.GetComponent<Player_movement_scr>();
        //InHandIndicator = GameObject.Find("WorkIndicator");
    }
    void Start()
    {
        ActionPossible = 0; //возможно возникновение БАГОВ при игhе более 1000к часов подряд
        //Cursor.visible = false;
        CursorSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        InHandIndicator.transform.position = Input.mousePosition;
        cursorpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorpos;
        //transform.position = Input.mousePosition; //3333333333333
        //// + OffsetInHandIndicator;//3333333333333
        currentCell = new Vector2Int(ChoseCell(transform.position.x),ChoseCell(transform.position.y));

        //ShowActiveCell();
        ShowCurrentCell();
        //PutTreeInCell();
        InUseArea();
        if (ActionPossible<Time.time)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseLMBaction();

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
        PlayerCell = new Vector2Int(ChoseCell(other.x), ChoseCell(other.y));
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
                ActiveCell = PlayerCell - new Vector2Int(0, Math.Sign(y));
            }
        }
    } 
    void ShowCurrentCell()
    {
        //cellActive = ToPlayerDistance();
        if (cellActive)
        {
            cell.transform.GetComponent<SpriteRenderer>().enabled = cellActive;
            cell.transform.position = (new Vector3(ActiveCell.x, ActiveCell.y,0));
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
    /*
    void PutTreeInCell() //НЕТ НЕЕЕЕЕЕЕУДООООООООЛЯЯЙЙЙЙЙЙ!!!!!!!!!!!!!!!!!!!!!! ААААААААААААААААААААААААААААААААААААА!
    {
        if (Input.GetMouseButtonDown(1))
        {
            testobject2.transform.GetComponent<GridBuilder_scr>().PutOnCell(testobject, currentCell.x, currentCell.y);
        }
        if (Input.GetMouseButtonDown(2))
        {
            testobject2.transform.GetComponent<GridBuilder_scr>().PutOnCell(testobject3, currentCell.x, currentCell.y);
        }
    }*/

    void MouseLMBaction()
    {
        if (!MouseOverUi())
        {
            if (InActiveSlot != null && InActiveSlot.Interactive && PlayerComponent.CanMove && !HandContainerFull)
            {

                float WaitTime = InActiveSlot.ActionSpeed;
                ActionPossible = Time.time + WaitTime;
                if (InActiveSlot.type == "Weapon")
                {
                    Instantiate(InActiveSlot.AttackZone, Player.transform.position, Quaternion.identity);
                    ShowWork();
                    return;
                }
                else
                {
                    GridBuilder.transform.GetComponent<GridBuilder_scr>().GetFromCell(ActiveCell.x, ActiveCell.y, InActiveSlot);
                    //Debug.Log(Time.time + " Start");
                    //float WaitTime = InHand.ActionSpeed;

                    //Debug.Log(ActionPossible + " Needed");

                }
                StartCoroutine(ShowWork(WaitTime));

                //Vector3 plpos = Player.transform.position;
                //Vector2 PlayerDir = new Vector2(plpos.x , plpos.y) * new Vector2(0, -1);
                Vector2 PlayerDir = ActiveCell - PlayerCell;
                StartCoroutine(PlayerComponent.MouseHitAction(WaitTime, PlayerDir));
                return;
            }
            else if (HandContainerFull)
            {
                DropItemInHand();
            }
        }
        //Debug.Log("U CANT");
    }

    public void DropItemInHand()
    {
        Vector3 newDirr = (transform.position - Player.transform.position).normalized;
        //Debug.Log(HandContainer);
        GameObject newDrop = Instantiate(DropCatalog.GetGObyID(HandContainer), Player.transform.position + newDirr * 5f, Quaternion.identity);
        CursorContainerActivation();
        //Vector3 newDirr = transform.position - Player.transform.position;
        newDrop.GetComponent<Rigidbody2D>().AddForce(newDirr * 1f);
    }
    public void DropItemInHand(GameObject trash)
    {
        Vector3 newDirr = (transform.position - Player.transform.position).normalized;
        Debug.Log(trash);
        GameObject newDrop = Instantiate(DropCatalog.GetGObyID(trash.GetComponent<Drop_scr>().id), Player.transform.position + newDirr * 5f, Quaternion.identity);
        CursorContainerActivation();
        //Vector3 newDirr = transform.position - Player.transform.position;
        newDrop.GetComponent<Rigidbody2D>().AddForce(newDirr * 1f);
    }

    IEnumerator ShowWork(float WaitTime)
    {
        GameObject obj = PlayerComponent.WorkInd;
        obj.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(WaitTime);
        obj.GetComponent<SpriteRenderer>().enabled = false;
    }
    void ShowWork()
    {
        PlayerComponent.WorkInd.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ChangeInHandItem(Drop_scr Hand)
    {
        InActiveSlot = Hand;
    }
    /*
    public void CursorContainerActivation(Sprite spr,bool active)
    {
        InHandIndicator.SetActive(active);
        InHandIndicator.GetComponent<Image>().sprite = spr;
    }
    */
    public void CursorContainerActivation()
    {
        if (InHandIndicator != null)
        {
            InHandIndicator.SetActive(false);
            InHandIndicator.GetComponent<Image>().sprite = null;
        }
        HandContainer = 0;
        HandContainerCount = 0;
        HandContainerFull = false;
        //Debug.Log("Hand Empty");
        //return false;

    }
    public void CursorContainerActivation(GameObject item, int count)
    {
        InActiveSlot = item.GetComponent<Drop_scr>();
        InHandIndicator.SetActive(true);
        InHandIndicator.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        HandContainer = InActiveSlot.id;
        HandContainerCount = count;
        HandContainerFull = true;
        //return true;
    }
    public void CursorContainerActivation(int count)
    {
        HandContainerCount += count;
    }


    private bool MouseOverUi()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResList);
        for (int i = 0; i < raycastResList.Count; i++)
        {
            if (raycastResList[i].gameObject.tag != "UI")
            {
                raycastResList.RemoveAt(i);
                i--;
            }
        }
        return raycastResList.Count > 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlot_scr : MonoBehaviour
{
    //public GameObject Inventory = null;
    //private Inventory_scr InventoryComponent;
    //public GameObject InvSlot;
    //private bool Empty = false;
    public string SlotType = null;
    public GameObject Default;
    public GameObject Active;
    //public GameObject Active = null 
    public bool busy = false;
    private GameObject Cursor;
    private Cursor_scr CursorComponent;
    public Image sprite;
    public GameObject inSlot;
    DropCatalog_scr DropCatalog;
    Player_movement_scr player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor = GameObject.Find("Cursor");
        player = GameObject.Find("Player").GetComponent<Player_movement_scr>();
        CursorComponent = Cursor.GetComponent<Cursor_scr>();
        DropCatalog = GameObject.Find("DropCatalog").GetComponent<DropCatalog_scr>();
        //sprite = Active.GetComponent<Image>();
        if (inSlot == null)
        {
            busy = false;
            Active.SetActive(false);
            Default.SetActive(true);
        }
        else
        {
            busy = true;
            Active.SetActive(true);
            Default.SetActive(false);
        }
    }

    // Update is called once per frame
    void SetSlot() //отчистить слот
    {
        Active.SetActive(false);
        Default.SetActive(true);
        busy = false;
        inSlot = null;
    }
    void SetSlot(GameObject item) // положить в слот
    {
        inSlot = item;
        sprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
        Active.SetActive(true);
        Default.SetActive(false);
        busy = true;
    }
    public void IteractWithSlot()
    {
        Debug.Log("Click");
        if (CursorComponent.HandContainerFull) // если в руках есть some
        {
            int id = CursorComponent.HandContainer;
            GameObject FreeHand = DropCatalog.GetGObyID(id);
            Drop_scr drop = FreeHand.GetComponent<Drop_scr>();
            Debug.Log(drop.type.Equals(SlotType));
            if (drop.type.Equals(SlotType))
            {
                if (busy) //клетка занята
                {
                    SlotBenifits(-1);
                    CursorComponent.CursorContainerActivation(inSlot, 1);
                    SetSlot(FreeHand);
                    SlotBenifits(1);
                    //
                }
                else //положить в пустую
                {
                    SetSlot(FreeHand);
                    SlotBenifits(1);
                    CursorComponent.CursorContainerActivation();
                }
            }
            else
            {
                Debug.Log("Этот предмет не подходит в этот слот");
            }
            
           // InventoryComponent.PutItem(SlotNumber);
        }
        else if(busy) // руки пустые, но в клетке предмет
        {
            CursorComponent.CursorContainerActivation(inSlot, 1);
            SlotBenifits(-1);
            SetSlot();
        }
    }

    void SlotBenifits(int sign)
    {
        Drop_scr drop = inSlot.GetComponent<Drop_scr>();
        player.EquipmentEffects(drop, sign);
    }
}

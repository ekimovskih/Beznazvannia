using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlot_scr : MonoBehaviour
{
    public GameObject Inventory = null;
    private Inventory_scr InventoryComponent;
    public GameObject InvSlot;
    //private bool Empty = false;
    public Sprite Default;
    public Sprite Active;
    //public GameObject Active = null 
    public bool busy = false;
    private GameObject Cursor;
    private Cursor_scr CursorComponent;
    public Image sprite;
    // Start is called before the first frame update
    void Start()
    {
        Cursor = GameObject.Find("Cursor");
        CursorComponent = Cursor.GetComponent<Cursor_scr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IteractWithSlot()
    {
        if (CursorComponent.HandContainerFull)
        {
            InventoryComponent.PutItem(SlotNumber);
        }
        else
        {
            InventoryComponent.TakeItem(SlotNumber);
        }
    }
}

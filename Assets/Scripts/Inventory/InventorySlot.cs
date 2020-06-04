using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Inventory = null;
    private Inventory_scr InventoryComponent;
    public GameObject InvSlot;
    public GameObject Count;
    public int SlotNumber;
    public bool SlotActive = false;
    //private bool Empty = false;
    public Sprite Default;
    public Sprite Active;
    //public GameObject Active = null 
    public bool busy = false;
    private GameObject Cursor;
    private Cursor_scr CursorComponent;
    public Text text;
    public Image sprite;
    private void Awake()
    {
        //InvSlot = this.gameObject.transform.GetChild(0).gameObject;
        //Count = InvSlot.gameObject.transform.GetChild(0).gameObject;
        Cursor = GameObject.Find("Cursor");
        CursorComponent = Cursor.GetComponent<Cursor_scr>();
        //text = Count.GetComponent<Text>();
        //sprite = InvSlot.GetComponent<Image>();

    }
    void Start()
    {
        InventoryComponent = Inventory.GetComponent<Inventory_scr>();
        //Inventory = GameObject.Find("Inventory");
    }
    public void SlotCreation(bool Activation)
    {
        this.gameObject.SetActive(Activation);
    }

    public void SlotActivation(bool Activation)
    {
        //Debug.Log(text.text + SlotNumber);
        if (!Activation)
        {
            //InventoryComponent.ItemsCount[SlotNumber] = 0;
            text.text = "";
            //Debug.Log(text.text + SlotNumber);
        }
        InvSlot.SetActive(Activation);
    }
    public void SetCount(int count)
    {
        if (count == 1)
        {
            text.text = "";
        }
        else
        {
            text.text = count.ToString();
        }
    }
    public void SetCount(int count, GameObject item)
    {
        sprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
        if (count == 1)
        {
            text.text = "";
        }
        else
        {
            text.text = count.ToString();
        }
        InvSlot.SetActive(true);
    }

    public void IteractWithSlot()
    {
        if (Inventory.activeSelf)
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
        else
        {
            InventoryComponent.ActivateSlot(SlotNumber);
        }
    }
    public void ActivateSlot(bool act)
    {
        if (!act)
        {
            SlotActive = false;
            this.gameObject.GetComponent<Image>().sprite = Default;
        }
        else
        {
            SlotActive = true;
            this.gameObject.GetComponent<Image>().sprite = Active;
        }
    }
}

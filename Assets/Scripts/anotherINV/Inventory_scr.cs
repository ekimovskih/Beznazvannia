using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public bool open = false;
    public bool isFull = false;
    public GameObject[] InvItems = new GameObject[48];
    public InventorySlot[] InvSlots;
    public int[] ItemsCount = new int[48];
    private int InvItemsLength;
    private int firstemptycell;
    private GameObject player;
    private GameObject cursor;
    [HideInInspector] public GameObject DropCatalog;
    public GameObject IVTR; //инвентарь в канвасе
    [HideInInspector] public int ActiveSlot = -1;


    public int inHand;
    public bool SmthInHand = false;
    public int inHandCount;
    //private int PreviousSlot;
    private Cursor_scr InHandInd;
    //aaaa blyat'
    private int inHand2;
    private int inHandCount2;

    public InventorySlot[] CraftSlots;
    public GameObject[] CraftItems = new GameObject[4];
    public int[] CraftCounts = new int[4];
    public GameObject CraftTab;


    private void Awake()
    {
        IVTR.SetActive(true);
        CraftTab.SetActive(true);

        cursor = GameObject.Find("Cursor");
        InHandInd = cursor.GetComponent<Cursor_scr>();
        player = GameObject.Find("Player");
        //IVTR = GameObject.Find("Inventory");
        InventorySlot[] FS = GameObject.Find("FastPanel").GetComponentsInChildren<InventorySlot>();
        InventorySlot[] IS = IVTR.GetComponentsInChildren<InventorySlot>();
        InventorySlot[] CS = CraftTab.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < 12; i++)
        {
            InvSlots[i] = FS[i];
            InvSlots[i].SlotNumber = i;
            InvSlots[i].Inventory = this.gameObject;
        }
        for (int i = 12; i < 48; i++)
        {
            InvSlots[i] = IS[i-12];
            InvSlots[i].SlotNumber = i;
            InvSlots[i].Inventory = this.gameObject;
        }
        for (int i = 0; i < 4; i++)
        {
            CraftSlots[i] = CS[i];
            CraftSlots[i].SlotNumber = i+50;
            CraftSlots[i].Inventory = this.gameObject;
        }


        ActivateSlot(0);
    }
    void Start()
    {
        IVTR.SetActive(false);
        CraftTab.SetActive(false);
        DropCatalog = GameObject.Find("DropCatalog");
        player = GameObject.Find("Player");
        InvItemsLength = InvItems.Length;
        ShowStartInv();
        IsInventoryFull();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IVTR.SetActive(!IVTR.activeSelf);
            CraftTab.SetActive(!CraftTab.activeSelf);
        }
        SwitchActiveSlot();
    }

    void IsInventoryFull()
    {
        bool FCNotFound = true;
        for (int i = 0; i < InvItemsLength; i++)
        {
            if (InvItems[i]==null&& FCNotFound)
            {
                FCNotFound = false;
                firstemptycell = i;
                InvSlots[i].SlotActivation(false);
            }
            else if (InvItems[i] == null)
            {
                //Debug.Log(InvItems[i]);
                InvSlots[i].SlotActivation(false);
            }
        }
        isFull = FCNotFound;
        //player.
        //Debug.Log("Inventory is full" + isFull);
        return;
    }
    public void AddItem(GameObject item)
    {
        Drop_scr drop = item.GetComponent<Drop_scr>();
        int count = drop.count;
        int id = drop.id;
        for (int i = 0; i < InvItemsLength; i++)
        {
            GameObject iItem= InvItems[i];
            if (iItem != null && iItem.GetComponent<Drop_scr>().id.Equals(id))
            {
                int istack = iItem.GetComponent<Drop_scr>().InStack;
                if (ItemsCount[i] + count <= istack)
                {
                    ItemsCount[i] += count;
                    drop.IsEmpty();
                    InvSlots[i].SetCount(ItemsCount[i]);
                    return;
                }
                else
                {
                    drop.count = ItemsCount[i] + count - istack;
                    ItemsCount[i] = istack;
                }
            }
        }
        if (!isFull)
        {
            InvItems[firstemptycell] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(id);
            ItemsCount[firstemptycell] = count;
            InvSlots[firstemptycell].SetCount(ItemsCount[firstemptycell], InvItems[firstemptycell]);
            drop.IsEmpty();
            IsInventoryFull();
        }
    }

    public void TakeItem(int slot)
    {
        if (InvItems[slot] != null && IVTR.activeSelf==true)
        {
            inHand = InvItems[slot].GetComponent<Drop_scr>().id;
            inHandCount = ItemsCount[slot];
            InHandInd.IHindicatorActivity(InvItems[slot].GetComponent<SpriteRenderer>().sprite,true);
            InvSlots[slot].SlotActivation(false);
            ItemsCount[slot] = 0;
            InvItems[slot] = null;
            SmthInHand = true;
            //PreviousSlot = slot;
            IsInventoryFull();
        }
    }

    public void PutItem(int slot)
    {
        //Debug.Log(IVTR);
        if (IVTR.activeSelf == true)
        {
            GameObject iItem;
            int[] iItemCount;
            if (slot < 50)
            {
                iItem = InvItems[slot];
            }
            else
            {
                iItem = CraftItems[slot - 50];
            }
            if (iItem != null)
            {
                int istack = iItem.GetComponent<Drop_scr>().InStack;
                if (iItem.GetComponent<Drop_scr>().id == inHand && ItemsCount[slot] + inHandCount <= istack)
                {
                    
                        ItemsCount[slot] += inHandCount;
                        //drop.IsEmpty();
                        InHandInd.IHindicatorActivity(null,false);
                        SmthInHand = false;
                        InvSlots[slot].SetCount(ItemsCount[slot]);
                        //IsInventoryFull();
                        //Debug.Log("AZAZAZAZA");
                        InvSlots[slot].SetCount(inHandCount, InvItems[slot]);
                    /*
                    else
                    {
                        inHandCount = ItemsCount[slot] + inHandCount - istack;
                        ItemsCount[slot] = istack;
                        InvSlots[slot].SetCount(inHandCount);
                        Debug.Log(inHandCount);
                    }*/
                }
                else
                {
                    InHandInd.IHindicatorActivity(InvItems[slot].GetComponent<SpriteRenderer>().sprite,true);
                    //InHandInd.IHindicatorActivity(InvItems[slot].GetComponent<Image>().sprite);
                    inHand2 = InvItems[slot].GetComponent<Drop_scr>().id;
                    inHandCount2 = ItemsCount[slot];
                    InvItems[slot] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(inHand);
                    ItemsCount[slot] = inHandCount;
                    InvSlots[slot].SetCount(inHandCount, InvItems[slot]);

                    inHand = inHand2;
                    inHandCount = inHandCount2;
                    //IsInventoryFull();
                    //return;
                }           
            }
            else
            {
                InHandInd.IHindicatorActivity(null,false);
                InvItems[slot] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(inHand);
                ItemsCount[slot] = inHandCount;
                InvSlots[slot].SetCount(inHandCount, InvItems[slot]);
                SmthInHand = false;
            }
            IsInventoryFull();
        }
    }

    public void ActivateSlot(int slot)
    {
        if(slot != ActiveSlot && ActiveSlot>=0)
        {
            InvSlots[ActiveSlot].ActivateSlot(false);
        }
        InvSlots[slot].ActivateSlot(true);
        ActiveSlot = slot;
        ChangeInHandItem();
        //Debug.Log(slot);
    }
    void SwitchActiveSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSlot(0);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSlot(1);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateSlot(2);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateSlot(3);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivateSlot(4);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ActivateSlot(5);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ActivateSlot(6);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ActivateSlot(7);
           
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ActivateSlot(8);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ActivateSlot(9);
            
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            ActivateSlot(10);
            
        }
        else if (Input.GetKeyDown("="))
        {
            ActivateSlot(11);
            
        }
        int wheel = System.Convert.ToInt32(Math.Sign(Input.GetAxis("Mouse ScrollWheel")));
        if (wheel != 0)
        {
            int support = ActiveSlot + wheel;
            if (support < 0)
            {
                support = 11;
            }
            if (support > 11)
            {
                support = 0;
            }
            ActivateSlot(support);
        }
    }
    void ChangeInHandItem()
    {
        GameObject item = InvItems[ActiveSlot];
        if (item != null)
        {
            cursor.GetComponent<Cursor_scr>().ChangeInHandItem(InvItems[ActiveSlot].GetComponent<Drop_scr>());
        }
        else
        {
            cursor.GetComponent<Cursor_scr>().ChangeInHandItem(null);
        }
    }
    void ShowStartInv()
    {
        for (int i = 0; i < 48; i++)
        {
            if (InvItems[i] != null)
            {
                //Debug.Log(i);
                ItemsCount[i] = InvItems[i].GetComponent<Drop_scr>().count;
                InvSlots[i].SetCount(ItemsCount[i], InvItems[i]);
            }
        }
    }
}

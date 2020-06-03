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
    public GameObject[]     InvItems = new GameObject[48];
    public InventorySlot[]  InvSlots;
    public int[]            ItemsCount = new int[48];
    private int InvItemsLength;
    private int firstemptycell;
    private GameObject player;
    private GameObject cursor;
    [HideInInspector] public GameObject DropCatalog;
    public GameObject IVTR; //инвентарь в канвасе
    [HideInInspector] public int ActiveSlot = -1;

    //public bool SmthnHand = false;

    private Cursor_scr Cursor;

    public int CraftPanelSize = 3;
    public InventorySlot[]  CraftSlots;
    public GameObject[]     CraftItems = new GameObject[4];
    public int[]            CraftCounts = new int[4];
    public GameObject CraftTab;
    public GameObject ResultSlots;


    private void Awake()
    {
        IVTR.SetActive(true);
        CraftTab.SetActive(true);

        cursor = GameObject.Find("Cursor");
        Cursor = cursor.GetComponent<Cursor_scr>();
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
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IVTR.SetActive(!IVTR.activeSelf);
            CraftTab.SetActive(!CraftTab.activeSelf);
            ResultSlots.SetActive(CraftTab.activeSelf);

            UpdateInventory();
        }
        SwitchActiveSlot();
    }

    void UpdateInventory()
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
            else
            {
                if (ItemsCount[i] < 1)
                {
                    InvSlots[i].SlotActivation(false);
                    InvItems[i] = null;
                }
                else
                {
                    InvSlots[i].SetCount(ItemsCount[i]);
                }
            }

            if (i< CraftPanelSize)
            {
                if (CraftItems[i] == null)
                {
                    CraftSlots[i].SlotActivation(false);
                    CraftSlots[i].SlotCreation(true);
                }
                else
                {
                    if (CraftCounts[i] < 1)
                    {
                        CraftSlots[i].SlotActivation(false);
                        CraftItems[i] = null;
                    }
                    else
                    {
                        CraftSlots[i].SetCount(CraftCounts[i]);
                    }
                }
                
            }
            else if (i>= CraftPanelSize && i < 4)
            {
                CraftSlots[i].SlotCreation(false);
            }
        }
        isFull = FCNotFound;
        //player.
        //Debug.Log("Inventory is full" + isFull);
        //return;
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
            UpdateInventory();
        }
    }

    public void TakeItem(Drop_scr drop)
    {
        GameObject[] component = drop.Recipe;
        int[] componentCount = drop.RecipeCount;
        for (int i=0; i < drop.Recipe.Length;i++)
        {
            int id = component[i].GetComponent<Drop_scr>().id;
            for (int k = 0; k < CraftPanelSize; k++)
            {

                if (CraftItems[k]!=null && id == CraftItems[k].GetComponent<Drop_scr>().id)
                {
                    CraftCounts[k] -= componentCount[i];
                    break;
                }
            }                
        }
        //SmthInHand = true;
        CraftSlotsCheker();
        UpdateInventory();
    }

    public void TakeItem(int slot)
    {
        GameObject[] Items = null;
        int[] Counts = null;
        InventorySlot[] Slots = null;
        if (slot < 50)
        {
            Items = InvItems;
            Counts = ItemsCount;
            Slots = InvSlots;
        }
        else if (slot < 60)
        {
            Items = CraftItems;
            Counts = CraftCounts;
            Slots = CraftSlots;
            //CraftSlotsCheker();
            slot -= 50;
        }
        if (Items[slot] != null && IVTR.activeSelf == true)
        {
            Cursor.CursorContainerActivation(Items[slot], Counts[slot]);
            //SmthInHand = true;
            Slots[slot].SlotActivation(false);
            Counts[slot] = 0;
            Items[slot] = null;
            UpdateInventory();
            ChangeInHandItem();
            CraftSlotsCheker();
        }
    }

    public void PutItem(int slot)
    {
        GameObject[] Items = null;
        int[] Counts = null;
        InventorySlot[] Slots = null;
        if (slot < 50)
        {
            Items = InvItems;
            Counts = ItemsCount;
            Slots = InvSlots;
        }
        else if (slot < 60)
        {
            Items = CraftItems;
            Counts = CraftCounts;
            Slots = CraftSlots;
            slot -= 50;
        }

        if (IVTR.activeSelf == true)
        {
            GameObject Item = Items[slot];
            int HandID = cursor.GetComponent<Cursor_scr>().HandContainer;

            int Count = Counts[slot];
            int HandCount = cursor.GetComponent<Cursor_scr>().HandContainerCount;

            

            if (Item != null)
            {
                int ItemID = Item.GetComponent<Drop_scr>().id;
                int istack = Item.GetComponent<Drop_scr>().InStack;
                if (ItemID == HandID)
                {
                    if (Counts[slot] + HandCount <= istack)
                    {
                        Counts[slot] += HandCount;
                        //SmthInHand = false;
                        Cursor.CursorContainerActivation();
                        Slots[slot].SetCount(Counts[slot]);
                        //Debug.Log("Fit");
                    }
                    else if (Counts[slot] == istack)
                    {
                        if (HandCount< istack)
                        {
                            Counts[slot] = HandCount;
                            Cursor.CursorContainerActivation(istack - HandCount);
                        }
                    }
                    else
                    {
                        Cursor.CursorContainerActivation(Counts[slot] - istack);
                        Counts[slot] = istack;
                        //SmthInHand = true;
                        Slots[slot].SetCount(Counts[slot]);
                        //Debug.Log("Dont Fit");
                    }
                }
                else
                {
                    GameObject SwitchHandContainer = Items[slot];
                    int SwitchHandCount = Counts[slot];

                    Items[slot] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(Cursor.HandContainer);
                    Counts[slot] = Cursor.HandContainerCount;
                    Slots[slot].SetCount(Counts[slot], Items[slot]);

                    //SmthInHand = true;
                    Cursor.CursorContainerActivation(SwitchHandContainer, SwitchHandCount);
                }           
            }
            else
            {
                Items[slot] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(Cursor.HandContainer);
                int stack = Items[slot].GetComponent<Drop_scr>().InStack;
                if (Cursor.HandContainerCount > stack)
                {
                    Counts[slot] = stack;
                    //Cursor.HandContainerCount -= stack;
                    Cursor.CursorContainerActivation(- stack);
                    Slots[slot].SetCount(Counts[slot], Items[slot]);
                }
                else
                {
                    Counts[slot] = Cursor.HandContainerCount;
                    Slots[slot].SetCount(Counts[slot], Items[slot]);
                    //SmthInHand = false;
                    Cursor.CursorContainerActivation();
                }
            }
            UpdateInventory();
            CraftSlotsCheker();
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
            cursor.GetComponent<Cursor_scr>().ChangeInHandItem(item.GetComponent<Drop_scr>());
            player.GetComponent<Player_movement_scr>().WorkInd.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            cursor.GetComponent<Cursor_scr>().ChangeInHandItem(null);
            player.GetComponent<Player_movement_scr>().WorkInd.GetComponent<SpriteRenderer>().sprite = null;
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

    public void CraftSlotsCheker()
    {
        //Debug.Log("start CraftSlotsCheker()");
        for (int i = 0; i < 4; i++)
        {
            if (CraftItems[i] != null)
            {
                //Debug.Log("Rabotaem");
                this.gameObject.GetComponent<WorkBench_scr>().CheckDropCatalog();
                return;
            }
        }
        //Debug.Log("Clear Slots");
        this.gameObject.GetComponent<WorkBench_scr>().UpdateSlots();
    }

    public bool HaveRoomForThisDrop(GameObject item)
    {
        Drop_scr drop = item.GetComponent<Drop_scr>();
        int count = drop.count;
        int id = drop.id;
        for (int i = 0; i < InvItemsLength; i++)
        {
            GameObject iItem = InvItems[i];
            if (iItem != null && iItem.GetComponent<Drop_scr>().id.Equals(id))
            {
                int istack = iItem.GetComponent<Drop_scr>().InStack;
                if (ItemsCount[i] < istack)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

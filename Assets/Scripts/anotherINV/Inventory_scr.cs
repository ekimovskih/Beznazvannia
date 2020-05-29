using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    private GameObject DropCatalog;
    public Vector3 offset = new Vector3(0.4f, -0.4f, 0);
    private GameObject IVTR;


    public int inHand;
    public bool SmthInHand = false;
    public int inHandCount;
    //private int PreviousSlot;
    private Cursor_scr InHandInd;
    //aaaa blyat'
    public int inHand2;
    public int inHandCount2;




    private void Awake()
    {
        cursor = GameObject.Find("Cursor");
        InHandInd = cursor.GetComponent<Cursor_scr>();
        player = GameObject.Find("Player");
        IVTR = GameObject.Find("Inventory");
        InventorySlot[] FS = GameObject.Find("FastPanel").GetComponentsInChildren<InventorySlot>();
        InventorySlot[] IS = IVTR.GetComponentsInChildren<InventorySlot>();
        for(int i = 0; i < 12; i++)
        {
            InvSlots[i] = FS[i];
            InvSlots[i].SlotNumber = i;
        }
        for (int i = 12; i < 48; i++)
        {
            InvSlots[i] = IS[i-12];
            InvSlots[i].SlotNumber = i;
        }
        IVTR.SetActive(false);
    }
    void Start()
    {
        DropCatalog = GameObject.Find("DropCatalog");
        player = GameObject.Find("Player");
        InvItemsLength = InvItems.Length;
        IsInventoryFull();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IVTR.SetActive(!IVTR.activeSelf);
        }


        if (SmthInHand)
        {

        }
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
        InvItems[firstemptycell] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(id);
        ItemsCount[firstemptycell] = count;
        InvSlots[firstemptycell].SetCount(ItemsCount[firstemptycell], InvItems[firstemptycell]);
        drop.IsEmpty();
        IsInventoryFull();
    }

    public void TakeItem(int slot)
    {
        if (InvItems[slot] != null)
        {
            inHand = InvItems[slot].GetComponent<Drop_scr>().id;
            inHandCount = ItemsCount[slot];
            InHandInd.IHindicatorActivity(InvItems[slot].GetComponent<SpriteRenderer>().sprite);
            InvSlots[slot].SlotActivation(false);
            ItemsCount[slot] = 0;
            InvItems[slot] = null;
            SmthInHand = true;
            //PreviousSlot = slot;
            IsInventoryFull();
            Debug.Log("AAAAAA " + slot);
        }
    }

    public void PutItem(int slot)
    {
        //Drop_scr item = inHand.GetComponent<Drop_scr>();
        if (InvItems[slot] != null)
        {
            InHandInd.IHindicatorActivity(InvItems[slot].GetComponent<SpriteRenderer>().sprite);

            inHand2 = InvItems[slot].GetComponent<Drop_scr>().id;
            inHandCount2 = ItemsCount[slot];
            InvItems[slot] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(inHand);
            ItemsCount[slot] = inHandCount;
            InvSlots[slot].SetCount(inHandCount, InvItems[slot]);

            inHand = inHand2;
            inHandCount = inHandCount2;
        }
        else
        {
            InHandInd.IHindicatorActivity(null);
            InvItems[slot] = DropCatalog.GetComponent<DropCatalog_scr>().GetGObyID(inHand);
            ItemsCount[slot] = inHandCount;
            InvSlots[slot].SetCount(inHandCount, InvItems[slot]);
            SmthInHand = false;
        }
        IsInventoryFull();

    }
}

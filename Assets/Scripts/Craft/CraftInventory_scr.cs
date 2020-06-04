using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventory_scr : Inventory_scr
{
    public int BenchSize = 2;
    private int numofslots = 4;
    private int activeSlots = 0;
    private int[] SlotsID;
    public GameObject CraftTable = null;
    private void Awake()
    {

        InvItems = new GameObject[numofslots];
        ItemsCount = new int[numofslots];
        //DropCatalog = GameObject.Find("DropCatalog");
        //IVTR = this.gameObject;
        CraftSlots = CraftTable.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < numofslots; i++)
        {
            if (i < BenchSize)
            {
                CraftSlots[i].Inventory = this.gameObject;
                CraftSlots[i].gameObject.SetActive(true); ;
                CraftSlots[i].SlotActivation(false);
            }
            else
            {
                CraftSlots[i].Inventory = this.gameObject;
                CraftSlots[i].gameObject.SetActive(false);
                CraftSlots[i].SlotActivation(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SlotsID = new int[numofslots];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanCraft()
    {
        int num = 0;
        for(int i = 0; i < BenchSize; i++)
        {
            if (InvItems[i] != null)
            {
                num++;
                SlotsID[i] = InvItems[i].GetComponent<Drop_scr>().id;
            }
            else
            {
                SlotsID[i] = 0;
            }
            
        }
        activeSlots = num;
        return activeSlots>0;
    }

    void CheckForRecipe()
    {
        if (CanCraft())
        {
            //int[] SlotsID;

            DropCatalog_scr DC = DropCatalog.GetComponent<DropCatalog_scr>();
            for (int i = 0; i < DC.length; i++)
            {
                GameObject current = DC.DROP[i];
                //Dcurrent.GetComponent<Drop_scr>();
            }
        }
    }
}

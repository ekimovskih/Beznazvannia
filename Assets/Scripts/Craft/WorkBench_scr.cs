using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBench_scr : MonoBehaviour
{
    public DropCatalog_scr DropCatalog = null;
    private int DClength;
    // GameObject Inventory = null;
    public Inventory_scr InventoryComponent;
    private int size = 60;
    private GameObject[] CanCraftItems;
    private GameObject[] MayCraftItems;
    private GameObject[] AlmostCanCraftItems;
    public CraftSlot[] CraftSlots;
    public GameObject ResultSlots;
    private int CurrentCanSlot = 0;
    private int CurrentMaySlot = 0;
    private int CurrentAlmostCanSlot = 0;
    //public GameObject[] CraftItems = null;
    //public int[] CraftCount= null;
    private void Awake()
    {
        //ResultSlots = GameObject.Find("ResultSlots");
        //Debug.Log(ResultSlots);
        CraftSlots = ResultSlots.GetComponentsInChildren<CraftSlot>();
    }
    void Start()
    {
        /*
        if (Inventory == null)
        {
            Inventory = GameObject.Find("InventoryManager");
            ResultSlots = GameObject.Find("ResultSlots");
        }
        */
        //CraftSlots = ResultSlots.GetComponentsInChildren<CraftSlot>();
        Debug.Log(CraftSlots.Length);
        DClength = DropCatalog.length;
        //InventoryComponent = Inventory.GetComponent<Inventory_scr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlots()
    {
        foreach (CraftSlot slot in CraftSlots)
        {
            slot.SetSlot();
            //Debug.Log("Close");
        }
    }
    public void CheckDropCatalog()
    {
        UpdateSlots();
        //Debug.Log("Otchistil");
        CurrentCanSlot = 0;
        CurrentMaySlot = 0;
        CurrentAlmostCanSlot = 0;
        CanCraftItems = new GameObject[size];
        MayCraftItems = new GameObject[size];
        AlmostCanCraftItems = new GameObject[size];
        int Sum = 0;
        GameObject[] Items = InventoryComponent.CraftItems;
        //GameObject[] Items = InventoryComponent.InvItems;
        int[] Counts = InventoryComponent.CraftCounts;
        //Debug.Log("poneslas");

        for (int i = 0; i < DClength; i++)
        {
            GameObject drop = DropCatalog.DROP[i];
            //Drop_scr dropComponent = drop.GetComponent<Drop_scr>();
            //GameObject[] recipe = dropComponent.Recipe;
            //Debug.Log("poneslas");
            CheckRecipe(Items, drop, Counts);
        }
        //Sum = CurrentCanSlot + CurrentMaySlot + CurrentAlmostCanSlot;
        for (int i = 0; i < CurrentCanSlot; i++)
        {
            if (Sum >= size)
            {
                break;
            }
            CraftSlots[Sum].SetSlot(CanCraftItems[i], 1);
            Sum++;
        }
        for (int i = 0; i < CurrentAlmostCanSlot; i++)
        {
            if (Sum >= size)
            {
                break;
            }
            CraftSlots[Sum].SetSlot(AlmostCanCraftItems[i], 2);
            Sum++;
        }
        for (int i = 0; i < CurrentMaySlot; i++)
        {
            if (Sum >= size)
            {
                break;
            }
            //Debug.Log(CraftSlots[Sum]);
            //Debug.Log(Sum + " " + i);
            CraftSlots[Sum].SetSlot(MayCraftItems[i], 3);
            Sum++;
        }
        for (int k = Sum; k < size; k++)
        {
            CraftSlots[k].SetSlot();
        }
    }

    public void CheckRecipe(GameObject[] craft, GameObject item, int[] Counts)
    {
        Drop_scr drop = item.GetComponent<Drop_scr>();
        GameObject[] recipe = drop.Recipe;
        int Rlength = recipe.Length;
        if (Rlength < 1)
        {
            return;
        }
        int[] recipeCount = drop.RecipeCount;
        int Clength = craft.Length;
        int coincidences = 0;
        bool IdealCount = true;

        for (int i = 0; i < Clength; i++)
        {
            
            if (craft[i] != null)
            {
                int Cid = craft[i].GetComponent<Drop_scr>().id;
                for (int k = 0; k < Rlength; k++)
                {
                    if (Cid == recipe[k].GetComponent<Drop_scr>().id)
                    {
                        if(Counts[i] < recipeCount[k])
                        {
                            IdealCount = false;
                        }
                        //Debug.Log("+ coincidence");
                        coincidences++;
                        break;
                    }
                    if (k== Rlength - 1)
                    {
                        coincidences = -10;
                    }
                }
                if (coincidences < 0)
                {
                    return;
                }
            }
        }
        if(coincidences>= Rlength)
        {
            if (IdealCount)
            {
                CanCraftItems[CurrentCanSlot] = item;
                CurrentCanSlot++;
                return;
            }
            else
            {
                AlmostCanCraftItems[CurrentAlmostCanSlot] = item;
                CurrentAlmostCanSlot++;
                return;
            }        
        }
        else
        {
            MayCraftItems[CurrentMaySlot] = item;
            CurrentMaySlot++;
            return;
        }
    }
}

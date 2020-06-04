using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    //private Inventory_scr InventoryComponent;
    public GameObject CraftItem;
    private Drop_scr CraftItemComponent;
    private int CraftItemCount;
    public GameObject InvSlot;
    public GameObject Count;
    public bool CanCraft = false;
    private Inventory_scr InventoryComponent;

    private void Awake()
    {
        
        //InventoryComponent = Inventory.GetComponent<Inventory_scr>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetSlot();
    }
    public void SetSlot(GameObject item, int CraftPossibility)
    {
        this.gameObject.SetActive(true);
        CraftItem = item;
        CraftItemComponent = item.GetComponent<Drop_scr>();
        CraftItemCount = CraftItemComponent.ResultCount;
        if (CraftItemCount < 2)
        {
            Debug.Log(CraftItemCount);
            Count.GetComponent<Text>().text = CraftItemCount.ToString();
        }
        else
        {
            Count.GetComponent<Text>().text = "";
        }
        InvSlot.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        Color Alpha = InvSlot.transform.GetComponent<Image>().color;
        if (CraftPossibility == 1)
        {
            CanCraft = true;
            Alpha.a = 1f;
            //Debug.Log("+1 Recipe");
        }
        else if (CraftPossibility == 3)
        {
            CanCraft = false;
            Alpha = Color.red;
            Alpha.a = 0.5f;
            //Debug.Log("Paint red " + item.name);
        }
        else
        {
            CanCraft = false;
            Alpha.a = 0.5f;
            //Debug.Log("+1 Allmost Recipe");
        }
        InvSlot.transform.GetComponent<Image>().color = Alpha;
    }
    public void SetSlot()
    {
        InvSlot.transform.GetComponent<Image>().color = Color.white;
        this.gameObject.SetActive(false);
    }

    public void IteractWithSlot()
    {
        Cursor_scr Cursor = GameObject.Find("Cursor").GetComponent<Cursor_scr>();
        if (CanCraft)
        {
            if (!Cursor.HandContainerFull)
            {
                Debug.Log(CraftItem);
                Cursor.CursorContainerActivation(CraftItem, CraftItemCount);


                if (InventoryComponent == null)
                {
                    InventoryComponent = GameObject.Find("InventoryManager").GetComponent<Inventory_scr>();
                }
                InventoryComponent.TakeItem(CraftItemComponent);
            }
            else if (Cursor.HandContainer == CraftItemComponent.id)
            {
                Cursor.CursorContainerActivation(CraftItemCount);
                InventoryComponent.TakeItem(CraftItemComponent);
            }
        }
        else
        {
            Debug.Log("Для сотворения чего-то  не хватает");
        }
    }


}

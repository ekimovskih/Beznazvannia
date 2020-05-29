using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory_scr Inventory;
    public GameObject InvSlot;
    public GameObject Count;
    public int SlotNumber;
    public bool SlotActive = false;
    private bool Empty = false;
    public Sprite Default;
    public Sprite Active;
    //public GameObject Active = null 
    public bool busy = false;

    private Text text;
    private Image sprite;
    private void Awake()
    {
        Inventory = GameObject.Find("InventoryManager").GetComponent<Inventory_scr>();
        InvSlot = this.gameObject.transform.GetChild(0).gameObject;
        Count = InvSlot.gameObject.transform.GetChild(0).gameObject;
        text = Count.GetComponent<Text>();
        sprite = InvSlot.GetComponent<Image>();

    }
    void Start()
    {
        //Inventory = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSlot(GameObject Item, int Count)
    {
        //InvSlot.GetComponent<Image>().sprite = Default;
    }
    public void SlotActivation(bool Activation)
    {
        if (Activation)
        {

        }
        else
        {
            //Inventory.ItemsCount[SlotNumber] = 0;
            Count.GetComponent<Text>().text = "";
        }
        InvSlot.SetActive(Activation);
    }
    public void SetCount(int count)
    {
        text.text = count.ToString();
    }
    public void SetCount(int count, GameObject item)
    {
        sprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
        text.text = count.ToString();
        InvSlot.SetActive(true);
    }

    public void IteractWithSlot()
    {
        if (Inventory.SmthInHand)
        {
            Inventory.PutItem(SlotNumber);
        }
        else
        {
            Inventory.TakeItem(SlotNumber);
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
        //Debug.Log(this.gameObject);
    }
}

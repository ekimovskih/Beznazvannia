using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMagnit_scr : MonoBehaviour
{
    private Inventory_scr Inventory;
    public float MagnitSTR = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("InventoryManager").GetComponent<Inventory_scr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Drop" && !Inventory.isFull)
        {
            Transform collis = collision.gameObject.transform;
            collis.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(this.gameObject.transform.position - collis.position)* MagnitSTR);
        }
    }
}

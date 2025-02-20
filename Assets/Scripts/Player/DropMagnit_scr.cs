﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMagnit_scr : MonoBehaviour
{
    private Inventory_scr Inventory;
    public float MagnitSTR = 2f;
    // Start is called before the first frame update
    private Collider2D Colliz;
    private float dist;

    void Start()
    {
        Inventory = GameObject.Find("InventoryManager").GetComponent<Inventory_scr>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Drop")
        {
            if ((!Inventory.isFull || Inventory.HaveRoomForThisDrop(collision.gameObject)))
            {
                Transform collis = collision.gameObject.transform;
                //Debug.Log((this.gameObject.transform.position - collis.position).magnitude);
                collis.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(this.gameObject.transform.position - collis.position) * MagnitSTR);
            }
            else
            {
                Transform collis = collision.gameObject.transform;
                collis.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(this.gameObject.transform.position - collis.position) * MagnitSTR/1000f);
            }
            
        }
    }
}

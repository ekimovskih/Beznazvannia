using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class ObjOnCell_scr : MonoBehaviour
{
    public string ItemType = "none";
    //public string fffffffffffffffffffff = "default";
    public int health=10;
    public GameObject cell = null;
    public int radiusSpace = 1;
    public bool isgrowing = false;
    // Start is called before the first frame update
    private GameObject Catalog = null;
    public GameObject[] Droplist = null;
    public Vector2Int[] DropRates = null;
    void Start()
    {
        Catalog = GameObject.Find("DropCatalog");
    }

    public bool LastHit(int strengh)
    {
        health -= strengh;
        if (health > 0)
        {
            //Debug.Log("Left " + health);
            return false;
        }
        else
        {
            StartCoroutine(WaitToDrop());
            //Drop(type);
            return true;
        }
    }

    public void Drop(string resoursce)
    {
        //System.Random value = new System.Random();
        int value;
        float chance;
        //GameObject[] Droplist = Catalog.transform.GetComponent<DropCatalog_scr>().GetDropList(type, supertype, this.gameObject);
        if (Droplist == null)
        {
            Debug.LogError("No droplist for " + this.gameObject);
            return;
        }
        if (DropRates == null)
        {
            Debug.LogError("No droprates for " + this.gameObject);
            return;
        }
        int DroplistLGH = Droplist.Length;
        for (int k=0; k< DroplistLGH; k++)
        {
            value = Random.Range(1, DropRates[k].y);
            for (int i = 0; i < value; i++)
            {
                chance = Random.Range(0f, 100f);
                //Debug.Log(chance);
                if (chance < DropRates[k].x)
                {
                    //Debug.Log(i + " " + chance);
                    Instantiate(Droplist[k], this.gameObject.transform.position + (Vector3.one * (i+k*10) / 100), Quaternion.identity, Catalog.transform);
                }
                else
                {
                    //Debug.Log("missfortune");
                }
            }
        }
        Destroy(this.gameObject);
    }
    IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(1f);
        Drop(ItemType);
        //Destroy(this.gameObject);
    }
}

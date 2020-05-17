using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class ObjOnCell_scr : MonoBehaviour
{
    public string type = "tree";
    public string supertype = "default";
    public int health=10;
    public GameObject cell = null;
    public int radiusSpace = 1;
    public bool isgrowing = false;
    // Start is called before the first frame update
    private GameObject Catalog = null;
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
            Drop(type);
            return true;
        }
    }

    public void Drop(string resoursce)
    {
        //System.Random value = new System.Random();
        int value;
        float chance;
        GameObject[] Droplist = Catalog.transform.GetComponent<DropCatalog_scr>().GetDropList(type, supertype, this.gameObject);
        if (Droplist == null)
        {
            Debug.LogError("No droplist for " + type + " " + supertype);
            return;
        }
        if (DropRates == null)
        {
            Debug.LogError("No droprates for " + type + " " + supertype);
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
        /*switch (resoursce) 
        {
            case "tree":
                switch (supertype)
                {
                    case "default":
                        value = Random.Range(1,3);
                        choseDrop = Random.Range(0, DroplistLGH-1);
                        Debug.Log("Droped " + value);
                        //Instantiate(obj, new Vector3(Wposition + 0.5f, -Hposition - 0.5f, -Hposition), Quaternion.identity, ThisObject.transform);
                        for (int i = 0; i < value; i++)
                        {
                            Instantiate(Droplist[choseDrop],this.gameObject.transform.position+(Vector3.one*i/100), Quaternion.identity, Catalog.transform);
                        }
                        break;
                    default:
                        Debug.Log("No Drop");
                        break;
                }
                break;
            default:
                Debug.Log("No Drop");
                break;
        }*/
        Destroy(this.gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropCatalog_scr : MonoBehaviour
{
    public GameObject[] DROP;
    private int[] DropID;
    public GameObject[] Craftable;
    [HideInInspector]public int length;
    private void Start()
    {
        length = DROP.Length;
        Craftable = new GameObject[length];
        DropID = new int[length];
        for (int i = 0; i< length; i++)
        {
            if (DROP[i] != null)
            {
                DropID[i] = DROP[i].GetComponent<Drop_scr>().id;

                /*
                Drop_scr current = GetComponent<Drop_scr>();
                DropID[i] = current.id;
                if (current.Craftable)
                {
                    Craftable[CraftableSlot] = DROP[i];
                    CraftableSlot++;
                }
                */
            }
        }
    }

    public GameObject GetGObyID(int id)
    {
        for (int i = 0; i < length; i++)
        {
            if (id == DropID[i])
            {
                return DROP[i];
            }
        }
        return null;
    }
}

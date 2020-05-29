using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropCatalog_scr : MonoBehaviour
{
    public GameObject[] DROP;
    private int[] DropID;
    private int length;
    private void Start()
    {
        length = DROP.Length;
        DropID = new int[length];
        for (int i = 0; i< length; i++)
        {
            if (DROP[i] != null)
            {
                DropID[i] = DROP[i].GetComponent<Drop_scr>().id;
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

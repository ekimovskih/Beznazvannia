using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public Item FindItemDrop (int id)
    {
        foreach(Item inList in items)
        {
            if (inList.id.Equals(id))
            {
                return inList;
            }
        }
        Debug.LogError("Objects id did not founded");
        return null;
    }
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public Sprite img;
}
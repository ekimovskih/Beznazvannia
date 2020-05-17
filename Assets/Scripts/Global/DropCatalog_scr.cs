using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCatalog_scr : MonoBehaviour
{
    public GameObject[] Tree_default;
    public Vector2Int[] Tree_default_DR;

    public GameObject[] Rock_default;
    public Vector2Int[] Rock_default_DR;

    public GameObject[] GetDropList(string type, string name, GameObject obj)
    {
        //GameObject[] result = null;
        switch (type)
        {
            case "tree":
                switch (name)
                {
                    case "default":
                        obj.GetComponent<ObjOnCell_scr>().DropRates = Tree_default_DR;
                        return Tree_default;

                    default:
                        return null;
                }

            case "rock":
                switch (name)
                {
                    case "default":
                        obj.GetComponent<ObjOnCell_scr>().DropRates = Rock_default_DR;
                        return Rock_default;

                    default:
                        return null;
                }
            default:
                return null;
        }
    }
}

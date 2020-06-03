using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin_scr : MonoBehaviour
{
    //public GameObject Bin;
    private Cursor_scr Cursor;
    private void Awake()
    {
        Cursor = GameObject.Find("Cursor").GetComponent<Cursor_scr>();
    }
    public void DeleteInHand()
    {
        Cursor.CursorContainerActivation();
    }
}

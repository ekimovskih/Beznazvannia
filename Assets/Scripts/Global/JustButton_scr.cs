using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class JustButton_scr : MonoBehaviour
{
    public int count=0;
    public Text text;
    //public GameObject go;
    //public float mod = 1;
    //private Vector3 offset = Vector3.down;
    //public bool off;
    //private Button j;

    //public void
    public void clickCounter()
    {
        count++;
        text.text = count.ToString();
    }

}

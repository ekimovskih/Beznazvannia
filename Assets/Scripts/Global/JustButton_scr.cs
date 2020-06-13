using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JustButton_scr : MonoBehaviour
{
    public int count=0;
    public Text text;
    public void clickCounter()
    {
        count++;
        text.text = count.ToString();
    }
}

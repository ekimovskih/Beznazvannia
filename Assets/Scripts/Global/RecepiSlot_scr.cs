using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecepiSlot_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public Image sprite;
    public Text Count;
    public Text Name;
    public void Set(GameObject item, int count)
    {
        Drop_scr drop = item.GetComponent<Drop_scr>();
        Name.text = drop.supertype;
        Count.text += count.ToString();
        sprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
}

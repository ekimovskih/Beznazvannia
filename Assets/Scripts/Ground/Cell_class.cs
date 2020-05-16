using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_class : MonoBehaviour
{

    public bool usable = true;
    public Sprite Texture = null;
    public string property; // ground \ rock \ sand \ etc
    public int Wposition;
    public int Hposition;
    private GameObject Parent;
    private string Location;
    [HideInInspector] public bool engaged = false;
    private GameObject ThisObject;


    public void Set(GameObject Papa, int Width, int Heigh)
    {
        ThisObject = this.gameObject;
        ThisObject.AddComponent<SpriteRenderer>();
        ThisObject.AddComponent<BoxCollider2D>();
        Parent = Papa;
        Wposition = Width;
        Hposition = Heigh;
        Location = Parent.GetComponent<GridBuilder_scr>().Location;
        ThisObject.transform.position = new Vector3(Width, -Heigh, 0);
        ThisObject.transform.parent = Parent.transform;
        ThisObject.transform.tag = "Cell";

        SetPropertys();
    }

    public void SetPropertys()
    {
        Texture = Parent.GetComponent<GridBuilder_scr>().Textures[0];
        this.gameObject.transform.GetComponent<SpriteRenderer>().sprite = Texture;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Cell")
        {
            //Debug.Log(collision.gameObject.GetComponent<Cell_class>().Wposition + ' ' + collision.gameObject.GetComponent<Cell_class>().Hposition);
            Debug.Log(collision.gameObject.GetComponent<Cell_class>().Wposition + ' ' + collision.gameObject.GetComponent<Cell_class>().Hposition);
        }
    }
}

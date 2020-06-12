using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_class : MonoBehaviour
{

    public bool usable = true;
    //public Sprite Texture = null;
    public string property; // ground \ rock \ sand \ etc
    //public float Wposition;
    //public float Hposition;
    public Vector2 Position;
    private GameObject Parent;
    private string Location;
    [HideInInspector] public bool engaged = false;
    private GameObject ThisObject;

    public GameObject objOnIt = null;
    private void Start()
    {
        ThisObject = this.gameObject;
        
        if (objOnIt != null)
        {
            objOnIt = Instantiate(objOnIt, new Vector3(Position.x + 0.5f, Position.y + 0.5f, Position.y + 0.5f), Quaternion.identity, ThisObject.transform);
        }
        
    }
    
    public void PutObj(GameObject obj)
    {
        if (objOnIt == null)
        {
            objOnIt = Instantiate(obj, new Vector3(Position.x + 0.5f, Position.y + 0.5f, Position.y+0.5f), Quaternion.identity, ThisObject.transform);
            //objOnIt = ThisObject.transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.Log("Cell already busy");
        }
    }
    public void GetObj(Drop_scr InHand)
    {
        GameObject Player = GameObject.Find("Player");
        if (objOnIt != null)
        {
            string ObjType = objOnIt.GetComponent<ObjOnCell_scr>().ItemType;
            string InHandType = InHand.type;
            Debug.Log(ObjType);
            if (ObjType.Equals(InHandType))
            {
                if (InHandType == "Axe")
                {
                    Player.GetComponent<AudioManager>().PlayAudioMine(1);
                }
                else if (InHandType == "Pickaxe")
                {
                    Player.GetComponent<AudioManager>().PlayAudioMine(0);
                }
                if (objOnIt.transform.GetComponent<ObjOnCell_scr>().LastHit(InHand))
                {
                    objOnIt = null;
                }
            }
            
            else
            {
                Player.GetComponent<AudioManager>().PlayAudioAttack();
            }
        }
        else
        {
            Player.GetComponent<AudioManager>().PlayAudioAttack();
            //Debug.Log(objOnIt);
            //Debug.Log("Cell is empty");
        }
    }

}

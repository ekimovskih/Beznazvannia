using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUpShowPanel_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DropPanel;
    public GameObject RecepiPanel;
    public DropPanel_scr[] Drops;
  
    public void CheckDrops(GameObject drop)
    {
        Drops = GetComponentsInChildren<DropPanel_scr>();
        for (int i = 0; i< Drops.Length; i++)
        {
            if (Drops[i].CheckID(drop.GetComponent<Drop_scr>()))
            {
                return;
            }
        }
        ShowNewDrop(drop);
    }
    public void ShowNewDrop(GameObject drop)
    {
        GameObject dropPan = Instantiate(DropPanel, gameObject.transform);
        dropPan.GetComponent<DropPanel_scr>().Show(drop);
    }
    void ShowRecepy()
    {

    }
}

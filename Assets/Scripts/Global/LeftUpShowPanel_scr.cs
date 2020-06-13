using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUpShowPanel_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DropPanel;
    public GameObject RecepiPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowNewDrop()
    {
        GameObject dropPan = Instantiate(DropPanel, gameObject.transform);

    }
    void ShowRecepy()
    {

    }
}

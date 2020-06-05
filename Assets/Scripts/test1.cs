using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    // Start is called before the first frame update
    public test pu = new test();
    public int j;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            pu.inc();
            Debug.Log(pu.g);
        }
    }
}

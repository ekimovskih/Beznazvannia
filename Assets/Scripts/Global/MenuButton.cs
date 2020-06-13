using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject Tab;
    public GameObject ButtonOn;
    public GameObject ButtonOFF;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Tab.SetActive(!Tab.activeSelf);
            ButtonOn.SetActive(!ButtonOn.activeSelf);
            ButtonOFF.SetActive(!ButtonOFF.activeSelf);
            //Debug.Log("sdfsdfsdf");
        }
    }
}

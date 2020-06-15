using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject Tab;
    public GameObject ButtonOn;
    public GameObject ButtonOFF;
    public AudioListener cameraListen;

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
    public void Exit()
    {
        Application.Quit();
    }
    public void Mute()
    {
        //AudioSource[] Asources = 
        AudioListener camera = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        AudioListener.volume = 0f;
        //camera.enabled = !camera.isActiveAndEnabled;
    }
    public void UnMute()
    {
        //AudioSource[] Asources = 
        AudioListener camera = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        AudioListener.volume = 1f;
        //camera.enabled = !camera.isActiveAndEnabled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPause : MonoBehaviour
{
    private AudioListener pauseController;

    public bool soundState;
    // Start is called before the first frame update
    void Start()
    {
        pauseController = gameObject.GetComponent<AudioListener>();
        soundState = true;
    }

    // Update is called once per frame
    void Update()
    {
        pauseController.enabled = soundState;
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (soundState == false)
            {
                soundState = true;
            }
            else
            {
                soundState = false;
            }
        }
    }
}

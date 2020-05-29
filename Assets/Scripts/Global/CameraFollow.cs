using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    //private GameObject Cam;
    private Vector3 zO = new Vector3(1, 1, 0);
    private void Awake()
    {
        //Cam = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }
}

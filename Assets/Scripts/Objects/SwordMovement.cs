using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion dir;
    [Range(-360f, 360f)] public float xy;
    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(30, new Vector3(0,0,1));
        Debug.Log(transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles.z);
        //transform.rotation = Quaternion.AngleAxis(xy, new Vector3(0, 0, 1));
        Quaternion n = Quaternion.AngleAxis(xy, new Vector3(0, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, n, Time.deltaTime * 1000);
        xy +=3;
        //Debug.Log(transform.rotation.eulerAngles);
        //transform.rotation = Quaternion.AngleAxis(xy, new Vector3(0,0,1));
        //transform.rotation = Quaternion.Euler(0, 0, xy);
        //transform.rotation = dir;
    }
}

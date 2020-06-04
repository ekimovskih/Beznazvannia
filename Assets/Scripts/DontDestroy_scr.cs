using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy_scr : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

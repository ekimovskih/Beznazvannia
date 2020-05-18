using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkIndicator_scr : MonoBehaviour
{
    public GameObject[] FastSlots;

    public void ChangeTool(int tool)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = FastSlots[tool].GetComponent<SpriteRenderer>().sprite;
    }
}

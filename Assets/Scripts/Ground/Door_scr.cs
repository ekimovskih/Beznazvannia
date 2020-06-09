using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_scr : MonoBehaviour
{
    public int Direction = 0;
    private GridBuilder_scr papa;
    private void Awake()
    {
        papa = GetComponentInParent<GridBuilder_scr>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log(papa);
            StartCoroutine(Timer(collision.GetComponent<Player_movement_scr>()));
            //papa.TeleportPlayer(Direction);
        }
    }
    IEnumerator Timer(Player_movement_scr van)
    {
        van.CanMove = false;
        StartCoroutine( GameObject.Find("DarkScreen").GetComponent<DarkScreen>().Darker());
        yield return new WaitForSeconds(1f);
        van.CanMove = true;
        papa.TeleportPlayer(Direction);
        
    }
}

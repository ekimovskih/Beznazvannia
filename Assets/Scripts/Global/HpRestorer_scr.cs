using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRestorer_scr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player_movement_scr player = GameObject.Find("Player").GetComponent<Player_movement_scr>();
        if (player.Health < 0)
        {
            GameObject.Find("InventoryManager").GetComponent<Inventory_scr>().DeathLose();
        }
        player.Health = player.MaxHealth;
        player.Stamina = player.MaxStamina;
        player.Completedlevels = 0;
    }
}

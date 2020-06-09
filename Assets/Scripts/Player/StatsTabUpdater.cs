using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsTabUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] RR = new Text[11];
    private Player_movement_scr player;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player_movement_scr>();
        RR = gameObject.GetComponentsInChildren<Text>();
        UpdateFewStats();
    }

    private void Update()
    {
        UpdateStats();
    }
    public void UpdateStats()
    {
        RR[0].text = "Здоровье: " + player.Health + "/" + player.MaxHealth;
        RR[1].text = "Скорость восстановления здоровья: " + player.RegenHP;
        RR[2].text = "Выносливость: " + player.Stamina + "/" + player.MaxStamina;
        RR[3].text = "Скорость восстановления выносливости: " + player.RegenSTM;
        RR[4].text = "Броня: " + player.Armor;
        /*
        RegenSTM += sign * drop.RegenSTM;
        speed += sign * drop.speed;
        JumpStrengh += sign * drop.JumpStrengh;
        JumpWaste += sign * drop.JumpWaste;
        Armor += sign * drop.Armor;
        */
    }
    public void UpdateFewStats()
    {
        RR[0].text = "Здоровье: " + player.Health + "/" + player.MaxHealth;
        RR[1].text = "Скорость восстановления здоровья: " + player.RegenHP;
        RR[2].text = "Выносливость: " + player.Health + "/" + player.MaxStamina;
        RR[3].text = "Скорость восстановления выносливости: " + player.RegenSTM;
    }
}

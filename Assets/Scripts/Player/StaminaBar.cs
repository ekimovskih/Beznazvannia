using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    private GameObject player = null;
    private Player_movement_scr StaminaController;


    void Start()
    {
        player = this.gameObject;
        fill = 1f;
        StaminaController = player.GetComponent<Player_movement_scr>();
        //bar = GameObject.Find("HPBar").GetComponent<Image>();
    }

    void Update()
    {
        float currentSTM = StaminaController.Stamina;
        fill = currentSTM / StaminaController.MaxStamina;
        bar.fillAmount = fill;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    private GameObject go = null;
    private Player_movement_scr hpController;


    void Start()
    {
        go = this.gameObject;
        hpController = go.GetComponent<Player_movement_scr>();
        fill = 1f;
        //bar = GameObject.Find("HPBar").GetComponent<Image>();
    }
    
    void Update()
    {
        float currentHP = hpController.Health;
        fill = currentHP / hpController.MaxHealth; 
        bar.fillAmount = fill;
    }
}

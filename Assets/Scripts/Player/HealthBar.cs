using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    
    void Start()
    {
        fill = 1f;
        bar = GameObject.Find("Bar").GetComponent<Image>();
    }
    
    void Update()
    {
        GameObject go = GameObject.Find("Player");
        Player_movement_scr hpController = go.GetComponent<Player_movement_scr>();
        float currentHP = hpController.Health;
        fill = currentHP / 100; 
        bar.fillAmount = fill;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    public GameObject go = null;


    void Start()
    {
        go = GameObject.Find("Player");
        fill = 1f;
        bar = GameObject.Find("HPBar").GetComponent<Image>();
    }
    
    void Update()
    {
        Player_movement_scr hpController = go.GetComponent<Player_movement_scr>();
        float currentHP = hpController.Health;
        fill = currentHP / 100; 
        bar.fillAmount = fill;
    }
}

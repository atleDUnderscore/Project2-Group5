using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Ref for HealthBar
    [SerializeField] Slider healthSlider;

    //Sets the Healthbar Slider to the current health
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }


    //Sets the Healthbar Slider to max
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    /*Test Keys for the health bar.
     * Tab is Health - 20
     * Left Shift is Health = 100
     * R is Health + 20
     */
    public void Update()
    {/*
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SetHealth((int)healthSlider.value - 20);
            Debug.Log("20 Health Lost");
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetMaxHealth(100);
            Debug.Log("Full Health Restored");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SetHealth((int)healthSlider.value + 20);
            Debug.Log("20 Health Restored");
        }*/
    }
}

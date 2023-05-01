using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Image Health_stats, Stamina_stats;


  public void Display_HealthStats(float healthValue)
    {
        healthValue /= 100;
        Health_stats.fillAmount = healthValue;
    }
    public void Display_Staminastats(float Staminavalue)
    {
        Staminavalue /= 100;
        Stamina_stats.fillAmount = Staminavalue;
    }
}

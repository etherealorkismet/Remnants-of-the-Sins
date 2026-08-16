using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;


    // Update is called once per frame
    void Update()
    {
        if (hpBar != null && PlayerStats.current.maxHealth > 0)
        {
            hpBar.fillAmount = (float)PlayerStats.current.currentHealth / PlayerStats.current.maxHealth;
        }
    }
}

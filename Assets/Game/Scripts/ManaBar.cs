using System;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image manaBar;


    void Update()
    {
        if (manaBar != null && PlayerStats.current.maxMana > 0)
        {
            manaBar.fillAmount = (float)PlayerStats.current.currentMana / PlayerStats.current.maxMana;
        }
    }
}

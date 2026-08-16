using System;
using System.Collections.Generic;
using UnityEngine;

public class SinModifier : MonoBehaviour
{
    public static SinModifier current;
    public enum SinType
    {
        Pride = 1,
        Greed = 2,
        Lust = 3,
        Envy = 4,
        Gluttony = 5,
        Wrath = 6,
        Sloth = 7
    }
    public SinType selectedSin;
    [Header("Pride Modifier Settings")]
    public float damageTaken = 0f;
    public float damageInterval = 50f;
    public bool updating = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        current = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (updating)
        {
            SinCondition((int)selectedSin);
        }
        updating = false;
    }


    public void SinStart(int sin)
    {
        if (sin == 1)
        {
            Debug.Log("I expect... perfection.");
            PlayerStats.current.maxHealth *= 1.05f;
            PlayerStats.current.maxMana *= 1.05f;
            PlayerStats.current.baseManaRegen *= 1.05f;
            PlayerStats.current.baseDamage *= 1.05f;
            PlayerStats.current.baseAttackSpeed *= 1.05f;
            PlayerStats.current.baseSpeed *= 1.05f;
            PlayerStats.current.baseCritChance *= 1.05f;
            PlayerStats.current.baseCritDamage *= 1.05f;
        }
    }

    public void SinCondition(int sin)
    {
        if (sin == 1)
        {
            if (damageTaken >= damageInterval)
            {
                Debug.Log("You are... despicable.");
                PlayerStats.current.currentHealth *= 0.9f;
                PlayerStats.current.attackSpeed *= 0.9f;
                PlayerStats.current.damage *= 0.9f;
                PlayerStats.current.speed *= 0.9f;
                damageTaken -= damageInterval;
            }
        }
        else
        {
            Debug.Log("Sin modifier has not been added yet.");
        }
    }
}

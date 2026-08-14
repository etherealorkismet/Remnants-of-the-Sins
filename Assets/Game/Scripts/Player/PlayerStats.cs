using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float baseDamage = 30f;
    public float baseAttackSpeed = 1f;
    public float baseSpeed = 0.25f;
    public float baseCritChance = 0.02f;
    public float baseCritDamage = 1.5f;

    [Header("Current Stats")]
    public float Currenthealth;
    public float damage;
    public float attackSpeed;
    public float speed;
    public float critChance;
    public float critDamage;

    public static PlayerStats current;
    void Awake()
    {
        current = this;
        Currenthealth = maxHealth;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        speed = baseSpeed;
        critChance = baseCritChance;
        critDamage = baseCritDamage;
    }

    void UpdatePlayerStats()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Currenthealth -= damage;
        if(Currenthealth <= 0)
        {
            Debug.Log("You dead lol");
        }
    }
}
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float maxMana = 100f;
    public float baseManaRegen = 1f;
    public float baseDamage = 30f;
    public float baseAttackSpeed = 1f;
    public float baseSpeed = 0.25f;
    public float baseCritChance = 0.02f;
    public float baseCritDamage = 1.5f;

    [Header("Current Stats")]
    public float currentHealth;
    public float currentMana;
    public float ManaRegen;
    public float damage;
    public float attackSpeed;
    public float speed;
    public float critChance;
    public float critDamage;

    public bool canBeDamaged = true;
    public float immunityTime = 25f;
    public float immunityTimer;

    public bool canRegenMana = false;
    public float regenInterval = 25f;
    public float regenTimer;

    public static PlayerStats current;
    void Awake()
    {
        current = this;
        currentHealth = maxHealth;
        currentMana = maxMana;
        baseManaRegen = ManaRegen;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        speed = baseSpeed;
        critChance = baseCritChance;
        critDamage = baseCritDamage;
    }

    void Update()
    {
        if (!canBeDamaged && immunityTimer <= immunityTime)
        {
            immunityTimer -= 0.1f;
        }
        if (immunityTimer <= 0)
        {
            canBeDamaged = true;
            immunityTimer = immunityTime;
        }

        if (!canRegenMana)
        {
            regenTimer -= 0.1f;
        }
        if (regenTimer <= 0)
        {
            canRegenMana = true;
            regenTimer = regenInterval;
        }
    }

    public void RegenMana()
    {
        if (canRegenMana)
        {
            
        }
    }

    public void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            canBeDamaged = false;
            immunityTimer = immunityTime;
            currentHealth -= damage;
            if(currentHealth <= 0)
            {
                Debug.Log("You dead lol");
            }
        }
    }
}
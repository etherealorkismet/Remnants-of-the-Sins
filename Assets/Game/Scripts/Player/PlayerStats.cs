using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float maxMana = 100f;
    public float baseManaRegen = 5f;
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

    public bool canRegenMana = true;
    public float regenInterval = 3f;
    public float regenTimer;

    public static PlayerStats current;
    void Awake()
    {
        current = this;
        currentHealth = maxHealth;
        currentMana = maxMana;
        ManaRegen = baseManaRegen;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        speed = baseSpeed;
        critChance = baseCritChance;
        critDamage = baseCritDamage;
        StartCoroutine(ManaRegeneration());
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
    /*
        if (!canRegenMana && regenTimer <= regenInterval)
        {
            regenTimer -= 0.1f;
        }
        if (regenTimer <= 0)
        {
            canRegenMana = true;
            regenTimer = regenInterval;
            RegenMana();
        }*/   
    }

    IEnumerator ManaRegeneration()
    {
        while (true)
        {
            if (canRegenMana)
            {
                if (currentMana < maxMana)
                {
                    currentMana += ManaRegen;

                    // Make sure mana doesn't go above max
                    if (currentMana > maxMana)
                    {
                        currentMana = maxMana;
                    }
                }
                else
                {
                    canRegenMana = false;
                    regenTimer = 0f;
                }
            }
            else
            {
                regenTimer += 0.1f;

                if (regenTimer >= regenInterval)
                {
                    canRegenMana = true;
                    regenTimer = 0f;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    

    public void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            canBeDamaged = false;
            immunityTimer = immunityTime;
            currentHealth -= damage;
            SinModifier.current.damageTaken += damage;
            SinModifier.current.updating = true;
            if(currentHealth <= 0)
            {
                Debug.Log("You dead lol");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
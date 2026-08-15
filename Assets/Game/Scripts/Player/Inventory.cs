using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory UI")]
    public RectTransform inventoryBar;
    public bool toggleInventory = false;

    Vector2 inactivePosition = new Vector2(0, 100);
    Vector2 activePosition = new Vector2(0, 0);

    float time = 0f;
    float animationTime = 0.25f;

    [Header("Items")]
    public TextAsset itemJSON;

    ItemDatabase itemDatabase;
    public static Inventory current;

    // Items currently held by the player
    public int NumberOfItems;
    public List<int> heldItems = new List<int>();

    void Awake()
    {
        current = this;

        inventoryBar = GameObject.FindWithTag("InvBar").GetComponent<RectTransform>();

        // Set inventory to closed position
        inventoryBar.anchoredPosition = inactivePosition;

        // Load JSON
        LoadItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeybindManager.keybind.InventoryBar))
        {
            if (!toggleInventory && time == 0f)
            {
                StartCoroutine(
                    InventoryBarObjectUpdate(
                        inactivePosition,
                        activePosition
                    )
                );
            }

            if (toggleInventory && time == 0f)
            {
                StartCoroutine(
                    InventoryBarObjectUpdate(
                        activePosition,
                        inactivePosition
                    )
                );
            }
        }
    }

    IEnumerator InventoryBarObjectUpdate(Vector2 ogPos, Vector2 newPos)
    {
        while (time < animationTime)
        {
            time += Time.deltaTime;

            float t = time / animationTime;

            t = Mathf.SmoothStep(0f, 1f, t);

            inventoryBar.anchoredPosition = Vector3.Lerp(
                new Vector3(ogPos.x, ogPos.y, 0),
                new Vector3(newPos.x, newPos.y, 0),
                t
            );

            yield return null;
        }

        inventoryBar.anchoredPosition = new Vector3(
            newPos.x,
            newPos.y,
            0
        );

        toggleInventory = !toggleInventory;
        time = 0f;
    }

    void LoadItems()
    {
        if (itemJSON == null)
        {
            Debug.LogError("Item JSON is not assigned!");
            return;
        }

        itemDatabase = JsonUtility.FromJson<ItemDatabase>(
            itemJSON.text
        );
        NumberOfItems = itemDatabase.items.Count;
        Debug.Log("Loaded " + itemDatabase.items.Count + " items.");
    }

    public void AddItem(int itemID)
    {
        ItemData item = itemDatabase.items.Find(
            x => x.id == itemID
        );

        if (item == null)
        {
            Debug.LogError("Could not find item: " + itemID);
            return;
        }

        heldItems.Add(itemID);

        ApplyItemEffects(item);
        Debug.Log("ID:" + item.id);
        Debug.Log("Picked up: " + item.name);
    }

    void ApplyItemEffects(ItemData item)
    {
        foreach (StatEffect effect in item.effects)
        {
            ApplyStatEffect(effect);
        }
    }

    void ApplyStatEffect(StatEffect effect)
    {
        switch (effect.stat)
        {
            case "health":

                if (effect.type == "flat")
                {
                    PlayerStats.current.maxHealth += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.maxHealth *= effect.value;
                }
                //logic, if players current hp is more than maxhp after the items effect, 
                if (PlayerStats.current.currentHealth > PlayerStats.current.maxHealth)
                {
                    PlayerStats.current.currentHealth = PlayerStats.current.maxHealth;
                }

                break;

            case "mana":

                if (effect.type == "flat")
                {
                    PlayerStats.current.maxHealth += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.maxHealth *= effect.value;
                }
                //logic, if players current mana is more than maxmana after the items effect, 
                if (PlayerStats.current.currentMana > PlayerStats.current.maxMana)
                {
                    PlayerStats.current.currentMana = PlayerStats.current.maxMana;
                }

                break;

            case "manaregen":

                if (effect.type == "flat")
                {
                    PlayerStats.current.baseManaRegen += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.ManaRegen *= effect.value;
                }

                break;

            case "damage":

                if (effect.type == "flat")
                {
                    PlayerStats.current.damage += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.damage *= effect.value;
                }

                break;

            case "attackSpeed":

                if (effect.type == "flat")
                {
                    PlayerStats.current.attackSpeed += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.attackSpeed *= effect.value;
                }

                break;

            case "speed":

                if (effect.type == "flat")
                {
                    PlayerStats.current.speed += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.speed *= effect.value;
                }

                break;

            case "critChance":

                if (effect.type == "flat")
                {
                    PlayerStats.current.critChance += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.critChance *= effect.value;
                }

                break;

            case "critDamage":

                if (effect.type == "flat")
                {
                    PlayerStats.current.critDamage += effect.value;
                }
                else if (effect.type == "multiplier")
                {
                    PlayerStats.current.critDamage *= effect.value;
                }

                break;

            default:
                Debug.LogWarning(
                    "Unknown stat: " + effect.stat
                );
                break;
        }
    }
}
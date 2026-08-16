using System;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    public static KeybindManager keybind;
    [Header("MovementKeybinds")]
    public KeyCode MoveForward = KeyCode.W;
    public KeyCode MoveLeft = KeyCode.A;
    public KeyCode MoveRight = KeyCode.D;
    public KeyCode MoveDown = KeyCode.S;
    public KeyCode Dash = KeyCode.LeftShift;
    [Header("Others")]
    public KeyCode WeaponSlot1 = KeyCode.Alpha1;
    public KeyCode WeaponSlot2 = KeyCode.Alpha2;
    public KeyCode InventoryBar = KeyCode.Tab;
    public KeyCode TestButton = KeyCode.J;
    public KeyCode TestButton2 = KeyCode.K;
    public KeyCode scroll1 = KeyCode.LeftArrow;
    public KeyCode scroll2 = KeyCode.RightArrow;
    void Awake()
    {
        keybind = this;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(TestButton))
        {
            Inventory.current.AddItem(UnityEngine.Random.Range(0,Inventory.current.NumberOfItems));
        }
        if (Input.GetKeyDown(TestButton2))
        {
            PlayerStats.current.TakeDamage(20);
        }
    }
}

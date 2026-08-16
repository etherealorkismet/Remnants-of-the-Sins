using UnityEngine;
using System;
using Unity.VisualScripting;

public class WeaponDir : MonoBehaviour
{
    Vector2 direction;
    Playermovement playerMovementSC;
    public float distance = 0.8f;
    public GameObject currentWeapon = null;
    public GameObject WeaponSlot1; //temp!!!!!!!!!!!!!!!! prefab of the sword
    public GameObject WeaponSlot2; //bow goes here
    public static WeaponDir current;


    void Awake()
    {
        current = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementSC = GetComponent<Playermovement>();   
        ChangeCurrentWeapon(WeaponSlot1);
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(currentWeapon != null)//move the weapon
        {
            WeaponPos();
        }


        //testing
        if (Input.GetKeyDown(KeybindManager.keybind.WeaponSlot1))
        {
            ChangeCurrentWeapon(WeaponSlot1);
        }
        if (Input.GetKeyDown(KeybindManager.keybind.WeaponSlot2))
        {
            ChangeCurrentWeapon(WeaponSlot2);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseWeapon();//temp!!!!!!!!!!!!!!!!
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopUseWeapon();//temp!!!!!!!!!!!!!!!!
        }

    }

    public void ChangeCurrentWeapon(GameObject Weapon)
    {
        if(currentWeapon == null)
        {
            currentWeapon = GameObject.Instantiate(Weapon,this.transform);//temp!!!!!!!!!!!!!!!! create teh weapon (sword)
        }
        if(currentWeapon.layer != Weapon.layer)
        {
            RemoveOldWeapon();
            GameObject weapon = GameObject.Instantiate(Weapon,this.transform);//temp!!!!!!!!!!!!!!!! create teh weapon (sword)
            currentWeapon = weapon;
            WeaponPos();
        }
    }
    
    void RemoveOldWeapon()
    {
        Destroy(currentWeapon);
    }

    void WeaponPos()
    {
        direction = playerMovementSC.lastMoveDirection;
        float angle = (float)(Math.Atan2(direction.y, direction.x) * (180f/Math.PI));

        currentWeapon.transform.localPosition = direction.normalized * distance;
        

        if (direction.x < 0)
        {
            currentWeapon.transform.rotation = Quaternion.Euler(180,0,-angle);
        }
        else
        {
            currentWeapon.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }

    public bool UseWeapon()//attack script
    {
        if (currentWeapon.GetComponent<Weapon>().Use())
        {
            return true;
        }
        if (currentWeapon.GetComponent<Weapon>().HoldToUseMD())
        {
            return true;
        }
        return false;
        
    }
    
    public void StopUseWeapon()
    {
        currentWeapon.GetComponent<Weapon>().HoldToUseMU();
    }


}

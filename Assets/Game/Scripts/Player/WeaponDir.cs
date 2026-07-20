using UnityEngine;
using System;

public class WeaponDir : MonoBehaviour
{
    Vector2 direction;
    Playermovement playerMovementSC;
    public float distance = 0.8f;
    GameObject currentWeapon = null;
    public GameObject Weapon; //temp!!!!!!!!!!!!!!!! prefab of the sword
    public GameObject Weapon2; //bow goes here




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementSC = GetComponent<Playermovement>();   
        ChangeCurrentWeapon(Weapon);
    }

    // Update is called once per frame
    void Update()
    {   
        if(currentWeapon != null)//move the weapon
        {
            WeaponPos();
        }


        //testing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCurrentWeapon(Weapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCurrentWeapon(Weapon2);
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
        RemoveOldWeapon();
        GameObject weapon = GameObject.Instantiate(Weapon,this.transform);//temp!!!!!!!!!!!!!!!! create teh weapon (sword)
        currentWeapon = weapon;
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
        currentWeapon.transform.rotation = Quaternion.Euler(0,0,angle);
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

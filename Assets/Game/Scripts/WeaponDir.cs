using UnityEngine;
using System;

public class WeaponDir : MonoBehaviour
{
    Vector2 direction;
    Player playerscript;
    public float distance = 0.8f;
    GameObject currentweapon = null;
    public GameObject Weapon; //temp!!!!!!!!!!!!!!!! prefab of the sword
    public GameObject Weapon2; //bow goes here
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerscript = GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {   
        if(currentweapon != null)//move the weapon
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
            Debug.Log(UseWeapon());//temp!!!!!!!!!!!!!!!!
        }

    }

    public void ChangeCurrentWeapon(GameObject Weapon)
    {
        RemoveOldWeapon();
        GameObject weapon = GameObject.Instantiate(Weapon,this.transform);//temp!!!!!!!!!!!!!!!! create teh weapon (sword)
        currentweapon = weapon;
    }
    
    void RemoveOldWeapon()
    {
        Destroy(currentweapon);
    }

    void WeaponPos()
    {
        direction = playerscript.lastMoveDirection;
        float angle = (float)(Math.Atan2(direction.y, direction.x) * (180f/Math.PI));

        currentweapon.transform.localPosition = direction.normalized * distance;
        currentweapon.transform.rotation = Quaternion.Euler(0,0,angle);
    }

    public bool UseWeapon()//attack script
    {
        try
        {
            currentweapon.GetComponent<Weapon>().Use();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class WeaponManagerScript : MonoBehaviour
{
    //MOVE TO WeaponDir.sc
    
    GameObject player;
    WeaponDir playerws; //playerweaponscript
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.gameObject;
        playerws = player.GetComponent<WeaponDir>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //MOVE TO WeaponDir.sc
    

    //MOVE TO WeaponDir.sc
    
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class DrawingGenerator : MonoBehaviour
{

    public GameObject linePrefab;
    Drawing activeLine;
    WeaponDir weaponmang;

    void Start()
    {
        weaponmang = FindFirstObjectByType<WeaponDir>();
    }

    void Update()
    {
        if (weaponmang.holdingSpell())
        {
            if (Input.GetMouseButtonDown(0)) //start line
            {
                GameObject newLine = Instantiate(linePrefab);
                activeLine = newLine.GetComponent<Drawing>();
            }

            if (Input.GetMouseButtonUp(0)) //end line
            {
                activeLine = null;
            }

            if (activeLine != null) //get mouse position
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePos);
            }   
        }
        
    }
}

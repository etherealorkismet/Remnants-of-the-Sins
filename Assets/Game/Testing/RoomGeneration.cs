using UnityEngine;
using System.Collections.Generic;

public class RoomGeneration : MonoBehaviour
{
    public GameObject startroom;
    //Dungeon Settings
    public Vector2 gridsize = new Vector2(5,5);
    public int minimumrooms = 5;
    public int maximumrooms = 10;
    public int currentrooms = 0; 
    //


    //stores which room is in what grid position
    public HashSet<Vector2> occupiedpositions = new HashSet<Vector2>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {


        GameObject start = Instantiate(startroom);
        start.transform.parent = this.transform;

        //count start room
        newRoom();
        //marking 0,0 as occupied cus of the start room
        occupiedpositions.Add(Vector2.zero);

        StartRoom startscript = start.GetComponent<StartRoom>();
        startscript.gridsize = this.gridsize;
        startscript.LevelMang = this.gameObject;
    }
    public void newRoom()
    {
        currentrooms += 1;
    }
    public bool MaxCheck()
    {
        return currentrooms < maximumrooms;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
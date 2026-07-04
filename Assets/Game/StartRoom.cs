/*
using System;
using System.CodeDom.Compiler;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;


public class StartRoom : MonoBehaviour
{
    int[] exits = new int[4] {0,0,0,0};
    public GameObject parent = null;
    public GameObject LevelMang = null;
    Vector2 pos;
    public Vector2 gridsize;
    Vector2[] directions = new Vector2[4]{Vector2.left,Vector2.up,Vector2.right,Vector2.down};


    public GameObject room;
    void Start()
    {
        RoomGeneration roomgenscript = LevelMang.GetComponent<RoomGeneration>();
        transform.position = pos * 5;
        


        for (int i = 0; i < 4; i++)
        {
            exits[i] = UnityEngine.Random.Range(0,2);
            
        }
        if(parent != null)
        {
            StartRoom parentscript = parent.GetComponent<StartRoom>();
            Vector2 parentdir = parentscript.pos - this.pos;
            for(int i = 0; i < 4; i++)
            {
                if(directions[i] == parentdir)
                {
                    exits[i] = 0;
                }
            }
        }


        for (int i = 0; i < 4; i++)
        {
            if (exits[i] == 1)
            {
                if (roomgenscript.MaxCheck())
                {
                    GenerateNewRoom(pos + directions[i]);
                }
                else
                {
                    exits[i] = 0;
                }
                
            }
        }
    }
    GameObject GenerateNewRoom(Vector2 newPos)
    {
        RoomGeneration roomgenscript = LevelMang.GetComponent<RoomGeneration>();
        roomgenscript.newRoom();
        
        GameObject newroom = Instantiate(room);
        StartRoom roomscript = newroom.GetComponent<StartRoom>();
        roomscript.LevelMang = this.LevelMang;
        roomscript.pos = newPos;
        roomscript.parent = this.gameObject;
        return newroom;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
using UnityEngine;

public class StartRoom : MonoBehaviour
{
    int[] exits = new int[4] {0,0,0,0};
    public GameObject parent = null;
    public GameObject LevelMang = null;
    Vector2 pos;
    public Vector2 gridsize;
    Vector2[] directions = new Vector2[4]{Vector2.left,Vector2.up,Vector2.right,Vector2.down};

    public GameObject room;

    void Start()
    {
        RoomGeneration roomgenscript = LevelMang.GetComponent<RoomGeneration>();

        transform.position = pos * 5;

        // Randomly generate exits
        for (int i = 0; i < 4; i++)
        {
            exits[i] = Random.Range(0, 2);
        }

        // Prevent generating back into the parent
        if (parent != null)
        {
            StartRoom parentscript = parent.GetComponent<StartRoom>();
            //get parents direction, example; 0,1 - 0,2 = 0,-1 = Vector2.left
            Vector2 parentdir = parentscript.pos - pos;

            for (int i = 0; i < 4; i++)
            {
                //dont want duplicates exits
                if (directions[i] == parentdir)
                {
                    exits[i] = 0;
                }
            }
        }

        // Generate neighbouring rooms
        for (int i = 0; i < 4; i++)
        {
            if (exits[i] == 1)
            {
                Vector2 newPos = pos + directions[i];
                //checks if current rooms are more than max rooms AND if there is a room occupying the position its currently trying to generate a room in
                if (roomgenscript.MaxCheck() && !roomgenscript.occupiedpositions.Contains(newPos))
                {
                    GenerateNewRoom(newPos);
                }
                else //if there are enough rooms, itll convert the rest of the exits to be closed instead of open
                {
                    exits[i] = 0;
                }
            }
        }
    }

    GameObject GenerateNewRoom(Vector2 newPos)
    {
        RoomGeneration roomGen = LevelMang.GetComponent<RoomGeneration>();

        // Count the room immediately
        roomGen.newRoom();

        // Reserve the position
        roomGen.occupiedpositions.Add(newPos);

        GameObject newroom = Instantiate(room);

        StartRoom roomscript = newroom.GetComponent<StartRoom>();
        roomscript.LevelMang = LevelMang;
        roomscript.pos = newPos;
        roomscript.parent = gameObject;

        return newroom;
    }
}
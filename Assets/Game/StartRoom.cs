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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RoomGeneration roomgenscript = LevelMang.GetComponent<RoomGeneration>();
        roomgenscript.newRoom();
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
                
            }
        }
        

    }
    GameObject GenerateNewRoom(Vector2 newPos)
    {
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

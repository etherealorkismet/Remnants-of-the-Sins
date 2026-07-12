using UnityEngine;
using System.Collections.Generic;

public class RoomNode : MonoBehaviour
{
    public RoomNode parent = null;
    public GameObject levelmang = null;

    public Vector2 gridposition;
    public Vector2 gridsize;

    public RoomType roomtype = RoomType.Normal;
    public int depth = 0;

    public bool[] exits = new bool[4];

    Vector2[] directions =
    {
        Vector2.left,
        Vector2.up,
        Vector2.right,
        Vector2.down
    };

    public void Generate()
    {
        DungeonManager roomgenscript = levelmang.GetComponent<DungeonManager>();

        transform.position = gridposition * 5;

        // Randomly generate exits
        for (int i = 0; i < 4; i++)
        {
            exits[i] = Random.Range(0, 4) == 1;
        }

        // Direction of the parent room
        Vector2 parentdir = Vector2.zero;

        if (parent != null)
        {
            parentdir = parent.gridposition - gridposition;

            // Always keep the doorway back to the parent open
            for (int i = 0; i < 4; i++)
            {
                if (directions[i] == parentdir)
                {
                    exits[i] = true;
                    break;
                }
            }
        }

        // Find all valid directions
        List<int> validDirections = new List<int>();

        int openExitCount = 0;

        for (int i = 0; i < 4; i++)
        {
            //ignore the parent direction
            if (parent != null && directions[i] == parentdir)
                continue;

            Vector2 newPos = gridposition + directions[i];

            if (roomgenscript.cangenerateroom(newPos))
            {
                validDirections.Add(i);

                if (exits[i])
                {
                    openExitCount++;
                }
            }
        }

        //force exits open if havent reached minimum room count
        if (roomgenscript.currentrooms < roomgenscript.minimumrooms)
        {
            int minimumExits;

            if (roomtype == RoomType.Start)
            {
                minimumExits = roomgenscript.startroomminimumexits;
            }
            else
            {
                minimumExits = roomgenscript.normalroomminimumexits;
            }

            while (openExitCount < minimumExits && validDirections.Count > 0)
            {
                int randomIndex = Random.Range(0, validDirections.Count);
                int direction = validDirections[randomIndex];

                if (!exits[direction])
                {
                    exits[direction] = true;
                    openExitCount++;
                }

                validDirections.RemoveAt(randomIndex);
            }
        }

        //generate neighbouring rooms
        for (int i = 0; i < 4; i++)
        {
            //dont gen back into the parent
            if (parent != null && directions[i] == parentdir)
                continue;

            if (exits[i])
            {
                Vector2 newPos = gridposition + directions[i];

                if (roomgenscript.cangenerateroom(newPos))
                {
                    generatenewroom(newPos);
                }
                else
                {
                    exits[i] = false;
                }
            }
        }
    }

    void generatenewroom(Vector2 newPos)
    {
        DungeonManager roomgenscript = levelmang.GetComponent<DungeonManager>();

        GameObject newroom = Instantiate(roomgenscript.roomnodeprefab);
        newroom.transform.parent = levelmang.transform;

        RoomNode roomscript = newroom.GetComponent<RoomNode>();

        roomscript.levelmang = levelmang;
        roomscript.parent = this;
        roomscript.gridposition = newPos;
        roomscript.gridsize = gridsize;
        roomscript.roomtype = RoomType.Normal;
        roomscript.depth = this.depth + 1;

        roomgenscript.newroom(roomscript);

        roomscript.Generate();
    }
}
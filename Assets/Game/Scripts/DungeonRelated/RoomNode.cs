using UnityEngine;
using System.Collections.Generic;

public class RoomNode : MonoBehaviour
{
    public RoomNode parent = null;
    public GameObject levelMang = null;

    public Vector2 gridPosition;
    public Vector2 gridSize;

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
        DungeonManager roomGenSC = levelMang.GetComponent<DungeonManager>();

        transform.position = gridPosition * 15;

        // Randomly generate exits
        for (int i = 0; i < 4; i++)
        {
            exits[i] = Random.Range(0, 4) == 1;
        }

        // Direction of the parent room
        Vector2 parentdir = Vector2.zero;

        if (parent != null)
        {
            parentdir = parent.gridPosition - gridPosition;

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

            Vector2 newPos = gridPosition + directions[i];

            if (roomGenSC.CanGenerateRoom(newPos))
            {
                validDirections.Add(i);

                if (exits[i])
                {
                    openExitCount++;
                }
            }
        }

        //force exits open if havent reached minimum room count
        if (roomGenSC.currentRooms < roomGenSC.minimumRooms)
        {
            int minimumExits;

            if (roomtype == RoomType.Start)
            {
                minimumExits = roomGenSC.startRoomminimumExits;
            }
            else
            {
                minimumExits = roomGenSC.normalRoomminimumExits;
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
                Vector2 newPos = gridPosition + directions[i];

                if (roomGenSC.CanGenerateRoom(newPos))
                {
                    GenerateNewRoom(newPos);
                }
                else
                {
                    exits[i] = false;
                }
            }
        }
    }

    void GenerateNewRoom(Vector2 newPos)
    {
        DungeonManager roomGenSC = levelMang.GetComponent<DungeonManager>();

        GameObject newRoom = Instantiate(roomGenSC.roomnodeprefab);
        newRoom.transform.parent = levelMang.transform;

        RoomNode roomSC = newRoom.GetComponent<RoomNode>();

        roomSC.levelMang = levelMang;
        roomSC.parent = this;
        roomSC.gridPosition = newPos;
        roomSC.gridSize = gridSize;
        roomSC.roomtype = RoomType.Normal;
        roomSC.depth = this.depth + 1;

        roomGenSC.NewRoom(roomSC);

        roomSC.Generate();
    }
}
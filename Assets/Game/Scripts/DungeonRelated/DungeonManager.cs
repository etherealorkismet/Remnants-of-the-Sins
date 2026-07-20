using UnityEngine;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour
{
    [Header("Node Prefab")]
    public GameObject roomnodeprefab;

    [Header("Dungeon Settings")]
    public Vector2 gridSize = new Vector2(5, 5);

    public int minimumRooms = 5;
    public int maximumRooms = 15;
    public int currentRooms = 0;

    [Header("Generation Settings")]
    public int startRoomminimumExits = 2;
    public int normalRoomminimumExits = 1;
    public RoomBuilder roomBuilderSC;
    private float threshold = 0;

    // Every room in the dungeon
    public Dictionary<Vector2, RoomNode> rooms = new Dictionary<Vector2, RoomNode>();

    void Awake()
    {
        roomBuilderSC = GetComponent<RoomBuilder>();

        // Create the starting RoomNode
        GameObject startobject = Instantiate(roomnodeprefab);
        startobject.transform.parent = transform;

        RoomNode startnode = startobject.GetComponent<RoomNode>();

        startnode.gridPosition = Vector2.zero;
        startnode.gridSize = gridSize;
        startnode.levelMang = gameObject;
        startnode.parent = null;
        startnode.roomtype = RoomType.Start;
        startnode.depth = 0;

        NewRoom(startnode);

        //generate the dungeon
        startnode.Generate();

        //assigning room types
        AssignRoomTypes();

        roomBuilderSC.BuildDungeon(rooms);
    }
    
    public void NewRoom(RoomNode room)
    {
        rooms.Add(room.gridPosition, room);//add room into dictionary storing the room and its position
        currentRooms++; //rooms  plus one la
    }

    public bool CanGenerateRoom(Vector2 position)
        {
            float halfWidth = Mathf.Floor(gridSize.x / 2);
            float halfHeight = Mathf.Floor(gridSize.y / 2);

            return currentRooms < maximumRooms &&
                !rooms.ContainsKey(position) &&
                position.x >= -halfWidth &&
                position.x <= halfWidth &&
                position.y >= -halfHeight &&
                position.y <= halfHeight;
        }

    void AssignRoomTypes()
    {
        //find deepest room
        RoomNode bossRoom = null;
        threshold = maximumRooms * 0.85f;
        
        foreach (RoomNode room in rooms.Values)
        {
            if (room.roomtype != RoomType.Normal)
                continue;

            if (bossRoom == null || room.depth > bossRoom.depth)
            {
                bossRoom = room; //setting the deepest room to be the boss room
            }
        }

        if (bossRoom != null)
        {
            bossRoom.roomtype = RoomType.Boss;
        }

        //get the rest of the normal rooms
        List<RoomNode> normalRooms = new List<RoomNode>();

        foreach (RoomNode room in rooms.Values)
        {
            if (room.roomtype == RoomType.Normal)
            {
                normalRooms.Add(room);
            }
        }

        //treasure room
        if (normalRooms.Count > 0)
        {
            int random = Random.Range(0, normalRooms.Count);
            normalRooms[random].roomtype = RoomType.Treasure;

            if (currentRooms >= threshold && normalRooms.Count > 1)//if more than 85% of maximum rooms, generate an extra treasure room
            {
                normalRooms.RemoveAt(random); //remove the treasure room from the list so it cant pick the same room to be the treasure room

                random = Random.Range(0, normalRooms.Count);
                normalRooms[random].roomtype = RoomType.Treasure;
            }
        }
    }
}
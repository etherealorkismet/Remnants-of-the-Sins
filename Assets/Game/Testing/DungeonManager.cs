using UnityEngine;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour
{
    [Header("Node Prefab")]
    public GameObject roomnodeprefab;

    [Header("Dungeon Settings")]
    public Vector2 gridsize = new Vector2(5, 5);

    public int minimumrooms = 5;
    public int maximumrooms = 15;
    public int currentrooms = 0;

    [Header("Generation Settings")]
    public int startroomminimumexits = 2;
    public int normalroomminimumexits = 1;
    public RoomBuilder roombuilder;
    private float threshold = 0;

    // Every room in the dungeon
    public Dictionary<Vector2, RoomNode> rooms = new Dictionary<Vector2, RoomNode>();

    void Awake()
    {
        roombuilder = GetComponent<RoomBuilder>();

        // Create the starting RoomNode
        GameObject startobject = Instantiate(roomnodeprefab);
        startobject.transform.parent = transform;

        RoomNode startnode = startobject.GetComponent<RoomNode>();

        startnode.gridposition = Vector2.zero;
        startnode.gridsize = gridsize;
        startnode.levelmang = gameObject;
        startnode.parent = null;
        startnode.roomtype = RoomType.Start;
        startnode.depth = 0;

        newroom(startnode);

        //generate the dungeon
        startnode.Generate();

        //assigning room types
        AssignRoomTypes();

        roombuilder.BuildDungeon(rooms);
    }
    
    public void newroom(RoomNode room)
    {
        rooms.Add(room.gridposition, room);//add room into dictionary storing the room and its position
        currentrooms++; //rooms  plus one la
    }

    public bool cangenerateroom(Vector2 position)
        {
            float halfWidth = Mathf.Floor(gridsize.x / 2);
            float halfHeight = Mathf.Floor(gridsize.y / 2);

            return currentrooms < maximumrooms &&
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
        threshold = maximumrooms * 0.85f;
        
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

            if (currentrooms >= threshold && normalRooms.Count > 1)//if more than 85% of maximum rooms, generate an extra treasure room
            {
                normalRooms.RemoveAt(random); //remove the treasure room from the list so it cant pick the same room to be the treasure room

                random = Random.Range(0, normalRooms.Count);
                normalRooms[random].roomtype = RoomType.Treasure;
            }
        }
    }
}
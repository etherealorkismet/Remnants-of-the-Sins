using UnityEngine;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour
{
    [Header("Node Prefab")]
    public GameObject roomnodeprefab;

    [Header("Dungeon Settings")]
    public Vector2 gridsize = new Vector2(5, 5);

    public int minimumrooms = 5;
    public int maximumrooms = 10;
    public int currentrooms = 0;

    // Every room in the dungeon
    public Dictionary<Vector2, RoomNode> rooms = new Dictionary<Vector2, RoomNode>();

    void Awake()
    {
        // Create the starting RoomNode
        GameObject startobject = Instantiate(roomnodeprefab);
        startobject.transform.parent = transform;

        RoomNode startnode = startobject.GetComponent<RoomNode>();

        startnode.gridposition = Vector2.zero;
        startnode.gridsize = gridsize;
        startnode.levelmang = gameObject;
        startnode.parent = null;
        startnode.roomtype = RoomType.Start;
        
        newroom(startnode);

        // Generate the whole dungeon
        startnode.Generate();

        // Now generation is finished
        AssignRoomTypes();

        // Later...
        // BuildDungeon();
    }

    public void newroom(RoomNode room)
    {
        rooms.Add(room.gridposition, room);
        currentrooms++;
    }

    public bool cangenerateroom(Vector2 position)
    {
        return currentrooms < maximumrooms &&
               !rooms.ContainsKey(position);
    }

    void AssignRoomTypes()
    {
        List<RoomNode> normalrooms = new List<RoomNode>();

        foreach (RoomNode room in rooms.Values)
        {
            if (room.roomtype == RoomType.Normal)
            {
                normalrooms.Add(room);
            }
        }

        if (normalrooms.Count == 0)
            return;

        // Treasure Room
        int random = Random.Range(0, normalrooms.Count);

        normalrooms[random].roomtype = RoomType.Treasure;
        normalrooms.RemoveAt(random);

        // Boss Room
        if (normalrooms.Count > 0)
        {
            random = Random.Range(0, normalrooms.Count);

            normalrooms[random].roomtype = RoomType.Boss;
            normalrooms.RemoveAt(random);
        }
    }
}
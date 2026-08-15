using UnityEngine;
using System.Collections.Generic;

public class RoomBuilder : MonoBehaviour
{
    public GameObject baseRoom;

    public void BuildDungeon(Dictionary<Vector2, RoomNode> rooms)
    {
        foreach (RoomNode node in rooms.Values)
        {
            Build(node);
        }
    }

    public void Build(RoomNode node)
    {
        GameObject room = Instantiate(
            baseRoom,
            node.transform.position,
            Quaternion.identity,
            node.transform
        );

        RoomVisual visual = room.GetComponent<RoomVisual>();

        if (node.roomtype == RoomType.Start || node.cleared)
        {
            visual.BuildExits(node);
        }
        else
        {
            visual.LockRoom();
        }

        switch (node.roomtype)
        {
            case RoomType.Start:
                Instantiate(visual.playerPrefab, visual.playerSpawn.position, Quaternion.identity);
                Instantiate(visual.dummyPrefab, visual.dummySpawn.position, Quaternion.identity, this.transform);
                break;


            case RoomType.Normal:
                visual.SpawnEnemies(node);
                break;


            case RoomType.Treasure:
                visual.SpawnItems();
                Instantiate(visual.swordPrefab, visual.swordSpawn.position, Quaternion.identity, this.transform);
                Instantiate(visual.bowPrefab, visual.bowSpawn.position, Quaternion.identity, this.transform);
                break;
            case RoomType.Boss:

                Instantiate(visual.bossPrefab, visual.bossSpawn.position, Quaternion.identity, node.transform );
                break;
        }
    }
        
}
using UnityEngine;
using System.Collections.Generic;

public class RoomBuilder : MonoBehaviour
{
    public GameObject baseroom;

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
            baseroom,
            node.transform.position,
            Quaternion.identity,
            node.transform);

        RoomVisual visual = room.GetComponent<RoomVisual>();

        // LEFT
        visual.leftDoor.SetActive(node.exits[0]);
        visual.leftWall.SetActive(!node.exits[0]);

        // UP
        visual.upDoor.SetActive(node.exits[1]);
        visual.upWall.SetActive(!node.exits[1]);

        // RIGHT
        visual.rightDoor.SetActive(node.exits[2]);
        visual.rightWall.SetActive(!node.exits[2]);

        // DOWN
        visual.downDoor.SetActive(node.exits[3]);
        visual.downWall.SetActive(!node.exits[3]);

        switch (node.roomtype) //room creation
        {
            case RoomType.Start:
                Instantiate(visual.playerPrefab, visual.playerSpawn.position, Quaternion.identity);
                Instantiate(visual.dummyPrefab, visual.dummySpawn.position, Quaternion.identity);
                break;

            case RoomType.Normal:
                visual.SpawnEnemies();
                break;

            case RoomType.Treasure:
                Instantiate(visual.chestPrefab, visual.treasureSpawn.position, Quaternion.identity);
                break;

            case RoomType.Boss:
                Instantiate(visual.bossPrefab, visual.bossSpawn.position, Quaternion.identity);
                break;
        }
    }
}
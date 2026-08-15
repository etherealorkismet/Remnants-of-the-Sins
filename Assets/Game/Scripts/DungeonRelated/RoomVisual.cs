using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public class RoomVisual : MonoBehaviour
{
    [Header("Doors")]
    public GameObject leftDoor;
    public GameObject upDoor;
    public GameObject rightDoor;
    public GameObject downDoor;

    [Header("Walls")]
    public GameObject leftWall;
    public GameObject upWall;
    public GameObject rightWall;
    public GameObject downWall;

    [Header("Spawn Points")]
    public Transform playerSpawn;
    public Transform dummySpawn;
    public GameObject enemySpawnGroup;
    public Transform bossSpawn;
    public GameObject treasureSpawn;
    public Transform swordSpawn;
    public Transform bowSpawn;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject dummyPrefab;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject chestPrefab;
    public GameObject swordPrefab;
    public GameObject bowPrefab;

        // LOCK THE ROOM
    // All walls are active and all doors are inactive.
    public void LockRoom()
    {
        leftDoor.SetActive(false);
        upDoor.SetActive(false);
        rightDoor.SetActive(false);
        downDoor.SetActive(false);

        leftWall.SetActive(true);
        upWall.SetActive(true);
        rightWall.SetActive(true);
        downWall.SetActive(true);
    }


    // BUILD THE ROOM BASED ON THE EXITS
    public void BuildExits(RoomNode node)
    {
        // LEFT
        leftDoor.SetActive(node.exits[0]);
        leftWall.SetActive(!node.exits[0]);

        // UP
        upDoor.SetActive(node.exits[1]);
        upWall.SetActive(!node.exits[1]);

        // RIGHT
        rightDoor.SetActive(node.exits[2]);
        rightWall.SetActive(!node.exits[2]);

        // DOWN
        downDoor.SetActive(node.exits[3]);
        downWall.SetActive(!node.exits[3]);
    }




    public void SpawnEnemies(RoomNode room)
    {
        List<Transform> spawnGroups = new List<Transform>();

        foreach (Transform pattern in enemySpawnGroup.transform)
        {
            spawnGroups.Add(pattern);
        }

        Transform selectedPattern = spawnGroups[Random.Range(0, spawnGroups.Count)];
        // Start room
        if (room.depth == 0)
        {
            return;
        }
        foreach (Transform spawnpoint in selectedPattern)
        {
            GameObject enemyPrefab = GetEnemyByDepth(room.depth);

            Instantiate(enemyPrefab, spawnpoint.position, Quaternion.identity, room.transform);
        }
    }

    private GameObject GetEnemyByDepth(int depth)
    {
        int enemyCount = EnemyManager.current.enemyTypes.Count();

        // Convert depth 1 into progression level 0
        int progression = depth - 1;

        // Don't go beyond the strongest enemy
        int maxIndex = Mathf.Min(progression, enemyCount - 1);

        // Random enemy from 0 to maxIndex
        return EnemyManager.current.enemyTypes[
            Random.Range(0, maxIndex + 1)
        ];
    }
    public void SpawnItems()
    {
        foreach(Transform obj in treasureSpawn.transform)
        {
            Instantiate(chestPrefab, obj.transform.position, Quaternion.identity , this.transform);
        }
    }
}
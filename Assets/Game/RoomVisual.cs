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
    public Transform bossSpawn;
    public Transform treasureSpawn;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject bossPrefab;
    public GameObject chestPrefab;

    public void SpawnEnemies()
    {
        Debug.Log("Spawn enemies here.");
    }
}
using UnityEngine;
public class RoomBuilder : MonoBehaviour
{
    public GameObject normalroom;
    public GameObject treasureroom;
    public GameObject bossroom;
    public GameObject startroom;

    public void Build(RoomType roomtype)
    {
        GameObject prefab = null;

        switch(roomtype)
        {
            case RoomType.Start:
                prefab = startroom;
                break;

            case RoomType.Normal:
                prefab = normalroom;
                break;

            case RoomType.Treasure:
                prefab = treasureroom;
                break;

            case RoomType.Boss:
                prefab = bossroom;
                break;
        }

        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }
}
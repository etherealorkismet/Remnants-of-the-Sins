using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;

    public static CameraFollow current;

    public bool cameraUpdate = true;

    Transform leftLimit;
    Transform rightLimit;
    Transform topLimit;
    Transform bottomLimit;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        RoomNode[] rooms = FindObjectsByType<RoomNode>();

        foreach (RoomNode room in rooms)
        {
            if (room.roomtype == RoomType.Start)
            {
                RoomVisual visual = room.GetComponentInChildren<RoomVisual>();

                if (visual != null)
                {
                    SetCameraLimits(visual);
                }

                break;
            }
        }
    }

    void LateUpdate()
    {
        if (cameraUpdate == true)
        {
            if (player == null)
                return;

            float x = Mathf.Clamp(
                player.position.x,
                leftLimit.position.x,
                rightLimit.position.x
            );

            float y = Mathf.Clamp(
                player.position.y,
                bottomLimit.position.y,
                topLimit.position.y
            );

            transform.position = new Vector3(
                x,
                y,
                transform.position.z
            );
        }
    }

    public void SetCameraLimits(RoomVisual room)
    {
        leftLimit = room.leftCameraLimit;
        rightLimit = room.rightCameraLimit;
        topLimit = room.topCameraLimit;
        bottomLimit = room.bottomCameraLimit;
    }
}
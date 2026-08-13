using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    public static CameraFollow current;
    public bool cameraUpdate = true;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (cameraUpdate == true)
        {
            if (player == null)
                return;

            transform.position = new Vector3(
                player.position.x,
                player.position.y,
                transform.position.z
            );   
        }
    }
}
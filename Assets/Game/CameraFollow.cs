using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
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
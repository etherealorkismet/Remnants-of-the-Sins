using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class DoorToRoom : MonoBehaviour
{
    Vector2 left;
    Vector2 up;
    Vector2 right;
    Vector2 down;
    void Awake()
    {
        float spaceBetween = DungeonManager.current.SpaceBetween - (4.75f*2);
        left = new Vector2(-spaceBetween,0);
        up = new Vector2(0,spaceBetween);
        right = new Vector2(spaceBetween,0);
        down = new Vector2(0,-spaceBetween);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("start");
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 playerposition = Playermovement.current.playerPos;
            if (this.gameObject.CompareTag("LeftDoor"))
            {
                Debug.Log("left");
                playerposition += left;
            }
            if (this.gameObject.CompareTag("UpDoor"))
            {
                Debug.Log("up");
                playerposition += up;
            }
            if (this.gameObject.CompareTag("RightDoor"))
            {
                Debug.Log("right");
                playerposition += right;
            }
            if (this.gameObject.CompareTag("DownDoor"))
            {
                Debug.Log("down");
                playerposition += down;
            }
            Debug.Log("end");
            Playermovement.current.UpdatePos(playerposition);
        }
    }
}

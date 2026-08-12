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
        left = new Vector2(-DungeonManager.current.SpaceBetween,0);
        up = new Vector2(0,DungeonManager.current.SpaceBetween);
        right = new Vector2(DungeonManager.current.SpaceBetween,0);
        down = new Vector2(0,-DungeonManager.current.SpaceBetween);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hi this works");
            if (this.gameObject.CompareTag("LeftDoor"))
            {
                Playermovement.current.playerPos += left;
            }
            if (this.gameObject.CompareTag("UpDoor"))
            {
                Playermovement.current.playerPos += up;
            }
            if (this.gameObject.CompareTag("RightDoor"))
            {
                Playermovement.current.playerPos += right;
            }
            if (this.gameObject.CompareTag("DownDoor"))
            {
                Playermovement.current.playerPos += down;
            }
            Playermovement.current.UpdatePos(Playermovement.current.playerPos);
        }
    }
}

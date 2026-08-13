using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class DoorToRoom : MonoBehaviour
{
    Vector2 left;
    Vector2 up;
    Vector2 right;
    Vector2 down;
    float animationTime = 0.4f;
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
        //Debug.Log("start");
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 playerposition = Playermovement.current.playerPos;
            Vector2 oldPosition = playerposition;
            //Vector2 doorPositionx = new Vector2(this.gameObject.transform.position.x, 0);
            //Vector2 doorPositiony = new Vector2(0, this.gameObject.transform.position.y);
            if (this.gameObject.CompareTag("LeftDoor"))
            {
                //Debug.Log("left");
                playerposition += left;
            }
            if (this.gameObject.CompareTag("UpDoor"))
            {
                //Debug.Log("up");
                playerposition += up;
            }
            if (this.gameObject.CompareTag("RightDoor"))
            {
                //Debug.Log("right");
                playerposition += right;
            }
            if (this.gameObject.CompareTag("DownDoor"))
            {
                //Debug.Log("down");
                playerposition += down;
            }
            //Debug.Log("end");
            Debug.Log(this.gameObject.tag);
            Debug.Log(oldPosition);
            Debug.Log(playerposition);
            Playermovement.current.UpdatePos(playerposition);
            StartCoroutine(CameraPan(oldPosition, playerposition));
        }
    }

    IEnumerator CameraPan(Vector2 ogPos, Vector2 newPos)
    {
        CameraFollow.current.cameraUpdate = false;
        Playermovement.current.canMove = false;

        float time = 0f;

        while (time < animationTime)
        {
            time += Time.deltaTime;

            float t = time / animationTime;

            // Smooth the movement
            t = Mathf.SmoothStep(0f, 1f, t);

            CameraFollow.current.transform.position = Vector3.Lerp(
                new Vector3(ogPos.x, ogPos.y, CameraFollow.current.transform.position.z),
                new Vector3(newPos.x, newPos.y, CameraFollow.current.transform.position.z),
                t
            );

            yield return null;
        }

        // Make absolutely sure the camera ends at the new position
        CameraFollow.current.transform.position = new Vector3(
            newPos.x,
            newPos.y,
            CameraFollow.current.transform.position.z
        );

        CameraFollow.current.cameraUpdate = true;
        Playermovement.current.canMove = true;
    }
}

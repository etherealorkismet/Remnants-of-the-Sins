using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.right; // Default dash direction
    private bool isDashing = false;
    private bool canDash = true;

    void Update()
    {
        // Don't read movement input while dashing
        if (!isDashing)
        {
            moveInput = Vector2.zero;

            // Up
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                moveInput.y += 1f;

            // Down
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                moveInput.y -= 1f;

            // Left
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                moveInput.x -= 1f;

            // Right
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                moveInput.x += 1f;

            // Prevent faster diagonal movement
            moveInput.Normalize();

            // Save the last movement direction
            if (moveInput != Vector2.zero)
            {
                lastMoveDirection = moveInput;
            }

            // Normal movement
            transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;
        }

        // Dash with Left Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float timer = 0f;

        while (timer < dashDuration)
        {
            float t = timer / dashDuration;

            // Ease out
            float currentSpeed = dashSpeed * (1 - t * t);

            transform.position += (Vector3)lastMoveDirection * currentSpeed * Time.deltaTime;

            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
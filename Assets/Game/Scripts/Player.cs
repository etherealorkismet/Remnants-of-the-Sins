using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.4f;
    public float dashCooldown = 1f;

    [Header("Animator")]//delete this later when done with the testing
    public Animator animator;
    Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector2 lastMoveDirection = Vector2.right; // Default dash direction
    private bool isDashing = false;
    private bool canDash = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
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
            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);
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

            rb.AddForce(lastMoveDirection * currentSpeed * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

}
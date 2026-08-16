using UnityEngine;
using System.Collections;

public class Playermovement : MonoBehaviour
{
    PlayerStats playerStatsSC;
    [Header("Dash")]
    public float dashMultiplier = 1.564f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Animator")]//delete this later when done with the testing
    public Animator animator;
    Rigidbody2D rb;
    public Vector2 moveInput;
    public Vector2 lastMoveDirection = Vector2.right; // Default dash direction
    private bool isDashing = false;
    private bool canDash = true;
    public Vector2 playerPos;
    public static Playermovement current;
    public bool canMove = true;

    void Start()
    {
        current = this;
        playerStatsSC = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        playerPos = this.transform.position;
        if (canMove == true)
        {
            // Don't read movement input while dashing
            if (!isDashing)
            {
                moveInput = Vector2.zero;

                // Up
                if (Input.GetKey(KeybindManager.keybind.MoveForward))
                    moveInput.y += 1f;

                // Down
                if (Input.GetKey(KeybindManager.keybind.MoveDown))
                    moveInput.y -= 1f;

                // Left
                if (Input.GetKey(KeybindManager.keybind.MoveLeft))
                    moveInput.x -= 1f;

                // Right
                if (Input.GetKey(KeybindManager.keybind.MoveRight))
                    moveInput.x += 1f;

                // Prevent faster diagonal movement
                moveInput.Normalize();

                // Save the last movement direction
                if (moveInput != Vector2.zero)
                {
                    lastMoveDirection = moveInput;
                }

                // Normal movement
                rb.AddForce(moveInput * playerStatsSC.speed);
            }

            // Dash with Left Shift
            if (Input.GetKeyDown(KeybindManager.keybind.Dash) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float timer = 0f;

        while (timer < dashDuration)
        {
            PlayerStats.current.canBeDamaged = false;
            PlayerStats.current.immunityTimer = 50f;
            if (canMove != true)
            {
                break;
            }
            float t = timer / dashDuration;

            // Ease out
            float currentSpeed = dashMultiplier * (1 - t * t);

            rb.AddForce(lastMoveDirection * currentSpeed);

            timer += Time.deltaTime;
            yield return null;
        }
        PlayerStats.current.canBeDamaged = true;
        PlayerStats.current.immunityTimer = PlayerStats.current.immunityTime;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    public void UpdatePos(Vector2 pos)
    {
        playerPos = pos;
        transform.position = pos;
    }

}
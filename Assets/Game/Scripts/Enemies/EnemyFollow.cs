using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

public class EnemyFollow : MonoBehaviour
{
    GameObject player;
    EnemyStats enemyStatsSC;

    public float distanceThreshold = 4f;
    public float attackRange = 1f;
    public bool inRange = false;

    private float distanceBetween;

    void Start()
    {
        enemyStatsSC = GetComponent<EnemyStats>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Calculate distance between enemy and player
        distanceBetween = Vector2.Distance(
            transform.position,
            player.transform.position
        );

        // Check if player is within attack range
        inRange = CheckRange();

        // Damage player if in attack range
        CanDamagePlayer();

        // Follow player if outside attack range
        if (!inRange && distanceBetween < distanceThreshold)
        {
            Vector2 direction = player.transform.position - transform.position;

            // Convert direction into one of 8 directions
            direction = Get8Direction(direction);

            // Move enemy
            transform.position +=
                (Vector3)(direction * enemyStatsSC.speed * Time.deltaTime);
        }
    }

    private Vector2 Get8Direction(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return Vector2.zero;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // RIGHT
        if (angle >= -22.5f && angle < 22.5f)
        {
            return Vector2.right;
        }

        // UP-RIGHT
        if (angle >= 22.5f && angle < 67.5f)
        {
            return new Vector2(1, 1).normalized;
        }

        // UP
        if (angle >= 67.5f && angle < 112.5f)
        {
            return Vector2.up;
        }

        // UP-LEFT
        if (angle >= 112.5f && angle < 157.5f)
        {
            return new Vector2(-1, 1).normalized;
        }

        // LEFT
        if (angle >= 157.5f || angle < -157.5f)
        {
            return Vector2.left;
        }

        // DOWN-LEFT
        if (angle >= -157.5f && angle < -112.5f)
        {
            return new Vector2(-1, -1).normalized;
        }

        // DOWN
        if (angle >= -112.5f && angle < -67.5f)
        {
            return Vector2.down;
        }

        // DOWN-RIGHT
        return new Vector2(1, -1).normalized;
    }

    private bool CheckRange()
    {
        if (distanceBetween <= attackRange)
        {
            return true;
        }

        return false;
    }

    private void CanDamagePlayer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange
        );

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(enemyStatsSC.damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

        // Detection range
        Gizmos.DrawWireSphere(
            transform.position,
            distanceThreshold
        );

        // Attack range
        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}
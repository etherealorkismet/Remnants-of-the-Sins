using UnityEngine;

public class Sword : MonoBehaviour, Weapon
{   
    public bool canAttack = true;
    public float attackCooldown = 10f;
    float time;
    public float attackRange = 1f;
    public bool inRange = false;

    public bool Use()
    {
        if (canAttack)
        {
            CheckGizmoCollision();
            canAttack = false;
        }
        return true;
    }
    public bool HoldToUseMD()
    {
        return false;
    }
    public bool HoldToUseMU()
    {
        return false;
    }

    void Update()
    {
        if (!canAttack && time <= attackCooldown)
        {
            time -= 0.1f;
        }
        if (time <= 0)
        {
            canAttack = true;
            time = attackCooldown;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void CheckGizmoCollision()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange
        );

        //Debug.Log("Objects detected: " + hitColliders.Length);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(PlayerStats.current.damage);

                }
            }
        }
    }
}

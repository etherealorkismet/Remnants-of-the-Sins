using UnityEngine;

public class Bomba : MonoBehaviour
{
    [Header("Bomb Settings")]
    public float windUpTime = 2.5f;
    public float damage;
    public float damageMultiplier = 2f;
    public float explosionRange = 0.8f;

    [Header("Detection")]
    public LayerMask targetLayers;

    private float timeLeft;

    // Set this when the bomb is created

    void Awake()
    {
        damage = PlayerStats.current.damage * damageMultiplier;
        timeLeft = windUpTime;
        transform.SetParent(null);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            CheckGizmoCollision();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    private void CheckGizmoCollision()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRange
        );

        //Debug.Log("Objects detected: " + hitColliders.Length);

        foreach (Collider2D hit in hitColliders)
        {
            //Debug.Log("Explosion detected: " + hit.name);

            if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage);
                    //Debug.Log("Player took " + damage + " damage!");
                }
            }

            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(damage);

                }
            }
        }
    }
}
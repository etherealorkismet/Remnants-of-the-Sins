using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ProjectileProperties : MonoBehaviour
{
    public Rigidbody2D rb;
    GameObject player;
    Playermovement playerSC;
    PlayerStats playerStatsSC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player =  GameObject.FindWithTag("Player");
        playerSC = player.GetComponent<Playermovement>();
        playerStatsSC = player.GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(playerSC.lastMoveDirection * 5 * Time.deltaTime);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
            enemy.TakeDamage(playerStatsSC.damage);
        }
        Destroy(gameObject);
    }
}

using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ProjectileProperties : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Playermovement playerSC;
    PlayerStats playerStatsSC;
    public AttackType TypeSC = AttackType.Click;
    float chargeTime;
    float maxChargeTime;
    float dmg;
    Vector2 dir;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        player =  GameObject.FindWithTag("Player");
        playerSC = player.GetComponent<Playermovement>();
        playerStatsSC = player.GetComponent<PlayerStats>();
        dir = playerSC.lastMoveDirection;
        dmg  = playerStatsSC.damage;
        if (this.gameObject.CompareTag("ProjectileUnaffected"))
        {
            if(transform.parent.CompareTag("Charge"))
            {
                chargeTime = ProjectileController.current.weaponCharge;
                maxChargeTime = ProjectileController.current.weaponMaxCharge;
                dmg = playerStatsSC.damage * (1 + (chargeTime/maxChargeTime)/2);
            }
        }
        transform.SetParent(null);
    }

    void Update()
    {
        rb.MovePosition((Vector3)dir / 10 + transform.position);
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
            enemy.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }
}

using UnityEngine;

public class potion : MonoBehaviour
{
    float healAmount = 25f;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float projectedHealth = PlayerStats.current.currentHealth + healAmount;
            if(projectedHealth > PlayerStats.current.maxHealth)
            {
                PlayerStats.current.currentHealth = PlayerStats.current.maxHealth;
            }
            else
            {
                PlayerStats.current.currentHealth += healAmount;
            }
            
            Destroy(this.gameObject);
        }
    }
}

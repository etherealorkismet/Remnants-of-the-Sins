using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health = 100f;
    float currentHealth;
    public float speed = 2f;
    public float damage = 10f;
    public GameObject potionPrefab;
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            int chance = Random.Range(0,101);
            if(chance >= 50)
            {
                Instantiate(potionPrefab, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log("Damage taken:" + damage + "         Current HP:" +currentHealth);
    }
}

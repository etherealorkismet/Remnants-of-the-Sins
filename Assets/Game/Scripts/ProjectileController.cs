using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static ProjectileController current;
    public GameObject bowDefault;
    public GameObject spellFireBallDefault;
    public GameObject spellBombDefault;
    void Awake()
    {
        current = this;
    }

    public void SpawnProjectile(Transform transform, GameObject projectile)
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}

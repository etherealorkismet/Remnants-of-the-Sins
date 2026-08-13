using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static ProjectileController current;
    public GameObject bowDefault;
    public GameObject spellFireBallDefault;
    public GameObject spellBombDefault;
    public float weaponCharge;
    public float weaponMaxCharge;
    void Awake()
    {
        current = this;
    }

    public void SpawnProjectile(Transform transform, GameObject projectile, Transform parentTransform)
    {
        Instantiate(projectile, transform.position, transform.rotation, parentTransform);
    }

    public void weaponChargeStats(float currentcharge, float maxcharge)
    {
        float sectionSize = maxcharge / 5f;
        weaponCharge = Mathf.Round(currentcharge / sectionSize) * sectionSize;
        weaponMaxCharge = maxcharge;
    }

}

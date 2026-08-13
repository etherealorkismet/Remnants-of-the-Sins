using UnityEngine;

public class Bow : MonoBehaviour, Weapon
{
    bool InUse;
    public float holdtime;
    public Transform projSpawner;
    public float holdThreshold = 500f;
    public GameObject projectile;
    
    void Update()
    {
        if (!InUse)
        {
            holdtime =0;
        }
        if (InUse && holdtime < holdThreshold)
        {
            holdtime += 1;
        }
    }
    public bool Use()
    {
        return false;
    }
    public bool HoldToUseMD()
    {
        InUse = true;
        return true;
    }

    public bool HoldToUseMU()
    {
        if (holdtime >= (holdThreshold/4))
        {
            ProjectileController.current.weaponChargeStats(holdtime,holdThreshold);
            ProjectileController.current.SpawnProjectile(projSpawner,ProjectileController.current.bowDefault,this.transform);
        }
        InUse = false;
        return false;
    }
}

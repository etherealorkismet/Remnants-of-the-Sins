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
        if (InUse)
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
        Debug.Log("arrow");
        InUse = true;
        return true;
    }

    public bool HoldToUseMU()
    {
        if (holdtime > holdThreshold)
        {
            ProjectileController.current.SpawnProjectile(projSpawner,ProjectileController.current.bowDefault);
        }
        InUse = false;
        return false;
    }
}

using UnityEngine;

public class Bow : MonoBehaviour, Weapon
{
    bool InUse;
    float holdtime;
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
            Debug.Log(holdtime);
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
            Instantiate(projectile, projSpawner.position, projSpawner.rotation);
        }
        InUse = false;
        return false;
    }
}

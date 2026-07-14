using UnityEngine;

public class Bow : MonoBehaviour, Weapon
{
    bool InUse;
    float holdtime;
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
        InUse = false;
        return false;
    }
}

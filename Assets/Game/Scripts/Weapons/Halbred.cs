using UnityEngine;

public class Halbred : MonoBehaviour, Weapon
{
    bool InUse;
    float holdtime;
    public float holdThreshold = 500f;
    
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
        InUse = true;
        return true;
    }

    public bool HoldToUseMU()
    {
        if (holdtime > holdThreshold)
        {
            Spin();
        }
        InUse = false;
        return false;
    }

    void Spin()
    {
        //rotate the thing
    }
}

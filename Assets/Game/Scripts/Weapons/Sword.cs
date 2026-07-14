using UnityEngine;

public class Sword : MonoBehaviour, Weapon
{
    public bool Use()
    {
        Debug.Log("NYOOM");
        return true;
    }
    public bool HoldToUseMD()
    {
        return false;
    }
    public bool HoldToUseMU()
    {
        return false;
    }
}

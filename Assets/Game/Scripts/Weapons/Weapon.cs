using UnityEngine;

//example if i have sword and bow that both use "UseWeapon()", it can get both
public interface Weapon
{
    bool Use();
    bool HoldToUseMD();
    bool HoldToUseMU();
}

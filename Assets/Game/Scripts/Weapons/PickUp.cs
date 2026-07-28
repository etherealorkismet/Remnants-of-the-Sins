using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameObject player;
    WeaponDir weaponDirSC;
    public GameObject weaponPrefab;
    
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        weaponDirSC = player.GetComponent<WeaponDir>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            weaponDirSC.WeaponSlot1 = weaponPrefab;
            weaponDirSC.ChangeCurrentWeapon(weaponPrefab);
            Destroy(gameObject);
        }
    }
}

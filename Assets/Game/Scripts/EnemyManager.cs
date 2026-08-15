using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager current;
    public GameObject[] enemyTypes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

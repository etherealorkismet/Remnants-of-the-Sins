using Unity.VisualScripting;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float windUpTime = 2.5f;
    public float explosionRange = 1f;
    private float timeLeft = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {

            Destroy(gameObject);
        }
    }
}

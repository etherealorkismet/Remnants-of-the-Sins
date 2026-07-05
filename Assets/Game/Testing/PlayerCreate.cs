using UnityEngine;

public class PlayerCreate : MonoBehaviour
{
    public GameObject playersprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject player = Instantiate(playersprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

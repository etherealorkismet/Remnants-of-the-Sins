using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;

    public GameObject[] ItemSprites = new GameObject[18];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        id = UnityEngine.Random.Range(0,Inventory.current.NumberOfItems);
        ItemSprites[id].SetActive(true);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.current.AddItem(id);
            Destroy(this.gameObject);
        }
    }
}

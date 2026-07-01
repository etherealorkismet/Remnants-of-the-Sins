using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public GameObject startroom;
    public Vector2 gridsize = new Vector2(5,5);
    int minimumrooms = 5;
    int maximumrooms = 10;
    int currentrooms = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        GameObject start = Instantiate(startroom);
        start.transform.parent = this.transform;

        StartRoom startscript = start.GetComponent<StartRoom>();
        startscript.gridsize = this.gridsize;
        startscript.LevelMang = this.gameObject;
    }
    public void newRoom()
    {
        currentrooms += 1;
    }
    public bool MaxCheck()
    {
        return currentrooms < maximumrooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

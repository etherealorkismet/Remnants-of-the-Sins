using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public RoomNode parent = null;
    public GameObject levelmang = null;

    public Vector2 gridposition;
    public Vector2 gridsize;

    public RoomType roomtype = RoomType.Normal;

    int[] exits = new int[4] { 0, 0, 0, 0 };

    Vector2[] directions =
    {
        Vector2.left,
        Vector2.up,
        Vector2.right,
        Vector2.down
    };

    public void Generate()
    {
        DungeonManager roomgenscript = levelmang.GetComponent<DungeonManager>();

        // Move this RoomNode to its correct position
        transform.position = gridposition * 5;

        // Generate random exits
        for (int i = 0; i < 4; i++)
        {
            exits[i] = Random.Range(0, 2);
        }

        // Don't generate back towards the parent
        if (parent != null)
        {
            Vector2 parentdir = parent.gridposition - gridposition;

            for (int i = 0; i < 4; i++)
            {
                if (directions[i] == parentdir)
                {
                    exits[i] = 0;
                    break;
                }
            }
        }

        // Generate neighbouring rooms
        for (int i = 0; i < 4; i++)
        {
            if (exits[i] == 1)
            {
                Vector2 newpos = gridposition + directions[i];

                if (roomgenscript.cangenerateroom(newpos))
                {
                    generatenewroom(newpos);
                }
                else
                {
                    exits[i] = 0;
                }
            }
        }
    }

    void generatenewroom(Vector2 newpos)
    {
        DungeonManager roomgenscript = levelmang.GetComponent<DungeonManager>();

        // Instantiate a new RoomNode using the manager's prefab
        GameObject newroom = Instantiate(roomgenscript.roomnodeprefab);
        newroom.transform.parent = levelmang.transform;

        RoomNode roomscript = newroom.GetComponent<RoomNode>();

        roomscript.levelmang = levelmang;
        roomscript.parent = this;
        roomscript.gridposition = newpos;
        roomscript.gridsize = gridsize;
        roomscript.roomtype = RoomType.Normal;

        // Register the room
        roomgenscript.newroom(roomscript);

        // Continue generation from the new room
        roomscript.Generate();
    }
}
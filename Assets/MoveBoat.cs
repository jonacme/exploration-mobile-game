using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveBoat : MonoBehaviour
{
    public Tilemap map;

    // Start is called before the first frame update
    void Start()
    {

    }

    Vector3Int RelativeCell(Vector2 direction)
    {
        return map.WorldToCell(transform.position + (Vector3)direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = map.CellToWorld(RelativeCell(new Vector2(-1, 0)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = map.CellToWorld(RelativeCell(new Vector2(1, 0)));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = map.CellToWorld(RelativeCell(new Vector2(0, 1)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = map.CellToWorld(RelativeCell(new Vector2(0, -1)));
        }
    }
}

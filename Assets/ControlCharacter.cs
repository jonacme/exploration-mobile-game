using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControlCharacter : MonoBehaviour
{
    public Tilemap map;

    public bool isMoving;
    public Vector3Int targetCell;
    public Vector3Int currentCell;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
    }

    Vector3Int RelativeCell(Vector2 direction)
    {
        return map.WorldToCell(transform.position + (Vector3)direction);
    }

    IEnumerator WaitToMove()
    {
        var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetCell = map.WorldToCell(worldpos);

        isMoving = true;

        while (map.WorldToCell(transform.position) != targetCell)
        {
            yield return new WaitForSeconds(0.3f);

            var diff = targetCell - map.WorldToCell(transform.position);
            
            Vector3Int nextcell;
            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            {
                nextcell = RelativeCell((new Vector2(diff.x, 0)).normalized);
            }
            else
            {
                nextcell = RelativeCell((new Vector2(0, diff.y)).normalized);
            }

            transform.position = map.CellToWorld(nextcell);
        }

        isMoving = false;

        yield break;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            if (!isMoving)
            {
                StartCoroutine(WaitToMove());
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}

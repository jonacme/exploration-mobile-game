using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    public Tilemap map;
    public Tilemap vis;

    public RuleTile goalTile;
    public RuleTile allCheckedTile;
    public RuleTile emptyTile;

    public Vector3Int start;
    public Vector3Int goal;

    public List<Vector3Int> allChecked;
    public List<Vector3Int> toCheck;


    // Start is called before the first frame update
    void Start()
    {

    }    

    // Update is called once per frame
    void Update()
    {
        start = map.WorldToCell(transform.position);

        if(Input.GetMouseButtonDown(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            goal = map.WorldToCell(worldPos);

            RefreshVisualisation();
        }

        if (Input.GetMouseButtonDown(1))
        {
            toCheck.Add(start);
            RefreshVisualisation();
        }
        if (Input.GetMouseButtonDown(2))
        {
            foreach(var t in toCheck)
            {
                
            }
        }

    }

    private void RefreshVisualisation()
    {
        
    }
}

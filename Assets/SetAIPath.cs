using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class SetAIPath : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private AIDestinationSetter _aiDestinationSetter;

    private Transform _targetTransform;

    private void Awake()
    {
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _targetTransform = new GameObject().transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int mapPos = map.WorldToCell(Input.mousePosition);

            _targetTransform.position = new Vector3(map.CellToWorld(mapPos).x, map.CellToWorld(mapPos).y, 0);
            
            _aiDestinationSetter.target = _targetTransform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Kristofer.exploration
{
    public class PathfindingScript : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap map;
        [SerializeField] private Tilemap visualIndicator;

        [Header("Tiles")]
        public RuleTile goalTile;
        public RuleTile allCheckedTile;
        public RuleTile toCheckTile;
        public RuleTile neighbourTile;
        public RuleTile emptyTile;


        [Header("Positions")]
        public Vector3Int startingPosition;
        public Vector3Int endingPosition;


        [Header("Pathfinding Lists")]
        public List<Vector3Int> allChecked;
        public List<Vector3Int> toCheck;

        private void Start()
        {
            
        }


    }


}

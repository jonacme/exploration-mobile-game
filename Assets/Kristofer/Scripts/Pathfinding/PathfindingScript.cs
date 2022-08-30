using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Kristofer.exploration
{
    public class PathfindingScript : MonoBehaviour
    {
        #region Variables
        [Header("Tilemaps")]
        [SerializeField] private Tilemap map;
        [SerializeField] private Tilemap visualIndicator;
       

        [Header("Tiles")]
        public RuleTile goalTile;
        public RuleTile allCheckedTile;
        public RuleTile toCheckTile;
        public RuleTile neighbourTile;
        public RuleTile emptyTile;
        public RuleTile pathTile;

        [Header("Number of tiles")]
        private int numX = 10;
        private int numY = 10;

        [Header("Positions")]
        public Vector3Int startingPosition;
        public Vector3Int endingPosition;


        [Header("Pathfinding Lists")]
        public List<Vector3Int> allChecked;
        public List<Vector3Int> toCheck;
        public List<Vector3Int> currentNeighbors;
        public Dictionary<Vector3Int, Vector3Int> previousTile;
        public List<Vector3Int> path;

        #endregion

        private Vector3Int[] FindNeighbors(Vector3Int tile)
        {


            return new[]
                {
                tile + new Vector3Int(0,1),
                tile + new Vector3Int(1,0),
                tile + new Vector3Int(0,-1),
                tile + new Vector3Int(-1,0)
                };

        }

        private bool OnGoal(Vector3Int tile)
        {

            return tile == endingPosition;

        }
        private bool CollidesWithWall(Vector3Int tile)
        {

            return map.GetSprite(tile) && map.GetSprite(tile).name == "Barricade Metal";
        }
        


        private void Update()
        {

            startingPosition = map.WorldToCell(transform.position);

            if(Input.GetMouseButtonDown(0))
            {

                previousTile = new Dictionary<Vector3Int, Vector3Int>();
                toCheck = new List<Vector3Int>();
                allChecked = new List<Vector3Int>();
                path = new List<Vector3Int>();

                var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               
                endingPosition = map.WorldToCell(worldPos);
                
                toCheck.Add(startingPosition);
                
                RefreshVisuals();
            }

            if(Input.GetMouseButtonDown(1))
            {

                toCheck.Add(startingPosition);
               
                RefreshVisuals();

            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                currentNeighbors = new List<Vector3Int>();

                foreach (var t in toCheck.ToArray())
                {

                    var neighbors = FindNeighbors(t);
                    currentNeighbors.AddRange(neighbors);

                    foreach (var n in neighbors)
                    {

                        if(allChecked.Contains(n))
                        {

                        }
                        else if(CollidesWithWall(n))
                        {

                        }
                        else if(OnGoal(n))
                        {
                            var curTile = t;
                            path.Add(curTile);
                            var limit = 20;

                            while (previousTile.ContainsKey(curTile))
                            {
                                curTile = previousTile[curTile];
                                path.Add(curTile);

                                limit--;
                                
                                if(limit == 0)
                                break;
                            
                            }

                            RefreshVisuals();
                            

                            return;

                        }
                        else
                        {
                            toCheck.Add(n);
                            
                            if(!previousTile.ContainsKey(n))
                            {
                                previousTile.Add(n, t);
                            }
                            

                        }

                        
                    }
                    toCheck.Remove(t);
                    allChecked.Add(t);
                }
                

                RefreshVisuals();


            }


        }

        private void RefreshVisuals()
        {

            for (int x = -numX; x < numX; x++)
            {

                for (int y = -numY; y < numY; y++)
                {
                    visualIndicator.SetTile(new Vector3Int(x, y), emptyTile);


                }

            }

            foreach (var t in currentNeighbors)
            {
                visualIndicator.SetTile(t, neighbourTile);
            }


            foreach (var t in toCheck)
            {

                visualIndicator.SetTile(t, toCheckTile);
            }
            foreach (var a in allChecked)
            {
                visualIndicator.SetTile(a, allCheckedTile);
            }

            foreach (var p in path)
            {

                visualIndicator.SetTile(p, pathTile);
            }

            visualIndicator.SetTile(endingPosition, goalTile);
        }


     

    }


}

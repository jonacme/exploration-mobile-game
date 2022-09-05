using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Kristofer.exploration
{

    public class PlayerController : MonoBehaviour
    {
        public Tilemap tileMap;

        public bool isMoving;
        [HideInInspector]
        public Vector3Int targetCell;
        
        // Start is called before the first frame update
        void Start()
        {
            isMoving = false;
        }

        Vector3Int RelativeCell(Vector2 direction)
        {
            return tileMap.WorldToCell(transform.position + (Vector3)direction);
        }

        public void SetPos(Vector3 position)
        {
            transform.position = tileMap.WorldToCell(position);

        }


        IEnumerator WaitToMove()
        {
            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetCell = tileMap.WorldToCell(worldpos);

            isMoving = true;

            while (tileMap.WorldToCell(transform.position) != targetCell)
            {
                yield return new WaitForSeconds(0.3f);

                var currentPosition = targetCell - tileMap.WorldToCell(transform.position);
                //Debug.Log(currentPosition);

                Vector3Int nextcell;
                if (Mathf.Abs(currentPosition.x) > Mathf.Abs(currentPosition.y))
                {
                    nextcell = RelativeCell((new Vector2(currentPosition.x, 0)).normalized);
                }
                else
                {
                    nextcell = RelativeCell((new Vector2(0, currentPosition.y)).normalized);
                }

                //transform.position = tileMap.CellToWorld(nextcell);

            }

            isMoving = false;
        }

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


 }




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

namespace Kristofer.exploration
{

    public class PlayerController : MonoBehaviour
    {
        public Tilemap tileMap;
        public KristoferFetch fetch;

        public bool isMoving;
        
        public Vector3Int targetCell;
        public Vector3 nextPos;
        public Vector3 lastPos;
        
        void Start()
        {
           
            Init();
        }

        public void Init()
        {
            isMoving = false;

        }


        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            foreach (var o in FindObjectsOfType<PlayerController>())
            {
                o.Init();

            }

        }



        Vector3Int RelativeCell(Vector2 direction)
        {
            return tileMap.WorldToCell(transform.position + (Vector3)direction);
        }

        public void SetPos(Vector3 position)
        {
            transform.position = tileMap.WorldToCell(position);

        }

        struct MovementData
        {
            public Vector3 direction;
            public Vector3 currentPos;

        }
        
        
        IEnumerator WaitToMove()
        {
            
            isMoving = true;

            while (tileMap.WorldToCell(transform.position) != targetCell)
            {
                yield return new WaitForSeconds(3f);

                var curPos = tileMap.WorldToCell(transform.position);
                
                if (lastPos == curPos && nextPos != targetCell)
                {
                    curPos = new Vector3Int(Mathf.FloorToInt(nextPos.x),Mathf.FloorToInt(nextPos.y),Mathf.FloorToInt(nextPos.z));
                }
                
                
                var diff = targetCell - tileMap.WorldToCell(transform.position);
                
                Vector3 direction;
                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                {
                    //nextcell = RelativeCell((new Vector2(currentPosition.x, 0)).normalized);
                    direction = new Vector3(diff.x, 0, 0).normalized;
                }
                else
                {
                    //nextcell = RelativeCell((new Vector2(0, currentPosition.y)).normalized);
                    direction = new Vector3(0, diff.y, 0).normalized;

                    
                }


                var movement = new MovementData();
                movement.direction = direction;
                movement.currentPos = new Vector3(curPos.x,curPos.y,curPos.z);
                nextPos = movement.currentPos + movement.direction;
              
                
                
                var json = JsonUtility.ToJson(movement);

                fetch.Post("http://127.0.0.1:8125/position/" + fetch.id.name, json);

                lastPos = movement.currentPos;

            }

            isMoving = false;
        }



        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetCell = tileMap.WorldToCell(worldpos);
                Debug.Log(worldpos);

                if (!isMoving)
                {
                    StartCoroutine(WaitToMove());
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }



    }


 }




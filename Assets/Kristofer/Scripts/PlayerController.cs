using System;
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
        
      
        public void Init()
        {
            isMoving = false;

        }
        
        void Start()
        {
           
            Init();
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

        public struct MovementData
        {
            public Vector3 direction;
            public Vector3 currentPos;

        }
        
        
        IEnumerator WaitToMove()
        {
            
            isMoving = true;

            while (tileMap.WorldToCell(transform.position) != targetCell)
            {
                yield return new WaitForSeconds(2f);

                var curPos = tileMap.WorldToCell(transform.position);
                
                if (lastPos == curPos && nextPos != targetCell)
                {
                    curPos = new Vector3Int(Mathf.FloorToInt(nextPos.x),Mathf.FloorToInt(nextPos.y),Mathf.FloorToInt(nextPos.z));
                    //Debug.Log("Waiting to move:" + curPos);
                }


                var diff = targetCell - curPos;
                
                Vector3 direction;
                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                {
                    
                    direction = new Vector3(diff.x, 0, 0).normalized;
                    Debug.Log("move in x direction: " + direction.x);
                }
                else
                {
                    
                    direction = new Vector3(0, diff.y, 0).normalized;
                    Debug.Log("move in y direction:" + direction.y);
                    
                }


                var movement = new MovementData();
                movement.direction = direction;
                movement.currentPos = new Vector3(curPos.x,curPos.y,curPos.z);
                nextPos = movement.currentPos + movement.direction;
                //Debug.Log("Movement: " + movement);
                
                
                var json = JsonUtility.ToJson(movement);
                Debug.Log("JSON: " + json);
                KristoferFetch.Post("http://127.0.0.1:8125/move/" + KristoferFetch.instance.id.name, json);
                
                lastPos = movement.currentPos;
                Debug.Log("lastPos: " + lastPos);

            }

            isMoving = false;

            yield break;

        }



        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null)
                {
                    var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    targetCell = tileMap.WorldToCell(worldPos);
                    Debug.Log("Move on click: " + worldPos);
                }

                if (!isMoving)
                {
                    StartCoroutine(WaitToMove());
                    Debug.Log("Waiting to move....");
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            
        }



    }


 }




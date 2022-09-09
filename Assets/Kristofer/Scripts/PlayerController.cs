using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Kristofer.exploration
{

    public class PlayerController : MonoBehaviour
    {
        public Tilemap tileMap;
        public KristoferFetch fetch;

        public bool isMoving;
        [HideInInspector]
        public Vector3Int targetCell;
        
        
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


        IEnumerator WaitToMove()
        {
            
            isMoving = true;

            while (tileMap.WorldToCell(transform.position) != targetCell)
            {
                yield return new WaitForSeconds(1.0f);

                var currentPosition = targetCell - tileMap.WorldToCell(transform.position);
                
                Vector3 direction;
                if (Mathf.Abs(currentPosition.x) > Mathf.Abs(currentPosition.y))
                {
                    //nextcell = RelativeCell((new Vector2(currentPosition.x, 0)).normalized);
                    direction = new Vector3(currentPosition.x, 0, 0).normalized;
                }
                else
                {
                    //nextcell = RelativeCell((new Vector2(0, currentPosition.y)).normalized);
                    direction = new Vector3(0, currentPosition.y, 0).normalized;

                    
                }

                var json = JsonUtility.ToJson(direction);

                StartCoroutine(fetch.InnerPost("http://127.0.0.1:8125/set-position/" + fetch.id.name, json));


            }

            isMoving = false;
        }



        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetCell = tileMap.WorldToCell(worldpos);

                if (!isMoving)
                {
                    StartCoroutine(WaitToMove());
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }



    }


 }




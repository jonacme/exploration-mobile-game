using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kristofer.exploration
{
    public class RefreshingData : MonoBehaviour
    {
        [SerializeField] private bool isRefreshing = false;
        public PlayerController player;

        void Start()
        {

            Init();
        }

        public void Init()
        {
            isRefreshing = false;

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
            return player.tileMap.WorldToCell(transform.position + (Vector3)direction);
        }

        public void SetPos(Vector3 position)
        {
            transform.position = player.tileMap.WorldToCell(position);

        }


        IEnumerator WaitToMove()
        {
            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.targetCell = player.tileMap.WorldToCell(worldpos);

            isRefreshing = true;

            while (player.tileMap.WorldToCell(transform.position) != player.targetCell)
            {
                yield return new WaitForSeconds(1.0f);

                var currentPosition = player.targetCell - player.tileMap.WorldToCell(transform.position);

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
                Debug.Log(nextcell);
                var json = JsonUtility.ToJson(new Vector3(nextcell.x, nextcell.y, nextcell.z));

                StartCoroutine(KristoferFetch.instance.InnerPost("http://127.0.0.1:8125/set-position/" + KristoferFetch.instance.id.name, json));
            }

            isRefreshing = false;
        }



        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isRefreshing)
                {
                    StartCoroutine(WaitToMove());
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }



    }



}
